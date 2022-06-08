using ToossyApp.Converters;
using ToossyApp.Models;
using ToossyApp.Renderers;
using ToossyApp.ViewModels;
using Xamarin.Forms;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ToossyApp.Resources;
using Akavache;
using System.Reactive.Linq;

namespace ToossyApp.Views
{
    public class ItineraryListPage : ContentPage
    {
        // : ContentPage
        MainViewModel _vm
        {
            get { return BindingContext as MainViewModel; }
        }

        ListView entries;

        public ItineraryListPage()
        {
            try
            {
                var typeOfActivity = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, Text = AppResources.ItineraryList_typeOfActivity_Itinerary };
                var researchLabel = new Label {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = AppResources.ItineraryList_researchLabel,
                    Style = Device.Styles.BodyStyle
                };

                BindingContextChanged += (sender, args) =>
                {
                    if (_vm == null)
                        return;


                    //_vm.IsLoading = true;

                    _vm.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "IsLocalizing")
                        {
                            var IsLocalizing = _vm.IsLocalizing;

                            if (IsLocalizing.Equals(true))
                            {
                                typeOfActivity.Text = AppResources.ItineraryList_typeOfActivity_Localization;
                            }
                            else
                            {
                                typeOfActivity.Text = AppResources.ItineraryList_typeOfActivity_Itinerary ;
                            }
                        }

                        if(e.PropertyName =="ItineraryEntries")
                        {
                            _vm.setSelectedItineraries();
                        }

                        if (e.PropertyName == "VisitesPayees")
                        {
                            if(_vm.VisitesPayees.Count > 0)
                            {
                                updateVisistesPayees();
                            }
                                
                        }

                        if (e.PropertyName == "IsFirstItineraryTest")
                        {
                            if (_vm.IsFirstItineraryTest == true)
                            {
                                _vm.setUnsetTestItinerary();
                            }
                        }
                    };
                };

                ;

                /*var filterButton = new ToolbarItem { Text = AppResources.ItineraryList_filterButton };
                var messageButton = new ToolbarItem { Text = AppResources.ItineraryList_messageButton };*/

                var filterButton = new ToolbarItem { Icon = "filter_icon.png" };
                var messageButton = new ToolbarItem { Text = AppResources.ItineraryList_messageButton, Icon = "map_icon.png" };

                filterButton.SetBinding(ToolbarItem.CommandProperty,"GoToFilterPage");
                messageButton.SetBinding(ToolbarItem.CommandProperty, "GoToMessagePage");
                messageButton.SetBinding(ToolbarItem.CommandParameterProperty, "DisplayedMessages");

                ToolbarItems.Add(messageButton);
                ToolbarItems.Add(filterButton);


                var viewableItineraryTemplate = new DataTemplate(() => {

                    var illustrationImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
                    illustrationImage.SetBinding(Image.SourceProperty, "Illustrations",converter: new IllustrationConverterThumb());

                    var titreLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontAttributes = FontAttributes.Bold,
                        Style = Device.Styles.ListItemTextStyle
                    };
                    titreLabel.SetBinding(Label.TextProperty, "Titre", converter: new TitreConverter());

                    var typeParcoursLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Style = Device.Styles.ListItemDetailTextStyle
                    };
                    typeParcoursLabel.SetBinding(Label.TextProperty, "Type_parcours", converter: new TypeParcoursConverter());

