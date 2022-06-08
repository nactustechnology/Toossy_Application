using System.Collections.Generic;
using System.Threading.Tasks;
using ToossyApp.Services;
using ToossyApp.Models;
using Akavache;
using ToossyApp.Helper;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Reactive.Linq;
using System;
using System.Linq;
using System.IO;
using System.Net.Http;

namespace ToossyApp.ViewModels
{
    public class MessageMapViewModel : BaseViewModel<List<MessagePin>>
    {
        List<MessagePin> _messagePins;
        public List<MessagePin> MessagePins
        {
            get { return _messagePins; }
            set
            {
                _messagePins = value;
                OnPropertyChanged();
            }
        }

        List<SelectedItem> _viewedMessages;
        public List<SelectedItem> ViewedMessages
        {
            get { return _viewedMessages; }
            set
            {
                _viewedMessages = value;
                OnPropertyChanged();
            }
        }

        double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        List<SelectedItem> _ratedItineraries;
        public List<SelectedItem> RatedItineraries
        {
            get { return _ratedItineraries; }
            set
            {
                _ratedItineraries = value;
                OnPropertyChanged();
            }
        }



        bool _isOffline;
        public bool IsOffline
        {
            get { return _isOffline; }
            set
            {
                _isOffline = value;
                OnPropertyChanged();
            }
        }

        GeoCoords _boundP1;
        public GeoCoords BoundP1
        {
            get { return _boundP1; }
            set
            {
                _boundP1 = value;
            }
        }

        GeoCoords _boundP2;
        public GeoCoords BoundP2
        {
            get { return _boundP2; }
            set
            {
                _boundP2 = value;
            }
        }

        readonly IItineraryDataService _itineraryDataService;
        public readonly IBlobCache _cache;
        private readonly IGeolocator _locator;

        public MessageMapViewModel(INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService, cache)
        {
            _itineraryDataService = itineraryDataService;
            _cache = cache;
            _locator = CrossGeolocator.Current;

            MyPosition = HelperMethods.syncGetMethod<GeoCoords>(getMyPosition);
            ViewedMessages = (List<SelectedItem>)Application.Current.Properties["ViewedMessages"];
            RatedItineraries = (List<SelectedItem>)Application.Current.Properties["RatedItineraries"];
            IsOffline = HelperMethods.syncGetMethod<bool>(getOfflineState);
            Application.Current.Properties["IsOffline"] = IsOffline;
        }

        async Task<GeoCoords> getMyPosition()
        {
            return await _cache.GetOrCreateObject<GeoCoords>("MyPosition", () => new GeoCoords { Latitude = 48.853396, Longitude = 2.348787 });
        }

        async Task<bool> getOfflineState()
        {
            return await _cache.GetOrCreateObject<bool>("IsOffline", () => false);
        }



        public async Task MessagePreLoading(int messageClef, double progressStep)
        {
            try
            {
                var result = await _cache.GetOrFetchObject(messageClef.ToString(), async () => await getMessageEntry(messageClef), DateTimeOffset.Now.AddDays(2)).Timeout<MessageEntry>(TimeSpan.FromMilliseconds(20000));

                if (result != null)
                {
                    Progress += progressStep;
                }
            }
            catch (Exception ex)
            {
            }
        }

        ItineraryBarycenter setItineraryBarycenter(IList<MessagePin> messagesToBePreLoaded)
        {
            double latitudeAverage = 0;
            double longitudeAverage = 0;

            var pinQty = messagesToBePreLoaded.Count;

            if (pinQty == 0)
            {
                var userLocation = new ItineraryBarycenter
                {
                    Latitude = MyPosition.Latitude,
                    Longitude = MyPosition.Longitude,
                    Range = 500
                };

                return userLocation;
            }


            foreach (MessagePin itineraryPin in messagesToBePreLoaded)
            {
                latitudeAverage += itineraryPin.Pin.Position.Latitude;
                longitudeAverage += itineraryPin.Pin.Position.Longitude;
            }


            latitudeAverage /= pinQty;
            longitudeAverage /= pinQty;

            double range;
            double rangeMax = 0;

            foreach (MessagePin itineraryPin in messagesToBePreLoaded)
            {

                range = distanceBetweenTwoLocations(itineraryPin.Pin.Position.Latitude, itineraryPin.Pin.Position.Longitude, latitudeAverage, longitudeAverage);

                if (range > rangeMax)
                    rangeMax = range;
            }

            var newBarycenter = new ItineraryBarycenter
            {
                Latitude = latitudeAverage,
                Longitude = longitudeAverage,
                Range = rangeMax + 0.5
            };

            return newBarycenter;
        }

        async Task<MessageEntry> getMessageEntry(int messageClef)
        {
            return await _itineraryDataService.GetMessageEntryAsync(messageClef);
        }

