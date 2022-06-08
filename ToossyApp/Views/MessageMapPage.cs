using Akavache;
using ToossyApp.Models;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reactive.Linq;
using XLabs.Forms.Controls;


namespace ToossyApp.Views
{
    public class MessageMapPage : ContentPage
    {
        MessageMapViewModel _vm
        {
            get { return BindingContext as MessageMapViewModel; }
        }

        readonly HybridWebView _map;
        ProgressBar progressBar = new ProgressBar { Progress = 0, IsVisible = false };
        ToolbarItem MessagesLoaderButton = new ToolbarItem() { Icon = "download_icon.png" };
        ToolbarItem OfflineMode = new ToolbarItem() { Icon = "connectivity_on_icon.png" };
        string mapZoom;

        public MessageMapPage()
        {
            try
            {
                //var rootFolderPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var rootFolderPath = "";
                var offlineUrlTemplate = "'file:" + rootFolderPath + "/toossy_map/{z}/{x}/{y}.png'";
                //var onlineUrlTemplate = "'https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}'";

                progressBar.SetBinding(ProgressBar.ProgressProperty, "Progress");

                BindingContextChanged += (sender, args) =>
                {
                    if (_vm == null)
                        return;

                    _vm.PropertyChanged += async (s, e) =>
                    {
                        if (e.PropertyName == "MessagePins")
                        {
                            string markers = "";
                            string latLngBounds = "";

                            foreach (MessagePin pin in _vm.MessagePins)
                            {
                                string labelQuoted = String.Format("<b>{0}</b>", pin.Pin.Label).Replace("\"", "\\\"");

                                if (pin.ViewStatus == 2)
                                {
                                    markers += "var Marker" + pin.Id_Message + " = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "], {zIndexOffset: 0, icon: IconOk}).on(\"click\",function (){ Native(\"GoToMessage\", " + pin.Id_Message + ") } ).addTo(mymap);";
                                }
                                else if (pin.ViewStatus == 1)
                                {
                                    if (pin.IsAvailable)
                                    {
                                        markers += "var Marker" + pin.Id_Message + " = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "], {zIndexOffset: 2, icon: IconPlay}).addTo(mymap).bindPopup(\"" + labelQuoted + "\");";
                                    }
                                    else if (pin.Id_Next_Message == 0)
                                    {
                                        markers += "var Marker" + pin.Id_Message + " = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "], {zIndexOffset: 3, icon: IconCanGoDarkBlue}).addTo(mymap).bindPopup(\"" + labelQuoted + "\");";
                                    }
                                    else if (pin.Id_Next_Message != 0)
                                    {
                                        markers += "var Marker" + pin.Id_Message + " = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "], {zIndexOffset: 4, icon: IconCanGoLightBlue}).addTo(mymap).bindPopup(\"" + labelQuoted + "\");";
                                    }
                                }
                                else if (pin.ViewStatus == 0)
                                {
                                    markers += "var Marker" + pin.Id_Message + " = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "], {zIndexOffset: 1, icon: IconCanNotGo}).addTo(mymap).bindPopup(\"" + labelQuoted + "\");";
                                }

                                latLngBounds += "[" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "],";
                            }

                            latLngBounds += "[" + _vm.MyPosition.Latitude.ToString().Replace(',', '.') + ", " + _vm.MyPosition.Longitude.ToString().Replace(',', '.') + "]";

                            string flyToBounds = "";

                            if (Application.Current.Properties.ContainsKey("firstDisplay") && (bool)Application.Current.Properties["firstDisplay"] == false)
                            {
                                mapZoom = (string)Application.Current.Properties["mapZoom"];
                            }
                            else
                            {
                                Application.Current.Properties["firstDisplay"] = false;

                                flyToBounds = "mymap.flyToBounds([" + latLngBounds + "]);";
                                mapZoom = "13";
                            }

                            string tileLayerType;

