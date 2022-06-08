using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akavache;
using ToossyApp.Services;
using ToossyApp.Models;
using ToossyApp.Helper;
using System.Reactive.Linq;


namespace ToossyApp.ViewModels
{
    public class ItineraryPreviewViewModel : BaseViewModel<ItineraryPin[]>
    {
        List<ItineraryPin> _itineraryPins;
        public List<ItineraryPin> ItineraryPins
        {
            get { return _itineraryPins; }
            set
            {
                _itineraryPins = value;
                OnPropertyChanged();
            }
        }


        ItineraryBarycenter _itineraryBarycenter;
        public ItineraryBarycenter ItineraryBarycenter
        {
            get { return _itineraryBarycenter; }
            set
            {
                _itineraryBarycenter = value;
                OnPropertyChanged();
            }
        }

        string _baseUrl;
        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
            }
        }

        readonly IBlobCache _cache;

        public ItineraryPreviewViewModel(INavService navService, IBlobCache cache) : base(navService, cache)
        {
            _cache = cache;
            //_baseUrl = baseUrl.Get();
        }

        public override async Task Init()
        {
        }

        public override async Task Init(ItineraryPin[] pinsArray)
        {
            try
            {
                ItineraryPins = pinsArray.ToList<ItineraryPin>();

                if (pinsArray.Count<ItineraryPin>() == 0)
                {
                    MyPosition = await LoadCurrentPosition();
                }

                ItineraryBarycenter = setItineraryBarycenter();
            }
            catch
            {
            }
        }

        async Task<GeoCoords> LoadCurrentPosition()
        {
            try
            {
                return HelperMethods.syncGetMethod<GeoCoords>(getMyPosition);
            }
            catch
            {
                return new GeoCoords { Latitude = 50, Longitude = 50 };
            }
        }

        async Task<GeoCoords> getMyPosition()
        {
            return await _cache.GetObject<GeoCoords>("MyPosition");
        }

        ItineraryBarycenter setItineraryBarycenter()
        {
            try
            {
                double latitudeAverage = 0;
                double longitudeAverage = 0;

                var pinQty = _itineraryPins.Count;

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


                foreach (ItineraryPin itineraryPin in _itineraryPins)
                {
                    latitudeAverage += itineraryPin.Pin.Position.Latitude;
                    longitudeAverage += itineraryPin.Pin.Position.Longitude;
                }


                latitudeAverage /= pinQty;
                longitudeAverage /= pinQty;

                double range;
                double rangeMax = 0;

                foreach (ItineraryPin itineraryPin in _itineraryPins)
                {

                    range = distanceBetweenTwoLocations(itineraryPin.Pin.Position.Latitude, itineraryPin.Pin.Position.Longitude, latitudeAverage, longitudeAverage);

                    if (range > rangeMax)
                        rangeMax = range;
                }

                var newBarycenter = new ItineraryBarycenter
                {
                    Latitude = latitudeAverage,
                    Longitude = longitudeAverage,
                    Range = rangeMax
                };

                return newBarycenter;
            }
            catch
            {
                return new ItineraryBarycenter { Latitude = 50, Longitude = 50, Range = 500 };
            }
        }
    }
}
