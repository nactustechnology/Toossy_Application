using Xamarin.Forms;
using ToossyApp.ViewModels;
using ToossyApp.Resources;
using System;

namespace ToossyApp.Views
{
    public class MenuPage : ContentPage
    {
        MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }

        public MenuPage()
        {
            try
            {

                //Title = AppResources.Menu_PageTitle;

                string imageFolder = "";

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        imageFolder = "";
                        break;
                    case Device.Android:
                        imageFolder = "Resources/drawable/";
                        break;
                }

                //______________Tutorial________________________
                //**********************************************
                var tutorialIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "book_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };


                var tutorialLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_TutorialLabel,
                    Style = Device.Styles.TitleStyle
                };

                var tutorial = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                //tutorial.Children.Add(tutorialIcon, 0, 0);
                //tutorial.Children.Add(tutorialLabel, 1, 0);


                //______________Favoris_________________________
                //**********************************************

                var favorisIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "favorite_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };

                var favorisLabel = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_FavoriteLabel,
                    Style = Device.Styles.TitleStyle
                };

                var favoris = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                favoris.Children.Add(favorisIcon, 0, 0);
                favoris.Children.Add(favorisLabel, 1, 0);

                //______________Facebook_________________________
                //**********************************************

                var facebookIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "facebook_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };

                var facebookLabel = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_FacebookLabel,
                    Style = Device.Styles.TitleStyle
                };

                var facebook = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                facebook.Children.Add(facebookIcon, 0, 0);
                facebook.Children.Add(facebookLabel, 1, 0);

                //______________Twitter_________________________
                //**********************************************

                var twitterIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "twitter_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };

                var twitterLabel = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_TwitterLabel,
                    Style = Device.Styles.TitleStyle
                };

                var twitter = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                twitter.Children.Add(twitterIcon, 0, 0);
                twitter.Children.Add(twitterLabel, 1, 0);

                //______________Mentions Légales_________________________
                //**********************************************

                var mentionsLegalesIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "settings_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };

                var mentionsLegalesLabel = new Label
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_ConditionsAndTermsLabel,
                    Style = Device.Styles.TitleStyle
                };

                var mentionsLegales = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                mentionsLegales.Children.Add(mentionsLegalesIcon, 0, 0);
                mentionsLegales.Children.Add(mentionsLegalesLabel, 1, 0);

                //______________Empty cache________________________
                //**********************************************
                var memoryIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "trash_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };


                var memoryLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_EmptyMemory,
                    Style = Device.Styles.TitleStyle
                };

                var flushMemory = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                flushMemory.Children.Add(memoryIcon, 0, 0);
                flushMemory.Children.Add(memoryLabel, 1, 0);

                //______________Generate test Itinerary________________________
                //**********************************************

                var testItineraryIcon = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Margin = 5,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "test_icon.png"),
                    HeightRequest = 48,
                    WidthRequest = 48
                };


                var testItineraryLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = AppResources.MenuPage_TestItinerary,
                    Style = Device.Styles.TitleStyle
                };

                var testItinerary = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto}
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                testItinerary.Children.Add(testItineraryIcon, 0, 0);
                testItinerary.Children.Add(testItineraryLabel, 1, 0);


                //**********************************************

                var mainLayout = new StackLayout
                {
                    Children = {
                        tutorial,
                        favoris,
                        twitter,
                        facebook,
                        mentionsLegales,
                        flushMemory,
                        testItinerary
                    },
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical
                };



                var tutorialOnTapped = new TapGestureRecognizer();
                tutorialOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.GoToTutorialPageCommand();
                };

                tutorial.GestureRecognizers.Add(tutorialOnTapped);

                var favorisOnTapped = new TapGestureRecognizer();
                favorisOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.GoToFavoriteListPageCommand();
                };

                favoris.GestureRecognizers.Add(favorisOnTapped);

                var twitterOnTapped = new TapGestureRecognizer();
                twitterOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.goOnTwitter();
                };

                twitter.GestureRecognizers.Add(twitterOnTapped);

                var facebookOnTapped = new TapGestureRecognizer();
                facebookOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.goOnFacebook();
                };

                facebook.GestureRecognizers.Add(facebookOnTapped);

                var mentionsLegalesOnTapped = new TapGestureRecognizer();
                mentionsLegalesOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.GoToMentionsLegalesPageCommand();
                };

                mentionsLegales.GestureRecognizers.Add(mentionsLegalesOnTapped);

                var flushMemoryOnTapped = new TapGestureRecognizer();
                flushMemoryOnTapped.Tapped += async (s, e) =>
                {
                    await _vm.flushMemory();

                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.ItineraryMenu_MemoryFlushed);
                    Device.BeginInvokeOnMainThread(() => {

                    });
                };

                flushMemory.GestureRecognizers.Add(flushMemoryOnTapped);

                var setTestItineraryOnTapped = new TapGestureRecognizer();
                setTestItineraryOnTapped.Tapped += async (s, e) =>
                {
                    var result = false;
                    string msgToDisplay = AppResources.ItineraryMenu_TestItineraryUnset;

                    result = await _vm.setUnsetTestItinerary();

                    if (result)
                    {
                        msgToDisplay = AppResources.ItineraryMenu_TestItinerarySet;
                    }

                    DisplayMessage(AppResources.MessageMap_InformationLabel, msgToDisplay);
                };

                testItinerary.GestureRecognizers.Add(setTestItineraryOnTapped);

                Content = mainLayout;
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