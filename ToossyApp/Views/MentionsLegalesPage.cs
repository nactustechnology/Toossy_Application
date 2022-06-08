
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class MentionsLegalesPage : ContentPage
    {
        MentionsLegalesViewModel _vm
        {
            get { return BindingContext as MentionsLegalesViewModel; }
        }

        public MentionsLegalesPage()
        {
            Title = AppResources.MentionsLegalesPage_PageTitle;

            var mentionsText = new WebView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Style = Device.Styles.BodyStyle,
                Source = new HtmlWebViewSource()
                {
                    Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                    String.Format("<p>{0}</p>", AppResources.MentionsLegalesPage_text) +
                                "</body>" +
                            "</html>"
                }
            };

            /*var goToCGU = new Button
            {
                Text = AppResources.MentionsLegalesPage_link,
            };

            goToCGU.Clicked += async (sender, args) =>
            {
                await _vm.goToCGU();
            };*/

            var goToCGU = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = Device.Styles.SubtitleStyle,
                Text = AppResources.MentionsLegalesPage_link,
                TextColor = Color.DeepSkyBlue,
            };

            var goToCGUTapGestureRecognizer = new TapGestureRecognizer();
            goToCGUTapGestureRecognizer.Tapped += async (s, e) => {
                await _vm.goToCGU();
            };

            goToCGU.GestureRecognizers.Add(goToCGUTapGestureRecognizer);

            var mainLayout = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(100 ,GridUnitType.Absolute )},
                    new RowDefinition{ Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            mainLayout.Children.Add(mentionsText, 0, 0);
            mainLayout.Children.Add(goToCGU, 0, 1);

            Content = new ScrollView()
            {
                Content = mainLayout,
                Orientation = ScrollOrientation.Vertical
            };
        }

    }
}