                            if (_vm.IsOffline)
                            {
                                tileLayerType = "var myLayer = L.tileLayer(" + offlineUrlTemplate + ", { attribution: 'Map tiles by <a href=\"http://stamen.com\">Stamen Design</a>, <a href=\"http://creativecommons.org/licenses/by/3.0\">CC BY 3.0</a> &mdash; Map data &copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>', bounds: myBounds, useCache: false, maxZoom: 16, minZoom: 11}).addTo(mymap);";
                            }
                            else
                            {
                                //tileLayerType = "var myLayer = L.tileLayer(" + onlineUrlTemplate + ", { attribution: 'Map data &copy; <a href=\"http://openstreetmap.org\">OpenStreetMap</a> contributors, <a href=\"http://creativecommons.org/licenses/by-sa/2.0/\">CC-BY-SA</a>, Imagery © <a href=\"http://mapbox.com\">Mapbox</a>', bounds: myBounds, useCache: true, maxZoom: 18, minZoom: 11, id: 'mapbox.streets', accessToken: 'pk.eyJ1IjoiamVhbi1mbG9yZW50IiwiYSI6ImNqOGU1eTNiaTBuZG8ycW1uMDZsejN1aDQifQ.PUsj98HO5-De99ZxCY6gJg'}).addTo(mymap);";
                                tileLayerType = "var myLayer = L.tileLayer('https://stamen-tiles-{s}.a.ssl.fastly.net/terrain/{z}/{x}/{y}.{ext}', {" +
                                    "attribution: 'Map tiles by <a href=\"http://stamen.com\">Stamen Design</a>, <a href=\"http://creativecommons.org/licenses/by/3.0\">CC BY 3.0</a> &mdash; Map data &copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>'," +
                                    "subdomains: 'abcd'," +
                                    "bounds: myBounds," +
                                    "useCache: true," +
                                    "minZoom: 11," +
                                    "maxZoom: 18," +
                                    "ext: 'png'" +
                                "}).addTo(mymap);";

                            }

                            string LeafletContent =
                                "<div id = \"mapid\" style = \"width: 100%; height: 100%; position: relative;\" ></div>" +

                                /*"<script src = \"https://unpkg.com/leaflet@^1.0.0/dist/leaflet-src.js\"></script>" +
                                "<script src = \"https://unpkg.com/pouchdb@^5.2.0/dist/pouchdb.js\"></script>" +
                                "<script src = \"https://unpkg.com/leaflet.tilelayer.pouchdbcached@latest/L.TileLayer.PouchDBCached.js\" ></script>" +
                                "<script type = \"text/javascript\" src = \"https://stamen-maps.a.ssl.fastly.net/js/tile.stamen.js\"></script>" +*/


                                "<script type = \"application/javascript\" src = \"leaflet/leaflet.js\"></script>" +
                                "<script type = \"application/javascript\" src = \"stamen/tile.stamen.js\"></script>" +
                                //"<script type = \"application/javascript\" src = \"leaflet/pouchdb.js\"></script>" +
                                //"<script type = \"application/javascript\" src = \"leaflet/LTileLayerPouchDBCached.js\"></script>" +

                                "<script type = \"application/javascript\" >" +
                                "var mymap = L.map('mapid').setView([" + _vm.MyPosition.Latitude.ToString().Replace(',', '.') + ", " + _vm.MyPosition.Longitude.ToString().Replace(',', '.') + "], " + mapZoom + ").on(\"zoomend\",function (){ Native(\"SetMapZoom\", mymap.getZoom().toString() ) } );" +
                                "var group = new L.featureGroup().addTo(mymap);" +
                                "var myBounds = new L.latLngBounds([" + _vm.BoundP1.Latitude.ToString().Replace(',', '.') + ", " + _vm.BoundP1.Longitude.ToString().Replace(',', '.') + "],[" + _vm.BoundP2.Latitude.ToString().Replace(',', '.') + ", " + _vm.BoundP2.Longitude.ToString().Replace(',', '.') + "]); " +
                                "var IconOk = L.icon({ iconUrl: 'map_marker_OK.png', iconSize: [40,64], iconAnchor: [20,64]});" +
                                "var IconPlay = L.icon({ iconUrl: 'map_marker_Play.png', iconSize: [160,256], iconAnchor: [80,256]});" +
                                "var IconCanGoDarkBlue = L.icon({ iconUrl: 'map_marker_CanGo_dark_blue.png', iconSize: [40,64], iconAnchor: [20,64]});" +
                                "var IconCanGoLightBlue = L.icon({ iconUrl: 'map_marker_CanGo_blue.png', iconSize: [40,64], iconAnchor: [20,64]});" +
                                "var IconCanNotGo = L.icon({ iconUrl: 'map_marker_CanNotGo.png', iconSize: [40,64], iconAnchor: [20,64]});" +
                                markers +
                                "var myPosition = L.circleMarker([" + _vm.MyPosition.Latitude.ToString().Replace(',', '.') + ", " + _vm.MyPosition.Longitude.ToString().Replace(',', '.') + "], { color: 'red', fillColor: '#f03', fillOpacity: 0.5, radius: 15}).addTo(mymap); " +
                                tileLayerType +
                                flyToBounds +
                                "</script>";


