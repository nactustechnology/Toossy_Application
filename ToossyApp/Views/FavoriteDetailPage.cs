
using ToossyApp.Converters;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class FavoriteDetailPage : ContentPage
    {
        FavoriteDetailViewModel _vm
        {
            get { return BindingContext as FavoriteDetailViewModel; }
        }

        ToolbarItem FavoriteRemoverButton = new ToolbarItem() { Text = "Retirer des favoris" };

        public FavoriteDetailPage()
        {
            try
            {
                Title = AppResources.FavoriteDetail_TitlePage;

                FavoriteRemoverButton.Clicked += async (sender, eventArgs) =>
                {
                    await _vm.removeFromFavorites();

                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.FavoriteDetail_MsgRemoved);
                };


                if (!ToolbarItems.Contains(FavoriteRemoverButton))
                    ToolbarItems.Add(FavoriteRemoverButton);

                var Titre = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Style = Device.Styles.TitleStyle
                };
                Titre.SetBinding(Label.TextProperty, "FavoriteEntry.Titre", converter: new TitreConverter());

                var Illustration = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = 15,
                    IsVisible = false
                };
                Illustration.SetBinding(Image.SourceProperty, "MessageEntry.Illustrations", converter: new IllustrationConverterMain());

                Illustration.SetBinding(Image.IsLoadingProperty, "ImageIsLoading", BindingMode.OneWayToSource);
                Illustration.SetBinding(Image.IsVisibleProperty, "ImageIsLoading", BindingMode.OneWay, converter: new ReverseBooleanConverter());

                var imageLoading = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        new Label { Text = AppResources.MessageDetail_LoadingMsg, HorizontalTextAlignment = TextAlignment.Center }
                    },
                    IsVisible = true
                };
                imageLoading.SetBinding(StackLayout.IsVisibleProperty, "ImageIsLoading", BindingMode.OneWay);

                var imageLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { imageLoading, Illustration }
                };

                Illustration.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "IsLoading")
                    {
                        if (Illustration.IsLoading.Equals(true))
                        {
                            _vm.IsLoading = true;
                        }
                        else
                        {
                            _vm.IsLoading = false;
                        }
                    }

                };

                Illustration.SetBinding(Image.SourceProperty, "FavoriteEntry.Illustrations", converter: new IllustrationConverterMain());

                var Titre_illustration = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.CaptionStyle
                };
                Titre_illustration.SetBinding(Label.TextProperty, "FavoriteEntry.Titre_illustrations");


                var Description = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                Description.SetBinding(WebView.SourceProperty, "FavoriteEntry.Texte", converter: new DescriptionConverter());

                var DescriptionHeight = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle
                };
                DescriptionHeight.SetBinding(Label.TextProperty, "FavoriteEntry.Texte", converter: new DescriptionHeightConverter());

                var DescriptionRow = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var DescriptionHeightRow = new RowDefinition { Height = GridLength.Auto };

                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        DescriptionRow,
                        DescriptionHeightRow
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                    }
                };


                grid.SetBinding(StackLayout.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());

                grid.Children.Add(Titre, 0, 0);
                Grid.SetColumnSpan(Titre, 2);

                grid.Children.Add(imageLayout, 0, 1);
                Grid.SetColumnSpan(imageLayout, 2);

                grid.Children.Add(Titre_illustration, 0, 2);
                Grid.SetColumnSpan(Titre_illustration, 2);

                grid.Children.Add(Description, 0, 3);
                Grid.SetColumnSpan(Description, 2);

                grid.Children.Add(DescriptionHeight, 0, 4);
                Grid.SetColumnSpan(DescriptionHeight, 2);

                var loading = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        new Label { Text = AppResources.MessageDetail_LoadingMsg, HorizontalTextAlignment = TextAlignment.Center }
                    }
                };

                loading.BindingContext = Illustration;
                loading.SetBinding(StackLayout.IsVisibleProperty, "IsLoading");

                double heightMax = 0;

                loading.LayoutChanged += (sender, e) =>
                {
                    if (grid.Children.Contains(DescriptionHeight) && DescriptionHeight.Height > heightMax)
                    {
                        heightMax = DescriptionHeight.Height * 1.05;
                        DescriptionRow.Height = new GridLength(heightMax, GridUnitType.Absolute);
                        DescriptionHeightRow.Height = new GridLength(0, GridUnitType.Absolute);
                    }
                };

                var mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { loading, grid }
                };


                Content = new ScrollView()
                {
                    Content = mainLayout,
                    Orientation = ScrollOrientation.Vertical
                };
            }
            catch (Exception ex)
            {

            }
        }

        void DisplayMessage(string msgType, string msg)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await DisplayAlert(msgType, msg, AppResources.ItineraryList_okLabel);
            });
        }
    }
}
