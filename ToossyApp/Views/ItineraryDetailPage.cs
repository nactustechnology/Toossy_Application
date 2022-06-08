using ToossyApp.Converters;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class ItineraryDetailPage : ContentPage
    {
        ItineraryDetailViewModel _vm
        {
            get { return BindingContext as ItineraryDetailViewModel; }
        }

        ScrollView scroll;

        public ItineraryDetailPage()
        {
            try
            {
                Title = AppResources.ItineraryDetail_PageTitle;

                var previewButton = new ToolbarItem { Text = AppResources.ItineraryDetail_PreviewMenu };
                previewButton.SetBinding(ToolbarItem.CommandProperty, "GoToItineraryPreviewPage");
                previewButton.SetBinding(ToolbarItem.CommandParameterProperty, "ItineraryEntry.ItineraryPins");

                ToolbarItems.Add(previewButton);

                var Titre = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Style = Device.Styles.TitleStyle
                };
                Titre.SetBinding(Label.TextProperty, "ItineraryEntry.Titre", converter: new TitreConverter());

                var Langue = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 15
                };
                Langue.SetBinding(Image.SourceProperty, "ItineraryEntry.Langue", converter: new LangueConverter());

                var Illustration = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = 10
                };
                Illustration.SetBinding(Image.SourceProperty, "ItineraryEntry.Illustrations", converter: new IllustrationConverterMain());

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
                Titre_illustration.SetBinding(Label.TextProperty, "ItineraryEntry.Titre_illustrations");


                var Type_parcours = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Type_parcours.SetBinding(Label.TextProperty, "ItineraryEntry.Type_parcours", converter: new TypeParcoursConverter());

                var Theme = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Theme.SetBinding(Label.TextProperty, "ItineraryEntry.Theme", converter: new ThemeConverter2());

                var Duree = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Duree.SetBinding(Label.TextProperty, "ItineraryEntry.Duree", converter: new DureeConverter());

                var Gratuit = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Gratuit.SetBinding(Label.TextProperty, "ItineraryEntry.Payant", converter: new GratuitConverter());

                var Nombre_message = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Nombre_message.SetBinding(Label.TextProperty, "ItineraryEntry.Nombre_message", converter: new Nombre_messageConverter());

                var Note = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 20
                };
                Note.SetBinding(Image.SourceProperty, "ItineraryEntry.Note", converter: new NoteConverter());

                var Nombre_commentaires = new Label
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Style = Device.Styles.BodyStyle
                };
                Nombre_commentaires.SetBinding(Label.TextProperty, "ItineraryEntry.Nombre_commentaires", converter: new Nombre_commentairesConverter());


                var Description = new WebView()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };
                Description.SetBinding(WebView.SourceProperty, "ItineraryEntry.Description", converter: new DescriptionConverter());

                var DescriptionHeight = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle
                };

                DescriptionHeight.SetBinding(Label.TextProperty, "ItineraryEntry.Description", converter: new DescriptionHeightConverter());


                var firstRow = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength (9, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                    }
                };

                firstRow.Children.Add(Titre, 0, 0);
                firstRow.Children.Add(Langue, 1, 0);

                var DescriptionRow = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var DescriptionHeightRow = new RowDefinition { Height = GridLength.Auto };


                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
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

                grid.SetBinding(Grid.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());

                grid.Children.Add(firstRow, 0, 0);
                Grid.SetColumnSpan(firstRow, 2);

                grid.Children.Add(imageLayout, 0, 1);
                Grid.SetColumnSpan(imageLayout, 2);

                grid.Children.Add(Titre_illustration, 0, 2);
                Grid.SetColumnSpan(Titre_illustration, 2);

                grid.Children.Add(Type_parcours, 0, 3);
                grid.Children.Add(Theme, 1, 3);

                grid.Children.Add(Duree, 0, 4);
                grid.Children.Add(Nombre_message, 1, 4);

                grid.Children.Add(Note, 0, 5);
                grid.Children.Add(Nombre_commentaires, 1, 5);

                grid.Children.Add(Description, 0, 7);
                Grid.SetColumnSpan(Description, 2);

                grid.Children.Add(DescriptionHeight, 0, 8);
                Grid.SetColumnSpan(DescriptionHeight, 2);

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
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        new Label { Text = AppResources.ItineraryDetail_LoadingMsg, HorizontalTextAlignment = TextAlignment.Center }
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


                scroll = new ScrollView()
                {
                    Content = mainLayout,
                    Orientation = ScrollOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                Content = scroll;
            }
            catch (Exception ex)
            {

            }
        }
    }
}