using ToossyApp.Models;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;
using ToossyApp.Services;

namespace ToossyApp.Views
{
    public class ItineraryPreviewPage : ContentPage
    {
        ItineraryPreviewViewModel _vm
        {
            get { return BindingContext as ItineraryPreviewViewModel; }
        }

        readonly WebView _map;

        public ItineraryPreviewPage()
        {
            BindingContextChanged += (sender, args) =>
            {
                if (_vm == null)
                    return;

                _vm.PropertyChanged += (s, e) =>
                {

                    if (e.PropertyName == "ItineraryBarycenter" && 2<1)
                    {
                        string markers = "";

                        foreach (ItineraryPin pin in _vm.ItineraryPins)
                        {
                            string labelQuoted = String.Format("<b>{0}</b>", pin.Pin.Label).Replace("\"", "\\\"");
                            markers += "var newMarker = L.marker([" + pin.Pin.Position.Latitude.ToString().Replace(',', '.') + ", " + pin.Pin.Position.Longitude.ToString().Replace(',', '.') + "]).addTo(mymap).bindPopup(\"" + labelQuoted + "\"); group.addLayer(newMarker); ";
                        }


                        string LeafletContent =
                            "<div id = \"mapid\" style = \"width: 100%; height: 100%; position: relative;\" ></div>" +

                            "<script type = \"application/javascript\" src = \"leaflet/leaflet.js\"></script>" +
                            "<script type = \"application/javascript\" src = \"stamen/tile.stamen.js\"></script>" +

                            "<script type = \"application/javascript\" >" +
                            "var mymap = L.map('mapid').setView([" + _vm.ItineraryBarycenter.Latitude.ToString().Replace(',', '.') + ", " + _vm.ItineraryBarycenter.Longitude.ToString().Replace(',', '.') + "], 13);" +
                            "var group = new L.featureGroup().addTo(mymap);" +
                            markers +
                            "L.tileLayer('https://stamen-tiles-{s}.a.ssl.fastly.net/terrain/{z}/{x}/{y}.{ext}', {" +
                                    "attribution: 'Map tiles by <a href=\"http://stamen.com\">Stamen Design</a>, <a href=\"http://creativecommons.org/licenses/by/3.0\">CC BY 3.0</a> &mdash; Map data &copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>'," +
                                    "subdomains: 'abcd'," +
                                    "useCache: true," +
                                    "minZoom: 11," +
                                    "maxZoom: 16," +
                                    "ext: 'png'" +
                                "}).addTo(mymap);" +

                        "mymap.fitBounds(group.getBounds());" +
                            "</script>";

                        _map.Source = new HtmlWebViewSource()
                        {
                            Html = "<html>" +
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
                                "</html>",
                            //BaseUrl = _vm.BaseUrl
                        };

                    }


                };
            };

            Title = AppResources.ItineraryPreview_PageTitle;

            _map = new WebView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Style = Device.Styles.BodyStyle,

            };



            var mainLayout = new Grid
            {
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength (1, GridUnitType.Star)
                    }
                }
            };

            mainLayout.Children.Add(_map);

            Content = _map;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Initialized ItineraryListViewModel
            if (_vm != null)
                await _vm.Init().ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