                    var themeLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Style = Device.Styles.ListItemDetailTextStyle
                    };
                    themeLabel.SetBinding(Label.TextProperty, "Theme", converter: new ThemeConverter1());

                    var noteImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HeightRequest = 15,
                        MinimumHeightRequest = 15
                        //Source = ImageSource.FromFile("Resources/drawable/stars_3.png")
                    };
                    noteImage.SetBinding(Image.SourceProperty, "Note", converter: new NoteConverter());

                   
                    var addItineraryButton = new ItinerarySwitch
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    addItineraryButton.SetBinding(ItinerarySwitch.ClefProperty, "Clef");
                    addItineraryButton.SetBinding(ItinerarySwitch.IsToggledProperty, "IsSelected");
                    addItineraryButton.Toggled += OnAddItineraryButtonToggled;

                    var switchTapGestureRecognizer = new TapGestureRecognizer();
                    switchTapGestureRecognizer.Tapped += (s, e) =>
                    {
                        bool actualToggleStatus = addItineraryButton.IsToggled;
                        addItineraryButton.IsToggled = !actualToggleStatus;
                    };

                    addItineraryButton.GestureRecognizers.Add(switchTapGestureRecognizer);

                    var grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (1.2, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (0.9, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (0.8, GridUnitType.Star) }
                        }
                    };

                    grid.Children.Add(illustrationImage, 0, 0);
                    Grid.SetRowSpan(illustrationImage,2);
                    grid.Children.Add(titreLabel, 1, 0);
                    Grid.SetColumnSpan(titreLabel, 3);

                    grid.Children.Add(typeParcoursLabel, 1, 1);
                    grid.Children.Add(themeLabel, 2, 1);
                    grid.Children.Add(noteImage, 3, 1);
                    //Grid.SetRowSpan(noteImage, 2);
                    grid.Children.Add(addItineraryButton, 4, 0);
                    Grid.SetRowSpan(addItineraryButton, 2);


                    return new ViewCell { View = grid };
                });

                var notViewableItineraryTemplate = new DataTemplate(() => {

                    var illustrationImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    };
                    illustrationImage.SetBinding(Image.SourceProperty, "Illustrations", converter: new IllustrationConverterThumb());

                    var titreLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        FontAttributes = FontAttributes.Bold,
                        Style = Device.Styles.ListItemTextStyle
                    };
                    titreLabel.SetBinding(Label.TextProperty, "Titre", converter: new TitreConverter());

                    var typeParcoursLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Style = Device.Styles.ListItemDetailTextStyle
                    };
                    typeParcoursLabel.SetBinding(Label.TextProperty, "Type_parcours", converter: new TypeParcoursConverter());

                    var themeLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Style = Device.Styles.ListItemDetailTextStyle
                    };
                    themeLabel.SetBinding(Label.TextProperty, "Theme", converter: new ThemeConverter1());

                    var noteImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 15,
                        MinimumHeightRequest = 15
                        //Source = ImageSource.FromFile("Resources/drawable/stars_3.png")
                    };
                    noteImage.SetBinding(Image.SourceProperty, "Note", converter: new NoteConverter());

                    var buyLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Style = Device.Styles.ListItemDetailTextStyle
                    };
                    buyLabel.SetBinding(Label.TextProperty, "IsSelected", converter: new IsSelectedConverter1());
                    buyLabel.SetBinding(Label.TextColorProperty, "IsSelected", converter: new IsSelectedConverter2());
                    

                    /*var buyItineraryButton = new Button
                    {
                        Text = AppResources.ItineraryList_buyLabel,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        IsEnabled = true,
                    };
                    
                    buyItineraryButton.SetBinding(Button.CommandParameterProperty, "Clef");

                    buyItineraryButton.Clicked += OnBuyItineraryButtonClicked;*/

                    var grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (1.2, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (0.9, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength (0.8, GridUnitType.Star) }
                        }
                    };

                    grid.Children.Add(illustrationImage, 0, 0);
                    Grid.SetRowSpan(illustrationImage, 2);

                    grid.Children.Add(titreLabel, 1, 0);
                    Grid.SetColumnSpan(titreLabel, 3);

                    grid.Children.Add(typeParcoursLabel, 1, 1);
                    grid.Children.Add(themeLabel, 2, 1);
                    grid.Children.Add(noteImage, 3, 1);

                    grid.Children.Add(buyLabel, 4, 0);
                    Grid.SetRowSpan(buyLabel, 2);

                    /*grid.Children.Add(buyItineraryButton, 4, 0);
                    Grid.SetRowSpan(buyItineraryButton, 2);*/

                    var ViewCell = new ViewCell { View = grid };

                    return ViewCell;
                });

                entries = new ListView
                {
                    IsPullToRefreshEnabled = true,
                    SeparatorVisibility = SeparatorVisibility.Default,
                    SeparatorColor = Color.FromHex("#a61f21"),
                    HasUnevenRows = true,
                    ItemTemplate = new ItineraryListDataTemplateSelector
                    {
                        ValidTemplate = viewableItineraryTemplate,
                        InvalidTemplate = notViewableItineraryTemplate
                    },
                    BackgroundColor = Color.White
                };

                entries.SetBinding(ListView.ItemsSourceProperty, "ItineraryEntries");
                entries.SetBinding(ListView.IsRefreshingProperty, "IsLoading", mode: BindingMode.OneWay);

                

                entries.Refreshing += async (sender, e) => {
                    await refreshViewList();
                };

            
                entries.ItemTapped += (sender, e) => {
                    var item = (ItineraryEntry)e.Item;
                    _vm.GoToItineraryDetailPage.Execute(item);
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

                loading.SetBinding(StackLayout.IsVisibleProperty, "IsLoading");


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

                var updateButton = new Image
                {
                    Aspect = Aspect.AspectFit,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Source = ImageSource.FromFile(imageFolder+"refresh_button.png"),
                    BackgroundColor = Color.White,
                    WidthRequest = 64
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (sender, e) => { await refreshViewList(); };
                //tapGestureRecognizer.Tapped += (sender, e) => { entries.IsRefreshing = true; };

                updateButton.GestureRecognizers.Add(tapGestureRecognizer);

                var result = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        entries,
                        updateButton,
                        researchLabel
                    },
                    BackgroundColor = Color.White
                };

                result.SetBinding(StackLayout.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());

                
                var mainLayout = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = { loading, result }
                };

                Content = mainLayout;
            }
            catch (Exception ex)
            {

            }

            
        }

        async Task refreshViewList()
        {
            try
            {
                if (_vm != null)
                {
                    bool PositionUpdated = false;
                    bool ItineraryUpdated = false;
                    bool PaidtineraryUpdated = false;

                    PositionUpdated = await _vm.LoadCurrentPosition().ConfigureAwait(continueOnCapturedContext: false);

                    if (PositionUpdated.Equals(false))
                        DisplayMessage( AppResources.ItineraryList_errorLabel, AppResources.ItineraryList_localizationError);

                    ItineraryUpdated = await _vm.LoadItineraries().ConfigureAwait(continueOnCapturedContext: false);

                    if (ItineraryUpdated.Equals(false))
                        DisplayMessage( AppResources.ItineraryList_errorLabel, AppResources.ItineraryList_itineraryResearchError);

                    PaidtineraryUpdated = await _vm.checkPaidItineraries().ConfigureAwait(continueOnCapturedContext: false);

                    if (PaidtineraryUpdated.Equals(false))
                    {
                        DisplayMessage(AppResources.ItineraryList_errorLabel, AppResources.ItineraryList_paidItineraryUpdateError);
                    }

                    if (Application.Current.Properties.ContainsKey("commentList"))
                    {
                        var commentList = (List<ItineraryComment>)Application.Current.Properties["commentList"];

                        var commentListBis = new List<ItineraryComment>(commentList);

                        foreach (ItineraryComment comment in commentListBis)
                        {
                            var result = await _vm.sendComment(comment);

                            if(result.IsTransmitted)
                            {
                                commentList.Remove(comment);
                            }
                            
                        }

                        Application.Current.Properties["commentList"] = commentList;
                    }

                    if (Application.Current.Properties.ContainsKey("reportList"))
                    {
                        var reportList = (List<ItineraryComment>)Application.Current.Properties["reportList"];

                        var reportListBis = new List<ItineraryComment>(reportList);

                        foreach (ItineraryComment comment in reportListBis)
                        {
                            var result = await _vm.sendComment(comment);

                            if (result.IsTransmitted)
                            {
                                reportList.Remove(comment);
                            }

                        }

                        Application.Current.Properties["reportList"] = reportList;
                    }
                }
            }
            catch(Exception ex)
            {
                _vm.IsLoading = false;
                _vm.IsLocalizing = false;
            }
        }

        void DisplayMessage(string msgType, string msg)
        {
            Device.BeginInvokeOnMainThread(async () => {

                await DisplayAlert(msgType, msg, AppResources.ItineraryList_okLabel);
            });
        }

        void DisplayDialogBox(string msgType, string msg)
        {
            Device.BeginInvokeOnMainThread(async () => {
                _vm.IsFirstItineraryTest = await DisplayAlert(msgType, msg, AppResources.Yes, AppResources.No);
            });
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (Application.Current.Properties.ContainsKey("ItineraryFilter"))
                {
                    _vm.ItineraryFiltre = (ItineraryFilter)Application.Current.Properties["ItineraryFilter"];
                }

                if (Application.Current.Properties.ContainsKey("VisitesPayees"))
                {
                    _vm.VisitesPayees = (List<TransactionResponse>)Application.Current.Properties["VisitesPayees"];
                }

                if (Application.Current.Properties.ContainsKey("SelectedItineraries"))
                {
                    _vm.SelectedItineraries = (List<SelectedItem>)Application.Current.Properties["SelectedItineraries"];
                }

                if (!Application.Current.Properties.ContainsKey("FirstDisplay"))
                {
                    Application.Current.Properties["FirstDisplay"] = true;

                    _vm.ItineraryEntries = (List<ItineraryEntry>)Application.Current.Properties["ItineraryEntries"];
                    await refreshViewList();

                }

                if (Application.Current.Properties.ContainsKey("FirstStartStatus") && (bool)Application.Current.Properties["FirstStartStatus"] == true)
                {
                    await _vm._cache.InsertObject<bool>("FirstStartStatus", false);
                    Application.Current.Properties["FirstStartStatus"] = false;

                    DisplayDialogBox(AppResources.TestItinerary_MsgType, AppResources.TestItinerary_Msg);
                    //var wantToSeeTestItinerary = await DisplayAlert(AppResources.TestItinerary_MsgType, AppResources.TestItinerary_Msg, AppResources.Yes, AppResources.No);

                }
            }
            catch(Exception ex)
            {

            }
        }


        void OnAddItineraryButtonToggled(object sender, ToggledEventArgs e)
        {
            var identifiant = (ItinerarySwitch)sender;

            if (e.Value)
            {
                _vm.selectItinerary(identifiant.Clef);
            }
            else
            {
                _vm.unselectItinerary(identifiant.Clef);
            }
        }

        /*void OnBuyItineraryButtonClicked(object sender, EventArgs e)
        {
            var item = (Button)sender;
            _vm.GoToItineraryPayPage.Execute(item.CommandParameter);
        }*/

        protected override async void OnDisappearing()
        {
            try
            {
                base.OnDisappearing();
                Application.Current.Properties["ItineraryFilter"] = _vm.ItineraryFiltre;
                Application.Current.Properties["SelectedItineraries"] = _vm.SelectedItineraries;

                await _vm._cache.InsertObject("ItineraryFilter", Application.Current.Properties["ItineraryFilter"]);
                await _vm._cache.InsertObject("SelectedItineraries", Application.Current.Properties["SelectedItineraries"]);

                await _vm._cache.InsertObject("commentList", Application.Current.Properties["commentList"]);
                await _vm._cache.InsertObject("reportList", Application.Current.Properties["reportList"]);
            }
            catch (Exception ex)
            {

            }
        }

        async Task updateVisistesPayees()
        {
            var selectedItinerariesOldQty = _vm.SelectedItineraries.Count;

            await _vm.checkPaidItineraries().ConfigureAwait(continueOnCapturedContext: false);

            var selectedItinerariesNewQty = _vm.SelectedItineraries.Count;

            if (selectedItinerariesOldQty != selectedItinerariesNewQty)
            {
                Device.BeginInvokeOnMainThread( () =>
                {
                    entries.BeginRefresh();
                    entries.EndRefresh();
                });
                
            }
        }

    }
}
 