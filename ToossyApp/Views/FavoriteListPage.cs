using ToossyApp.Converters;
using ToossyApp.Models;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class FavoriteListPage : ContentPage
    {
        FavoriteListViewModel _vm
        {
            get { return BindingContext as FavoriteListViewModel;  }
        }

        public FavoriteListPage()
        {
            try
            {
                Title = AppResources.FavoriteList_TitlePage;
                var typeOfActivity = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, Text = AppResources.FavoriteList_LoadingMsg };


                var itemTemplate = new DataTemplate(() => {

                    var illustrationImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        MinimumHeightRequest = 250
                    };
                    illustrationImage.SetBinding(Image.SourceProperty, "Illustrations", converter: new IllustrationConverterThumb());

                    var titreLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontAttributes = FontAttributes.Bold,
                        VerticalTextAlignment = TextAlignment.Center,
                        Style = Device.Styles.ListItemTextStyle
                    };
                    titreLabel.SetBinding(Label.TextProperty, "Titre");

                    var grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (3, GridUnitType.Star) },
                        }
                    };

                    grid.Children.Add(illustrationImage, 0, 0);
                    grid.Children.Add(titreLabel, 1, 0);

                    return new ViewCell { View = grid };
                });

                var entries = new ListView
                {
                    IsPullToRefreshEnabled = false,
                    SeparatorVisibility = SeparatorVisibility.Default,
                    SeparatorColor = Color.FromHex("#a61f21"),
                    ItemTemplate = itemTemplate,
                    BackgroundColor = Color.White
                };

                entries.SetBinding(ListView.ItemsSourceProperty, "FavoriteEntries");
               
                entries.ItemTapped += (sender, e) =>
                {
                    var item = (MessageEntry)e.Item;
                    _vm.GoToFavoriteDetailPage.Execute(item);
                };


                var loading = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        typeOfActivity
                    }
                };

                entries.SetBinding(ListView.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());
                loading.SetBinding(StackLayout.IsVisibleProperty, "Isloading");

                var mainLayout = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { loading, entries }
                };

                Content = mainLayout;
            }
            catch(Exception ex)
            {

            }
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                await _vm.Init();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
