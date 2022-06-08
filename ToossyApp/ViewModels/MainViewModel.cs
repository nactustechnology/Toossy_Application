using Akavache;
using System.Collections.Generic;
using ToossyApp.Services;
using Xamarin.Forms;
using ToossyApp.Models;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ToossyApp.Helper;
using System;
using System.Linq;
using System.IO;
using ToossyApp.Resources;
using Xamarin.Forms.Maps;


namespace ToossyApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ItineraryFilter _itineraryFiltre;
        public ItineraryFilter ItineraryFiltre
        {
            get { return _itineraryFiltre; }
            set
            {
                _itineraryFiltre = value;
            }
        }

        bool _isFirstItineraryTest;
        public bool IsFirstItineraryTest
        {
            get { return _isFirstItineraryTest; }
            set
            {
                _isFirstItineraryTest = value;
                OnPropertyChanged();
            }
        }

        List<ItineraryEntry> _itineraryEntries;
        public List<ItineraryEntry> ItineraryEntries
        {
            get { return _itineraryEntries; }
            set
            {
                _itineraryEntries = value;
                OnPropertyChanged();
            }
        }

        List<MessagePin> _displayedMessages;
        public List<MessagePin> DisplayedMessages
        {
            get { return _displayedMessages; }
            set
            {
                _displayedMessages = value;
            }
        }

        List<SelectedItem> _selectedItineraries;
        public List<SelectedItem> SelectedItineraries
        {
            get { return _selectedItineraries; }
            set
            {
                _selectedItineraries = value;
            }
        }

        List<TransactionResponse> _visitesPayees;
        public List<TransactionResponse> VisitesPayees
        {
            get { return _visitesPayees; }
            set
            {
                _visitesPayees = value;
                OnPropertyChanged();
            }
        }

        readonly IItineraryDataService _itineraryDataService;
        public readonly IBlobCache _cache;

        //______________________________________CONSTRUCTOR____________________________________________
        //*********************************************************************************************

        public MainViewModel(ILocationService locService, INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService)
        {
            try
            {
                _itineraryDataService = itineraryDataService;
                _cache = cache;

                IsLoading = false;

                _displayedMessages = new List<MessagePin>();

                SelectedItineraries = HelperMethods.syncGetMethod<List<SelectedItem>>(getSelectedItineraries);
                Application.Current.Properties["SelectedItineraries"] = SelectedItineraries;

                ItineraryEntries = HelperMethods.syncGetMethod<List<ItineraryEntry>>(getItineraryEntries);
                Application.Current.Properties["ItineraryEntries"] = ItineraryEntries;

                MyPosition = HelperMethods.syncGetMethod<GeoCoords>(getMyPosition);

                ItineraryFiltre = HelperMethods.syncGetMethod<ItineraryFilter>(getFilter);
                Application.Current.Properties["ItineraryFilter"] = ItineraryFiltre;



                VisitesPayees = HelperMethods.syncGetMethod<List<TransactionResponse>>(getVisitesPayees);
                Application.Current.Properties["VisitesPayees"] = VisitesPayees;

                Application.Current.Properties["ViewedMessages"] = HelperMethods.syncGetMethod<List<SelectedItem>>(getViewedMessages);
                Application.Current.Properties["RatedItineraries"] = HelperMethods.syncGetMethod<List<SelectedItem>>(getRatedItineraries);
                Application.Current.Properties["commentList"] = HelperMethods.syncGetMethod<List<ItineraryComment>>(getCommentList);
                Application.Current.Properties["reportList"] = HelperMethods.syncGetMethod<List<ItineraryReport>>(getReportList);
                Application.Current.Properties["FirstStartStatus"] = HelperMethods.syncGetMethod<bool>(getFirstStartStatus);
                _isFirstItineraryTest = false;
            }
            catch (Exception ex)
            {

            }
        }

        //*********************************************************************************************************************************

        async Task<ItineraryFilter> getFilter()
        {
            return await _cache.GetOrCreateObject<ItineraryFilter>("ItineraryFilter", () => getFilterDefaultValue());
        }

        ItineraryFilter getFilterDefaultValue()
        {
            Dictionary<string, string[]> DefaultFiltreDictionary = FiltreDictionary.getDictionary();

            var defaultFilter = new ItineraryFilter
            {
                FiltreDistance = DefaultFiltreDictionary["FiltreDistance"],
                FiltreDuree = DefaultFiltreDictionary["FiltreDuree"],
                FiltreGratuitTarif = DefaultFiltreDictionary["FiltreGratuitTarif"],
                FiltreLangue = DefaultFiltreDictionary["FiltreLangue"],
                FiltreNote = DefaultFiltreDictionary["FiltreNote"],
                FiltreTheme = DefaultFiltreDictionary["FiltreTheme"],
                FiltreTypeParcours = DefaultFiltreDictionary["FiltreTypeParcours"],
            };

            return defaultFilter;
        }

        async Task<bool> getFirstStartStatus()
        {
            bool Result = await _cache.GetOrCreateObject<bool>("FirstStartStatus", () => true);

            return Result;
        }

        async Task<List<SelectedItem>> getSelectedItineraries()
        {
            List<SelectedItem> Result = await _cache.GetOrCreateObject<List<SelectedItem>>("SelectedItineraries", () => new List<SelectedItem> { });

            Result.RemoveAll(item => item.DateLimit < DateTimeOffset.Now);

            return Result;
        }

        async Task<List<TransactionResponse>> getVisitesPayees()
        {
            List<TransactionResponse> Result = await _cache.GetOrCreateObject<List<TransactionResponse>>("VisitesPayees", () => new List<TransactionResponse> { });

            Result.RemoveAll(item => item.DateFin < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            return Result;
        }

        async Task<List<SelectedItem>> getViewedMessages()
        {
            List<SelectedItem> Result = await _cache.GetOrCreateObject<List<SelectedItem>>("ViewedMessages", () => new List<SelectedItem> { });

            Result.RemoveAll(item => item.DateLimit < DateTimeOffset.Now);

            return Result;
        }

        async Task<List<ItineraryComment>> getCommentList()
        {
            return await _cache.GetOrCreateObject<List<ItineraryComment>>("commentList", () => new List<ItineraryComment>());
        }

        async Task<List<ItineraryReport>> getReportList()
        {
            return await _cache.GetOrCreateObject<List<ItineraryReport>>("reportList", () => new List<ItineraryReport>());
        }

        async Task<List<SelectedItem>> getRatedItineraries()
        {
            List<SelectedItem> Result = await _cache.GetOrCreateObject<List<SelectedItem>>("RatedItineraries", () => new List<SelectedItem> { });

            Result.RemoveAll(item => item.DateLimit < DateTimeOffset.Now);

            return Result;
        }

        async Task<List<ItineraryEntry>> getItineraryEntries()
        {
            return await _cache.GetOrCreateObject<List<ItineraryEntry>>("ItineraryEntries", () => new List<ItineraryEntry> { });
        }

        async Task<GeoCoords> getMyPosition()
        {
            return await _cache.GetOrCreateObject<GeoCoords>("MyPosition", () => new GeoCoords { Latitude = 50, Longitude = 50 });
        }

        //*********************************************************************************************************************************************
        public override async Task Init()
        {
        }

        public async Task<bool> LoadCurrentPosition()
        {
            if (IsLoading)
                return false;

            IsLocalizing = true;
            IsLoading = true;

            try
            {
                MyPosition = await HelperMethods.getPositionFromDevice();
                await _cache.InsertObject("MyPosition", MyPosition);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                IsLocalizing = false;
            }
        }

        public async Task<bool> LoadItineraries(bool newGPSCoordinates = true)
        {
            if (IsLoading && IsLocalizing)
                return false;

            IsLoading = true;

            try
            {
                _itineraryEntries = await getNewItineraryEntries();

                //_________________check si des parcours n'apparaissent plus________

                var ItinerariesCached = await _cache.GetObject<List<ItineraryEntry>>("ItineraryEntries");

                foreach (SelectedItem selectedItinerary in SelectedItineraries)
                {
                    if (ItineraryEntries.Exists(item => item.Clef == selectedItinerary.Clef).Equals(false))
                    {
                        try
                        {
                            var itineraryToBeAdded = ItinerariesCached.Find(item => item.Clef == selectedItinerary.Clef);

                            if (itineraryToBeAdded != null)
                            {
                                _itineraryEntries.Add(itineraryToBeAdded);

                            }
                        }
                        catch (KeyNotFoundException ex)
                        {
                        }
                    }
                }

                //__________________________________________________________________

                ItineraryEntries = _itineraryEntries;

                await _cache.InsertObject("ItineraryEntries", ItineraryEntries);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {

                IsLoading = false;
            }
        }

        async Task<List<ItineraryEntry>> getNewItineraryEntries()
        {
            return await _itineraryDataService.GetItineraryEntriesAsync((ItineraryFilter)Application.Current.Properties["ItineraryFilter"], MyPosition);
        }

        public void setSelectedItineraries()
        {
            foreach (ItineraryEntry itinerary in _itineraryEntries)
            {
                if (_selectedItineraries.Exists(item => item.Clef == itinerary.Clef))
                {
                    itinerary.IsSelected = true;
                    addToDisplayedMessagesList(itinerary.Clef);
                }
                else
                {
                    itinerary.IsSelected = false;
                }
            }
        }

        public void selectItinerary(int itineraryClef)
        {
            if (_selectedItineraries.Exists(item => item.Clef == itineraryClef).Equals(false))
            {
                _selectedItineraries.Add(new SelectedItem() { Clef = itineraryClef, DateLimit = DateTimeOffset.Now.AddDays(3) });

                addToDisplayedMessagesList(itineraryClef);
            }
            else
            {
                _selectedItineraries.Find(item => item.Clef == itineraryClef).DateLimit = DateTimeOffset.Now.AddDays(3);
            }
        }

        public async Task<bool> checkPaidItineraries()
        {
            try
            {
                if (_visitesPayees.Count != 0)
                {
                    var researchedToken = string.Join(", ", _visitesPayees.Select(x => x.Token).ToArray());

                    var tokenResult = new List<SelectedItem>();

                    if (researchedToken != null)
                    {
                        tokenResult = await _itineraryDataService.GetTokenResult(researchedToken);
                    }

                    var payantItineraries = _itineraryEntries.FindAll(item => item.Payant == 1).Select(item => item.Clef);

                    foreach (int itineraryClef in payantItineraries)
                    {
                        _selectedItineraries.RemoveAll(item => item.Clef == itineraryClef);
                    }


                    foreach (SelectedItem itinerary in tokenResult)
                    {

                        selectPaidItinerary(itinerary.Clef, itinerary.DateLimit);
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void selectPaidItinerary(int itineraryClef, DateTimeOffset DateFin)
        {

            if (_selectedItineraries.Exists(item => item.Clef == itineraryClef).Equals(false) && DateFin > DateTimeOffset.UtcNow)
            {
                _selectedItineraries.Add(new SelectedItem() { Clef = itineraryClef, DateLimit = DateFin });
                _itineraryEntries.Find(item => item.Clef == itineraryClef).IsSelected = true;

                addToDisplayedMessagesList(itineraryClef);
            }
            /*else if(_selectedItineraries.Exists(item => item.Clef == itineraryClef).Equals(true) && DateFin < DateTimeOffset.UtcNow)
            {
                SelectedItineraries.RemoveAll(item => item.Clef == itineraryClef );
            }*/
        }

        void addToDisplayedMessagesList(int itineraryClef)
        {
            List<ItineraryEntry> searchList = (List<ItineraryEntry>)ItineraryEntries;
            ItineraryEntry selectedItinerary = searchList.Find(itinerary => itinerary.Clef == itineraryClef);
            ItineraryPin[] messagePinArray = selectedItinerary.ItineraryPins;
            List<SelectedItem> viewedMessagesList = (List<SelectedItem>)Application.Current.Properties["ViewedMessages"];

            int i = 1;
            int previousMessageViewStatus = 0;

            if (messagePinArray != null)
            {
                foreach (ItineraryPin messagePin in messagePinArray)
                {
                    var doublon = DisplayedMessages.FindAll(item => item.Id_Message == messagePin.Id_Message);

                    if (doublon.Count.Equals(0))
                    {
                        var newMessagePin = new MessagePin();
                        newMessagePin.Id_Itinerary = itineraryClef;
                        newMessagePin.Id_Message = messagePin.Id_Message;



                        if (selectedItinerary.Type_parcours == 1)
                        {
                            if (i == messagePinArray.Length)
                            {
                                newMessagePin.Id_Next_Message = newMessagePin.Id_Message;
                            }
                            else
                            {
                                newMessagePin.Id_Next_Message = messagePinArray[i].Id_Message;
                            }

                            if (viewedMessagesList.FindAll(item => item.Clef == newMessagePin.Id_Message).Count > 0)
                            {
                                newMessagePin.ViewStatus = 2;
                            }
                            else if (i == 1 || previousMessageViewStatus == 2)
                            {
                                newMessagePin.ViewStatus = 1;
                            }
                            else
                            {
                                newMessagePin.ViewStatus = 0;
                            }

                            previousMessageViewStatus = newMessagePin.ViewStatus;
                        }
                        else
                        {
                            newMessagePin.Id_Next_Message = 0;
                            newMessagePin.ViewStatus = 1;
                        }

                        newMessagePin.IsTelechargeable = false;

                        if (selectedItinerary.Telechargeable == 1)
                        {
                            newMessagePin.IsTelechargeable = true;
                        }

                        newMessagePin.Pin = messagePin.Pin;

                        newMessagePin.IsAvailable = false;

                        DisplayedMessages.Add(newMessagePin);
                    }

                    i++;
                }
            }


            //Application.Current.Properties["displayedMessages"] = DisplayedMessages;
        }

        public void unselectItinerary(int Clef)
        {
            if (_selectedItineraries.Exists(item => item.Clef == Clef).Equals(true))
            {
                _selectedItineraries.RemoveAll(item => item.Clef == Clef);

                removeFromDisplayedMessagesList(Clef);
            }
        }

        void removeFromDisplayedMessagesList(int itineraryClef)
        {
            DisplayedMessages.RemoveAll(message => message.Id_Itinerary == itineraryClef);

            //Application.Current.Properties["displayedMessages"] = DisplayedMessages;
        }

        public async Task<ItineraryComment> sendComment(ItineraryComment newComment)
        {
            return await _itineraryDataService.AddCommentAsync(newComment);
        }

        //_______________________________________Commandes de navigation____________________________________________
        //**********************************************************************************************************

        Command _goToFilterPage;
        public Command GoToFilterPage
        {
            get
            {
                return _goToFilterPage ?? (_goToFilterPage = new Command(async () => await GoToFilterPageCommand()));
            }
        }

        Command<List<MessagePin>> _goToMessagePage;
        public Command<List<MessagePin>> GoToMessagePage
        {
            get
            {
                return _goToMessagePage ?? (_goToMessagePage = new Command<List<MessagePin>>(async (messagePins) => await GoToMessagePageCommand(messagePins)));
            }
        }

        Command<ItineraryEntry> _goToItineraryDetailPage;
        public Command<ItineraryEntry> GoToItineraryDetailPage
        {
            get
            {
                return _goToItineraryDetailPage ?? (_goToItineraryDetailPage = new Command<ItineraryEntry>(async (itineraryDetails) => await GoToItineraryDetailPageCommand(itineraryDetails)));
            }
        }

        Command<int> _goToItineraryPayPage;
        public Command<int> GoToItineraryPayPage
        {
            get
            {
                return _goToItineraryPayPage ?? (_goToItineraryPayPage = new Command<int>(async (itineraryClef) => await GoToItineraryPayPageCommand(itineraryClef)));
            }
        }

        async Task GoToFilterPageCommand()
        {
            await NavService.NavigateTo<FilterViewModel>();
        }

        async Task GoToMessagePageCommand(List<MessagePin> messagePins)
        {
            await NavService.NavigateTo<MessageMapViewModel, List<MessagePin>>(messagePins);
        }

        async Task GoToItineraryPayPageCommand(int itineraryClef)
        {
            var itineraryDetails = ItineraryEntries.Find(item => item.Clef == itineraryClef);
            await NavService.NavigateTo<ItineraryPayViewModel, ItineraryEntry>(itineraryDetails);
        }

        async Task GoToItineraryDetailPageCommand(ItineraryEntry itineraryDetails)
        {
            await NavService.NavigateTo<ItineraryDetailViewModel, ItineraryEntry>(itineraryDetails);
        }

        //________________________________MenuViewModel________________________________________________________________
        //*************************************************************************************************************

        Command _goToTutorialPage;
        public Command GoToTutorialPage
        {
            get
            {
                return _goToTutorialPage ?? (_goToTutorialPage = new Command(async () => await GoToTutorialPageCommand()));
            }
        }

        public async Task GoToTutorialPageCommand()
        {
            await NavService.NavigateTo<TutorialViewModel>();
        }

        Command _goToFavoriteListPage;
        public Command GoToFavoriteListPage
        {
            get
            {
                return _goToFavoriteListPage ?? (_goToFavoriteListPage = new Command(async () => await GoToFavoriteListPageCommand()));
            }
        }

        public async Task GoToFavoriteListPageCommand()
        {
            await NavService.NavigateTo<FavoriteListViewModel>();
        }

        Command _goToMentionsLegalesPage;
        public Command GoToMentionsLegalesPage
        {
            get
            {
                return _goToMentionsLegalesPage ?? (_goToMentionsLegalesPage = new Command(async () => await GoToMentionsLegalesPageCommand()));
            }
        }

        public async Task GoToMentionsLegalesPageCommand()
        {
            try
            {
                await NavService.NavigateTo<MentionsLegalesViewModel>();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task goOnTwitter()
        {
            var address = "https://twitter.com/";

            Device.OpenUri(new Uri(address));
        }

        public async Task goOnFacebook()
        {
            var address = "https://www.facebook.com/111281252874167";

            Device.OpenUri(new Uri(address));
        }

        public async Task flushMemory()
        {
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

            rootDirectory += "/toossy_map";

            System.IO.DirectoryInfo di = new DirectoryInfo(rootDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public async Task<bool> setUnsetTestItinerary()
        {
            bool isTestItinerarySet = await _cache.GetOrCreateObject<bool>("IsTestItinerarySet", () => false);

            if (isTestItinerarySet)
            {
                await unsetTestItinerary();

                await _cache.InsertObject("IsTestItinerarySet", false);

                return false;
            }
            else
            {
                await setTestItinerary();

                await _cache.InsertObject("IsTestItinerarySet", true);

                return true;
            }
        }

        async Task unsetTestItinerary()
        {

            unselectItinerary(0);

            ItineraryEntries.RemoveAll(item => item.Clef == 0);

            await _cache.InsertObject<List<ItineraryEntry>>("ItineraryEntries", ItineraryEntries);
            Application.Current.Properties["ItineraryEntries"] = ItineraryEntries;

            await _cache.InvalidateObject<MessageEntry>("1");
            await _cache.InvalidateObject<MessageEntry>("2");
            await _cache.InvalidateObject<MessageEntry>("3");
            await _cache.InvalidateObject<MessageEntry>("4");
            await _cache.InvalidateObject<MessageEntry>("5");
            await _cache.InvalidateObject<MessageEntry>("6");
            await _cache.InvalidateObject<MessageEntry>("7");

            await LoadItineraries();
        }

        async Task setTestItinerary()
        {
            try
            {
                var testItinerary = new ItineraryEntry
                {
                    Clef = 0,
                    Titre = AppResources.MenuPage_TestItinerary_Titel,
                    Description = AppResources.MenuPage_TestItinerary_Description,
                    Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel,
                    Illustrations = "test/main_QPtxLmSevsRRZEIGbZjPKQUhH5mkrffP.jpeg",
                    Type_parcours = 0,
                    Theme = 4,
                    Langue = "fr-FR",
                    Duree = 120,
                    Payant = 0,
                    Telechargeable = 1,
                    Tarif = 0.00,
                    Tva = 0.20,
                    Currency = "EUR",
                    Nombre_message = 13,
                    Note = 5.0,
                    Nombre_commentaires = 45,
                    Latitude = MyPosition.Latitude,
                    Longitude = MyPosition.Longitude,
                };

                testItinerary.ItineraryPins = await getTestItineraryPins(MyPosition);


                ItineraryEntries.Add(testItinerary);

                await _cache.InsertObject<List<ItineraryEntry>>("ItineraryEntries", ItineraryEntries);
                Application.Current.Properties["ItineraryEntries"] = ItineraryEntries;

                selectItinerary(0);

                await LoadItineraries();
            }
            catch (Exception ex)
            {
            }
        }

        async Task<ItineraryPin[]> getTestItineraryPins(GeoCoords localisation)
        {
            try
            {
                var TestMessage_1 = new MessageEntry();
                TestMessage_1.Clef = 1;
                TestMessage_1.Clef_parcours = 0;
                TestMessage_1.Titre = AppResources.MenuPage_TestItinerary_Example1;
                TestMessage_1.Texte = AppResources.MenuPage_TestItinerary_Description1;
                TestMessage_1.Illustrations = "test/1/main_N2DFBjbjiTGKValUh3Ln3DFuPJSSIWvA.jpg";
                TestMessage_1.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel1;
                TestMessage_1.Payant = 0;

                var TestMessage_2 = new MessageEntry();
                TestMessage_2.Clef = 2;
                TestMessage_2.Clef_parcours = 0;
                TestMessage_2.Titre = AppResources.MenuPage_TestItinerary_Example2;
                TestMessage_2.Texte = AppResources.MenuPage_TestItinerary_Description2;
                TestMessage_2.Illustrations = "test/2/main_ZaqQ9CmRKmbCMmMtfausAxfsb46AjR9y.jpg";
                TestMessage_2.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel2;
                TestMessage_2.Payant = 0;

                var TestMessage_3 = new MessageEntry();
                TestMessage_3.Clef = 3;
                TestMessage_3.Clef_parcours = 0;
                TestMessage_3.Titre = AppResources.MenuPage_TestItinerary_Example3;
                TestMessage_3.Texte = AppResources.MenuPage_TestItinerary_Description3;
                TestMessage_3.Illustrations = "test/3/main_QB2MbhsugkjIs2303IHfLHoXlXAlAucR.jpg";
                TestMessage_3.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel3;
                TestMessage_3.Payant = 0;

                var TestMessage_4 = new MessageEntry();
                TestMessage_4.Clef = 4;
                TestMessage_4.Clef_parcours = 0;
                TestMessage_4.Titre = AppResources.MenuPage_TestItinerary_Example4;
                TestMessage_4.Texte = AppResources.MenuPage_TestItinerary_Description4;
                TestMessage_4.Illustrations = "test/4/main_noYIZeW5F3onw0opo46YvkFQjbznsu2H.jpg";
                TestMessage_4.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel4;
                TestMessage_4.Payant = 0;

                var TestMessage_5 = new MessageEntry();
                TestMessage_5.Clef = 5;
                TestMessage_5.Clef_parcours = 0;
                TestMessage_5.Titre = AppResources.MenuPage_TestItinerary_Example5;
                TestMessage_5.Texte = AppResources.MenuPage_TestItinerary_Description5;
                TestMessage_5.Illustrations = "test/5/main_m8LdsvanEeJ3YyHNr301STZ1XAI4iCVD.jpg";
                TestMessage_5.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel5;
                TestMessage_5.Payant = 0;

                var TestMessage_6 = new MessageEntry();
                TestMessage_6.Clef = 6;
                TestMessage_6.Clef_parcours = 0;
                TestMessage_6.Titre = AppResources.MenuPage_TestItinerary_Example6;
                TestMessage_6.Texte = AppResources.MenuPage_TestItinerary_Description6;
                TestMessage_6.Illustrations = "test/6/main_tmueMOxbJR1jk0Yo2Wtqweh2jpufL9DH.jpg";
                TestMessage_6.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel6;
                TestMessage_6.Payant = 0;

                var TestMessage_7 = new MessageEntry();
                TestMessage_7.Clef = 7;
                TestMessage_7.Clef_parcours = 0;
                TestMessage_7.Titre = AppResources.MenuPage_TestItinerary_Example7;
                TestMessage_7.Texte = AppResources.MenuPage_TestItinerary_Description7;
                TestMessage_7.Illustrations = "test/7/main_X2crD1FXcuueKVanCrBzB6xrcqv25cys.jpg";
                TestMessage_7.Titre_illustrations = AppResources.MenuPage_TestItinerary_IllustrationsTitel7;
                TestMessage_7.Payant = 0;

                await _cache.InsertObject("1", TestMessage_1);
                await _cache.InsertObject("2", TestMessage_2);
                await _cache.InsertObject("3", TestMessage_3);
                await _cache.InsertObject("4", TestMessage_4);
                await _cache.InsertObject("5", TestMessage_5);
                await _cache.InsertObject("6", TestMessage_6);
                await _cache.InsertObject("7", TestMessage_7);

                var testItineraryPins = new ItineraryPin[7];
                var testItineraryLabels = new string[]
                {
                    AppResources.MenuPage_TestItinerary_Example1,
                    AppResources.MenuPage_TestItinerary_Example2,
                    AppResources.MenuPage_TestItinerary_Example3,
                    AppResources.MenuPage_TestItinerary_Example4,
                    AppResources.MenuPage_TestItinerary_Example5,
                    AppResources.MenuPage_TestItinerary_Example6,
                    AppResources.MenuPage_TestItinerary_Example7,
                };


                var examplesPositions = getArrayPosition(localisation);
                examplesPositions[6] = new Position(localisation.Latitude, localisation.Longitude);



                int i = 0;
                foreach (Position examplePosition in examplesPositions)
                {
                    var testItineraryPin = new ItineraryPin();
                    testItineraryPin.Id_Message = (i + 1);
                    testItineraryPin.Pin.Label = testItineraryLabels[i];
                    testItineraryPin.Pin.Position = examplePosition;

                    testItineraryPins[i] = testItineraryPin;
                    i++;
                }

                return testItineraryPins;
            }
            catch (Exception ex)
            {
                return new ItineraryPin[7];
            }
        }


        Position[] getArrayPosition(GeoCoords localisation)
        {
            var arrayPosition = new Position[7];

            arrayPosition[0] = getGPSPosition(localisation.Latitude, localisation.Longitude, 300, 0);
            arrayPosition[1] = getGPSPosition(localisation.Latitude, localisation.Longitude, 150, 150 * Math.Sqrt(3));
            arrayPosition[2] = getGPSPosition(localisation.Latitude, localisation.Longitude, -150, 150 * Math.Sqrt(3));
            arrayPosition[3] = getGPSPosition(localisation.Latitude, localisation.Longitude, -300, 0);
            arrayPosition[4] = getGPSPosition(localisation.Latitude, localisation.Longitude, -150, -150 * Math.Sqrt(3));
            arrayPosition[5] = getGPSPosition(localisation.Latitude, localisation.Longitude, 150, -150 * Math.Sqrt(3));

            return arrayPosition;
        }

        Position getGPSPosition(double pLatitude, double pLongitude, double xDistanceInMeters, double yDistanceInMeters)
        {
            double latRadian = pLatitude * Math.PI / 180;

            double degLatKm = 110.574235;
            double degLongKm = 110.572833 * Math.Cos(latRadian);
            double deltaLat = (xDistanceInMeters / 1000) / degLatKm;
            double deltaLong = (yDistanceInMeters / 1000) / degLongKm;

            double Lat = pLatitude + deltaLat;
            double Long = pLongitude + deltaLong;

            var newPosition = new Position(Lat, Long);

            return newPosition;
        }

    }
}

