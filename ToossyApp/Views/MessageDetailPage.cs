using Xamarin.Forms;
using ToossyApp.ViewModels;
using ToossyApp.Converters;
using ToossyApp.Resources;
using System;
using System.Collections.Generic;
using ToossyApp.Models;

namespace ToossyApp.Views
{
    public class MessageDetailPage : ContentPage
    {
        MessageDetailViewModel _vm
        {
            get { return BindingContext as MessageDetailViewModel; }
        }

        ToolbarItem MessagesSaverButton = new ToolbarItem() { Text = "Sauvegarder" };

        public MessageDetailPage()
        {
            try
            {
                MessagesSaverButton.Clicked += async (sender, eventArgs) =>
                {
                    await _vm.saveMessageToFavorites();

                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.MessageDetail_MsgAdded);
                };

                BindingContextChanged += (sender, args) =>
                {
                    _vm.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "IsLoading")
                        {
                            if (_vm.IsTelechargeable && !ToolbarItems.Contains(MessagesSaverButton))
                                ToolbarItems.Add(MessagesSaverButton);
                        }
                    };
                };

                var Titre = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Style = Device.Styles.TitleStyle
                };
                Titre.SetBinding(Label.TextProperty, "MessageEntry.Titre", converter: new TitreConverter());



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

                var Titre_illustration = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.CaptionStyle
                };
                Titre_illustration.SetBinding(Label.TextProperty, "MessageEntry.Titre_illustrations");


                var Description = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                Description.SetBinding(WebView.SourceProperty, "MessageEntry.Texte", converter: new DescriptionConverter());

                var DescriptionHeight = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle
                };

                DescriptionHeight.SetBinding(Label.TextProperty, "MessageEntry.Texte", converter: new DescriptionHeightConverter());

                var reportButton = new Button()
                {
                    BackgroundColor = Color.OrangeRed,
                    BorderColor = Color.Black,
                    Text = AppResources.ReportPage_ReportLabel
                };
                reportButton.SetBinding(Button.CommandProperty, "GoToReportPage");
                reportButton.SetBinding(Button.CommandParameterProperty, "MessageEntry.Clef");

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
                        DescriptionHeightRow,
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                    },
                    IsVisible = false
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

                grid.Children.Add(reportButton, 0, 5);
                Grid.SetColumnSpan(reportButton, 2);

                double heightMax = 0;

                grid.LayoutChanged += (sender, e) =>
                {
                    if (grid.Children.Contains(DescriptionHeight) && DescriptionHeight.Height > heightMax)
                    {
                        heightMax = DescriptionHeight.Height * 1.15;
                        DescriptionRow.Height = new GridLength(heightMax, GridUnitType.Absolute);
                        DescriptionHeightRow.Height = new GridLength(0, GridUnitType.Absolute);
                    }
                };

                var loading = new StackLayout
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

                loading.SetBinding(StackLayout.IsVisibleProperty, "IsLoading");


                var mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { loading, grid }
                };


                Content = new ScrollView()
                {
                    Content = mainLayout,
                    Orientation = ScrollOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                Application.Current.Properties["MessageAlreadyCalled"] = false;

                List<SelectedItem> viewedMessagesList = (List<SelectedItem>)Application.Current.Properties["ViewedMessages"];
                viewedMessagesList.Add(new SelectedItem() { Clef = _vm.MessageEntry.Clef, DateLimit = DateTimeOffset.Now.AddDays(3) });
                Application.Current.Properties["ViewedMessages"] = viewedMessagesList;
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