        public override async Task Init()
        {
        }

        public async Task StartListening()
        {
            try
            {
                if (_locator.IsListening)
                    return;

                ///This logic will run on the background automatically on iOS, however for Android and UWP you must put logic in background services. Else if your app is killed the location updates will be killed.
                await _locator.StartListeningAsync(TimeSpan.FromSeconds(4), 30).ConfigureAwait(continueOnCapturedContext: false);
                _locator.PositionChanged += async (sender, e) =>
                {
                    var position = e.Position;

                    MyPosition = new GeoCoords()
                    {
                        Latitude = position.Latitude,
                        Longitude = position.Longitude
                    };
                    await _cache.InsertObject("MyPosition", MyPosition);
                };
            }
            catch (Exception ex)
            {

            }
        }

        public async Task StopListening()
        {
            if (!_locator.IsListening)
                return;
            await _locator.StopListeningAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<bool> LoadCurrentPosition()
        {
            try
            {
                MyPosition = await HelperMethods.getPositionFromDevice();
                await _cache.InsertObject("MyPosition", MyPosition);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task checkRating()
        {
            try
            {
                if (MessagePins == null)
                    return;

                var messagePinsToBeChecked = MessagePins;
                int ititneraryIdToBeRated = default(int);

                foreach (MessagePin messagePin in messagePinsToBeChecked)
                {
                    var isItineraryFinished = messagePinsToBeChecked.FindAll(item => item.Id_Itinerary == messagePin.Id_Itinerary);

                    if (RatedItineraries.FindAll(item => item.Clef == messagePin.Id_Itinerary).Count == 0 && isItineraryFinished.FindAll(item => item.ViewStatus != 2).Count == 0)
                    {
                        RatedItineraries.Add(new SelectedItem() { Clef = messagePin.Id_Itinerary, DateLimit = DateTimeOffset.Now.AddDays(3) });
                        ititneraryIdToBeRated = messagePin.Id_Itinerary;
                    }

                }

                if (ititneraryIdToBeRated != default(int))
                    await GoToRatingPageCommand(ititneraryIdToBeRated);
            }
            catch (Exception ex)
            {

            }
        }

        public override async Task Init(List<MessagePin> messagePins)
        {
            getBounds(messagePins);
            MessagePins = messagePins;
        }

        Command<MessagePin> _goToMessageDetailPage;
        public Command<MessagePin> GoToMessageDetailPage
        {
            get
            {
                return _goToMessageDetailPage ?? (_goToMessageDetailPage = new Command<MessagePin>(async (messageDetails) => await GoToMessageDetailPageCommand(messageDetails)));
            }
        }

        async Task GoToMessageDetailPageCommand(MessagePin messageDetails)
        {
            await NavService.NavigateTo<MessageDetailViewModel, MessagePin>(messageDetails);
        }

        /*Command<int> _goToRatingPage;
        public Command<int> GoToRatingPage
        {
            get
            {
                return _goToRatingPage ?? (_goToRatingPage = new Command<int>(async (itineraryKey) => await GoToRatingPageCommand(itineraryKey)));
            }
        }*/

        async Task GoToRatingPageCommand(int itineraryKey)
        {
            await NavService.NavigateTo<RatingViewModel, int>(itineraryKey);
        }

        async void getBounds(List<MessagePin> pinsList)
        {
            try
            {
                BoundP1 = new GeoCoords();
                BoundP2 = new GeoCoords();

                if (pinsList.Count != 0)
                {
                    var lngMin = pinsList.Select(x => x.Pin.Position.Longitude).Min();
                    var lngMax = pinsList.Select(x => x.Pin.Position.Longitude).Max();

                    var latMin = pinsList.Select(x => x.Pin.Position.Latitude).Min();
                    var latMax = pinsList.Select(x => x.Pin.Position.Latitude).Max();

                    double degLatKm = 110.574235;
                    double deltaLat = 1 / degLatKm;

                    latMin -= deltaLat;
                    latMax += deltaLat;

                    double latRadian = ((latMin + latMax) / 2) * Math.PI / 180;
                    double degLongKm = 110.572833 * Math.Cos(latRadian);
                    double deltaLong = 1 / degLongKm;

                    lngMin -= deltaLong;
                    lngMax += deltaLong;

                    //définition des 2 coins du rectangle

                    BoundP1.Longitude = lngMin;
                    BoundP1.Latitude = latMin;

                    BoundP2.Longitude = lngMax;
                    BoundP2.Latitude = latMax;
                }
                else
                {
                    await LoadCurrentPosition();

                    BoundP1.Longitude = MyPosition.Longitude;
                    BoundP1.Latitude = MyPosition.Latitude;

                    BoundP2.Longitude = BoundP1.Longitude;
                    BoundP2.Latitude = BoundP1.Latitude;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<double> saveMap()
        {
            try
            {
                int MapLenght;

                int XfirstTile;
                int XlastTile;
                int YfirstTile;
                int YlastTile;

                int tileNumber;

                var zoomMin = 11;
                var zoomMax = 17;


                tileNumber = getTileNumber(zoomMin, zoomMax);

                var progressStep = Convert.ToDouble(1) / Convert.ToDouble(tileNumber);

                string rootDirectory = "";

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        //rootDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                        break;
                    case Device.Android:
                        //rootDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                        break;
                }


                if (!Directory.Exists(rootDirectory.ToString() + "/toossy_map"))
                {
                    Directory.CreateDirectory(rootDirectory.ToString() + "/toossy_map");
                }

                var mapRootDirectory = rootDirectory.ToString() + "/toossy_map";

                HttpClient client = new HttpClient();
                Progress = 0;

                for (int z = zoomMin; z < zoomMax; z++)
                {
                    MapLenght = (int)Math.Pow(2, z);

                    var SouthWestTile = getTileIndex(BoundP1, MapLenght);
                    var NorthEastTile = getTileIndex(BoundP2, MapLenght);

                    XfirstTile = SouthWestTile[0];
                    XlastTile = NorthEastTile[0];
                    YfirstTile = NorthEastTile[1];
                    YlastTile = SouthWestTile[1];

                    var XtileMapRootDirectory = mapRootDirectory.ToString() + "/" + z.ToString();

                    if (!Directory.Exists(XtileMapRootDirectory))
                    {
                        Directory.CreateDirectory(XtileMapRootDirectory);
                    }


                    var XfolderList = Directory.GetDirectories(XtileMapRootDirectory);

                    foreach (string folderPath in XfolderList)
                    {
                        var folderPathArray = folderPath.Split('/');

                        int number;

                        if (Int32.TryParse(folderPathArray.Last(), out number).Equals(false) || (number < XfirstTile || number > XlastTile))
                        {
                            Directory.Delete(folderPath, true);
                        }
                    }

                    for (int x = XfirstTile; x < XlastTile + 1; x++)
                    {
                        var YtileMapRootDirectory = XtileMapRootDirectory.ToString() + "/" + x.ToString();

                        if (!Directory.Exists(YtileMapRootDirectory))
                        {
                            Directory.CreateDirectory(YtileMapRootDirectory);
                        }

                        var YfileList = Directory.GetFiles(YtileMapRootDirectory);

                        foreach (string filePath in YfileList)
                        {
                            var filePathArray = filePath.Split('/');

                            filePathArray = filePathArray.Last().Split('.');

                            int number;

                            if (Int32.TryParse(filePathArray.First(), out number).Equals(false) || (number < YfirstTile || number > YlastTile))
                            {
                                File.Delete(filePath);
                            }
                        }


                        for (int y = YfirstTile; y < YlastTile + 1; y++)
                        {
                            string tileFilePath = YtileMapRootDirectory.ToString() + "/" + y.ToString() + ".png";

                            Progress += progressStep;

                            if (!System.IO.File.Exists(tileFilePath))
                            {
                                var url = new Uri(String.Format("http://tile.stamen.com/terrain/{0}/{1}/{2}.png", z, x, y));

                                await downloadTile(client, url, tileFilePath);
                            }
                        }
                    }
                }

                return progressStep;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        int[] getTileIndex(GeoCoords Position, int mapLenght)
        {
            try
            {
                var xtile = (int)Math.Floor(mapLenght * ((Position.Longitude + 180) / 360));

                var latitude_rad = Position.Latitude * Math.PI / 180;

                var ytile = (int)Math.Floor(mapLenght * (1 - (Math.Log(Math.Tan(latitude_rad) + (1 / Math.Cos(latitude_rad))) / Math.PI)) / 2);

                return new int[] { xtile, ytile };
            }
            catch (Exception ex)
            {
                return new int[] { 0, 0 };
            }
        }

        async Task downloadTile(HttpClient client, Uri url, string tileFilePath)
        {
            try
            {
                var imageByte = await client.GetByteArrayAsync(url);
                System.IO.File.WriteAllBytes(tileFilePath, imageByte);
            }
            catch (Exception ex)
            {
            }
        }

        int getTileNumber(int zoomMin, int zoomMax)
        {
            try
            {
                int tileNumber = 0;
                int MapLenght;

                for (int z = zoomMin; z < zoomMax; z++)
                {
                    MapLenght = (int)Math.Pow(2, z);

                    var SouthWestTile = getTileIndex(BoundP1, MapLenght);
                    var NorthEastTile = getTileIndex(BoundP2, MapLenght);


                    tileNumber += (NorthEastTile[0] - SouthWestTile[0] + 1) * (SouthWestTile[1] - NorthEastTile[1] + 1);
                }

                return tileNumber;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}