                            string Html = "<html>" +
                                            "<head>" +
                                                "<link rel= \"stylesheet\" href = \"leaflet/leaflet.css\"/>" +
                                                "<style media=\"screen\" type=\"text/css\">" +
                                                    "body { padding: 0; margin: 0; }" +
                                                    "html, body, #map { height: 100%; width: 100vw; }" +
                                                "</style>" +
                                            "</head>" +
                                            "<body>" +
                                                String.Format("<p>{0}</p>", LeafletContent) +
                                            "</body>" +
                                        "</html>";

                            _map.LoadContent(Html);

                        }


                        if (e.PropertyName == "MyPosition")
                        {
                            if (_vm.MessagePins != null && _vm.MessagePins.Count != 0)
                            {
                                _map.InjectJavaScript("myPosition.setLatLng([" + _vm.MyPosition.Latitude.ToString().Replace(',', '.') + ", " + _vm.MyPosition.Longitude.ToString().Replace(',', '.') + "]);");

                                await SetMessagesStatus().ConfigureAwait(continueOnCapturedContext: false);
                            }
                        }

                        if (e.PropertyName == "Progress")
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await progressBar.ProgressTo(_vm.Progress, 250, Easing.Linear);
                            }
                            );
                        }

                        if (e.PropertyName == "IsOffline")
                        {
                            if (_vm.IsOffline)
                            {
                                OfflineMode.Icon = "connectivity_off_icon.png";
                                _map.InjectJavaScript("myLayer.remove();");
                                _map.InjectJavaScript("myLayer = L.tileLayer(" + offlineUrlTemplate + ", { attribution: 'Map tiles by <a href=\"http://stamen.com\">Stamen Design</a>, <a href=\"http://creativecommons.org/licenses/by/3.0\">CC BY 3.0</a> &mdash; Map data &copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>', bounds: myBounds, useCache: false, maxZoom: 16, minZoom: 11}).addTo(mymap);");
                            }
                            else
                            {
                                OfflineMode.Icon = "connectivity_on_icon.png";
                                _map.InjectJavaScript("myLayer.remove();");
                                //_map.InjectJavaScript("myLayer = L.tileLayer(" + onlineUrlTemplate + ", { attribution: 'Map data &copy; <a href=\"http://openstreetmap.org\">OpenStreetMap</a> contributors, <a href=\"http://creativecommons.org/licenses/by-sa/2.0/\">CC-BY-SA</a>, Imagery © <a href=\"http://mapbox.com\">Mapbox</a>', bounds: myBounds, useCache: true, maxZoom: 18, minZoom: 11, id: 'mapbox.streets', accessToken: 'pk.eyJ1IjoiamVhbi1mbG9yZW50IiwiYSI6ImNqOGU1eTNiaTBuZG8ycW1uMDZsejN1aDQifQ.PUsj98HO5-De99ZxCY6gJg'}).addTo(mymap);");
                                _map.InjectJavaScript("myLayer = L.tileLayer('https://stamen-tiles-{s}.a.ssl.fastly.net/terrain/{z}/{x}/{y}.{ext}', {" +
                                    "attribution: 'Map tiles by <a href=\"http://stamen.com\">Stamen Design</a>, <a href=\"http://creativecommons.org/licenses/by/3.0\">CC BY 3.0</a> &mdash; Map data &copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>'," +
                                    "subdomains: 'abcd'," +
                                    "bounds: myBounds," +
                                    "useCache: true," +
                                    "minZoom: 11," +
                                    "maxZoom: 18," +
                                    "ext: 'png'" +
                                "}).addTo(mymap);");
                            }
                        }
                    };
                };

                //Title = AppResources.MessageMap_PageTitle;

                MessagesLoaderButton.Clicked += async (sender, eventArgs) =>
                {
                    var messagesToBePreLoaded = _vm.MessagePins;
                    int messagesCount = messagesToBePreLoaded.Count;

                    if (messagesCount != 0)
                    {

                        await LunchMessagesPreloading(messagesToBePreLoaded);


                        lunchMapPreloading();
                    }
                    else
                    {
                        await DisplayMessage(AppResources.MessageMap_WarningLabel, AppResources.MessageMap_noneItinerarySelected);
                    }
                };

                ToolbarItems.Add(MessagesLoaderButton);

                OfflineMode.Clicked += (sender, eventArgs) =>
                {
                    _vm.IsOffline = !_vm.IsOffline;
                };

                ToolbarItems.Add(OfflineMode);

                var serializer = new XLabs.Serialization.JsonNET.JsonSerializer();

                _map = new HybridWebView(serializer)
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                };

                _map.RegisterCallback("GoToMessage", GoToMessage);
                _map.RegisterCallback("SetMapZoom", SetMapZoom);

                var mainLayout = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition
                        {
                            Height = new GridLength(1, GridUnitType.Star)
                        }
                    }
                };

                mainLayout.Children.Add(_map);
                mainLayout.Children.Add(progressBar);

                Content = mainLayout;
            }
            catch (Exception ex)
            {
            }
        }

        async Task LunchMessagesPreloading(IList<MessagePin> messagesToBePreLoaded)
        {
            try
            {
                progressBar.IsVisible = false;

                _vm.Progress = 0;

                progressBar.IsVisible = true;

                double progressStep = Convert.ToDouble(1) / Convert.ToDouble(messagesToBePreLoaded.Count);

                foreach (MessagePin message in messagesToBePreLoaded)
                {
                    await _vm.MessagePreLoading(message.Id_Message, progressStep).ConfigureAwait(continueOnCapturedContext: false);
                }


                //progressBar.IsVisible = false;

                if (1 - _vm.Progress < progressStep)
                {
                    await DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.MessageMap_msgPreloadingSuccess);
                }
                else
                {
                    await DisplayMessage(AppResources.MessageMap_WarningLabel, AppResources.MessageMap_msgPreloadingFail);
                }

            }
            catch (Exception ex)
            {
                await DisplayMessage(AppResources.MessageMap_WarningLabel, AppResources.MessageMap_msgPreloadingFail);
            }
        }

        async void lunchMapPreloading()
        {
            try
            {
                progressBar.IsVisible = false;

                _vm.Progress = 0;

                progressBar.IsVisible = true;

                var progressStep = await _vm.saveMap().ConfigureAwait(continueOnCapturedContext: false);

                Device.BeginInvokeOnMainThread(() =>
                {
                    progressBar.IsVisible = false;
                }
                );



                var finalSumDelta = Convert.ToDouble(1) - _vm.Progress;

                if (finalSumDelta < progressStep)
                {
                    await DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.MessageMap_mapPreloadingSuccess);
                }
                else
                {
                    await DisplayMessage(AppResources.MessageMap_WarningLabel, AppResources.MessageMap_mapPreloadingFail);
                }
            }
            catch (Exception ex)
            {
                await DisplayMessage(AppResources.MessageMap_WarningLabel, AppResources.MessageMap_mapPreloadingFail);
            }
        }

        public async Task SetMessagesStatus()
        {
            try
            {
                var currentPosition = _vm.MyPosition;

                if (_vm.MessagePins == null)
                    return;

                var viewableMessagesList = _vm.MessagePins;

                int distanceLimit = 50;

#if DEBUG
                distanceLimit = 200;
#endif

                /*if (viewableMessagesList == null)
                    return;*/

                foreach (MessagePin messagePin in viewableMessagesList)
                {
                    var messagePosition = messagePin.Pin.Position;

                    var distance = _vm.distanceBetweenTwoLocations(currentPosition.Latitude, currentPosition.Longitude, messagePosition.Latitude, messagePosition.Longitude) * 1000;

                    if (messagePin.ViewStatus == 1 && distance <= distanceLimit)
                    {

                        if (messagePin.IsAvailable == false)
                        {
                            var v = CrossVibrate.Current;
                            v.Vibration();
                        }


                        messagePin.IsAvailable = true;

                        _map.InjectJavaScript("Marker" + messagePin.Id_Message + ".setIcon(IconPlay).setZIndexOffset(4).off(\"click\").on(\"click\",function (){ Native(\"GoToMessage\", " + messagePin.Id_Message + ") } );");
                        _map.InjectJavaScript("mymap.flyTo([" + _vm.MyPosition.Latitude.ToString().Replace(',', '.') + ", " + _vm.MyPosition.Longitude.ToString().Replace(',', '.') + "], " + mapZoom + ");");
                    }
                    else if (messagePin.ViewStatus == 1 && distance > distanceLimit)
                    {
                        messagePin.IsAvailable = false;

                        string labelQuoted = String.Format("<b>{0}</b>", messagePin.Pin.Label).Replace("\"", "\\\"");

                        if (messagePin.Id_Next_Message == 0)
                        {
                            _map.InjectJavaScript("Marker" + messagePin.Id_Message + ".setIcon(IconCanGoDarkBlue).setZIndexOffset(2).bindPopup(\"" + labelQuoted + "\")");
                        }
                        else
                        {
                            _map.InjectJavaScript("Marker" + messagePin.Id_Message + ".setIcon(IconCanGoLightBlue).setZIndexOffset(3).bindPopup(\"" + labelQuoted + "\")");
                        }
                    }
                    else if (messagePin.ViewStatus == 2 && messagePin.IsAvailable == true)
                    {
                        messagePin.IsAvailable = false;

                        _map.InjectJavaScript("Marker" + messagePin.Id_Message + ".setIcon(IconOk).setZIndexOffset(0).on(\"click\",function (){ Native(\"GoToMessage\", " + messagePin.Id_Message + ") } )");
                    }
                }



            }
            catch (Exception ex)
            {

            }
        }

        public void GoToMessage(string msgId)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {

                    var messagePin = _vm.MessagePins.Find(pin => pin.Id_Message == Int32.Parse(msgId));



                    if (messagePin.Id_Next_Message != 0)
                    {
                        var nextMessage = _vm.MessagePins.Find(pin => pin.Id_Message == messagePin.Id_Next_Message);

                        if (nextMessage.ViewStatus == 0)
                            nextMessage.ViewStatus = 1;
                    }

                    if ((Application.Current.Properties.ContainsKey("MessageAlreadyCalled").Equals(true) && (bool)Application.Current.Properties["MessageAlreadyCalled"] == false) || Application.Current.Properties.ContainsKey("MessageAlreadyCalled").Equals(false))
                    {
                        messagePin.ViewStatus = 2;
                        Application.Current.Properties["MessageAlreadyCalled"] = true;
                        _vm.GoToMessageDetailPage.Execute(messagePin);

                    }
                });

            }
            catch (Exception ex)
            {

            }
        }

        void SetMapZoom(string zoom)
        {
            try
            {
                mapZoom = zoom;
            }
            catch (Exception ex)
            {
            }
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await _vm.StartListening().ConfigureAwait(continueOnCapturedContext: false);

                if (Application.Current.Properties.ContainsKey("IsOffline"))
                {
                    _vm.IsOffline = (bool)Application.Current.Properties["IsOffline"];
                }



                if (Application.Current.Properties.ContainsKey("firstDisplay") && (bool)Application.Current.Properties["firstDisplay"] == false)
                {
                    await SetMessagesStatus();
                }
                else
                {
                    //Application.Current.Properties["firstDisplay"] = false;
                }

                await _vm.checkRating();
            }
            catch (Exception ex)
            {
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await _vm.StopListening().ConfigureAwait(continueOnCapturedContext: false);

            Application.Current.Properties["ViewedMessages"] = _vm.ViewedMessages;

            await _vm._cache.InsertObject("ViewedMessages", Application.Current.Properties["ViewedMessages"]);
            await _vm._cache.InsertObject("RatedItineraries", Application.Current.Properties["RatedItineraries"]);
            await _vm._cache.InsertObject("IsOffline", _vm.IsOffline);
            Application.Current.Properties["IsOffline"] = _vm.IsOffline;
            Application.Current.Properties["mapZoom"] = mapZoom;
        }

        async Task DisplayMessage(string msgType, string msg)
        {
            Device.BeginInvokeOnMainThread(async () => {

                await DisplayAlert(msgType, msg, AppResources.ItineraryList_okLabel);
            });
        }
    }
}
