using System;
using Xamarin.Forms;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using ToossyApp.Converters;
using ToossyApp.Models;
using System.Text.RegularExpressions;
using Akavache;
using System.Reactive.Linq;

namespace ToossyApp.Views
{
    public class ItineraryPayPage : ContentPage
    {
        ItineraryPayViewModel _vm
        {
            get{ return BindingContext as ItineraryPayViewModel; }
        }


        Label priceHtControl = new Label();
        Label tvaLabel = new Label();
        Label tvaControl = new Label();
        Label priceTtcControl = new Label();

        Entry emailControl;
        Entry cardHolderControl;
        Entry cardNumberControl;
        Entry dateControl;
        Entry cvcControl;

        Button buyItineraryButton = new Button
        {
            IsEnabled = true,
            Text = AppResources.PayPage_buyButtonText,
        };

        public ItineraryPayPage()
        {
            try
            {
                Title = AppResources.PayPage_PageTitle;

                BindingContextChanged += (sender, args) => {

                    if (_vm == null)
                        return;

                    _vm.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == "UserData")
                        {
                            setLayout();
                        }
                    };
                };
            }
            catch(Exception ex)
            {

            }
        }

        void DisplayMessage(string msgType, string msg)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await DisplayAlert(msgType, msg, AppResources.ItineraryList_okLabel);
            });
        }

        bool checkData()
        {    
            if(checkEmail()&&checkCardHolder()&& (checkCardNumber()&& checkCvc()&& checkDate() || _vm.UserData.CardToken != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        bool checkEmail()
        {
            if (emailControl.Text==null)
                return false;

            bool isEmail = Regex.IsMatch(emailControl.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            return isEmail;
        }

        bool checkCardHolder()
        {
            if (cardHolderControl.Text == null)
                return false;

            bool isName = Regex.IsMatch(cardHolderControl.Text, @"^[\p{L} \.\-]+$", RegexOptions.IgnoreCase);

            return isName;
        }

        bool checkCardNumber()
        {
            if (cardNumberControl.Text == null)
                return false;

            var creditCardNumber = Regex.Replace(cardNumberControl.Text, @"[^\d]", "");

            bool isNumber = Regex.IsMatch(creditCardNumber, @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", RegexOptions.IgnoreCase);

            return isNumber;
        }

        bool checkDate()
        {
            if (dateControl.Text == null)
                return false;

            bool isDate = Regex.IsMatch(dateControl.Text, @"^((0[1-9])|(1[0-2]))\/(\d{2})$", RegexOptions.IgnoreCase);

            if (isDate)
            {
                var todayMonth = DateTime.Today.Month;
                var todayYear = int.Parse(DateTime.Today.Year.ToString().Substring(2,2));

                var enteredMonth = int.Parse(dateControl.Text.Replace("/", "").Substring(0,2));
                var enteredYear = int.Parse(dateControl.Text.Replace("/", "").Substring(2, 2));

                if(enteredYear < todayYear)
                {
                    isDate = false;
                }
                else if(enteredYear > todayYear)
                {
                    isDate = true;
                }
                else if(enteredYear == todayYear)
                {
                    if(enteredMonth >= todayMonth)
                    {
                        isDate = true;
                    }
                    else
                    {
                        isDate = false;
                    }
                }
            }

            return isDate;
        }

        bool checkCvc()
        {
            if (cvcControl.Text == null)
                return false;

            bool isNumber = Regex.IsMatch(cvcControl.Text, @"^[0-9]{3,4}$", RegexOptions.IgnoreCase);

            return isNumber;
        }

        public delegate bool regexMethodNameDelegate();

        void setEntryColor(object sender, regexMethodNameDelegate methodName)
        {
            var entry = (Entry)sender;

            if(methodName())
            {
                entry.TextColor = Color.SkyBlue;
            }
            else
            {
                entry.TextColor = Color.Red;
            }
        }
        

        void setLayout()
        {
            Device.BeginInvokeOnMainThread(()=> {

                var cardForm = new Grid
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
                        new RowDefinition { Height = GridLength.Auto },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                };

                var approvalAndRememberButtons = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                };

                var itineraryTitelLabel = new Label
                {
                    Text = AppResources.PayPage_itineraryTitle,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                var itineraryTitelControl = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.BodyStyle,
                };

                itineraryTitelControl.SetBinding(Label.TextProperty, "ItineraryEntry.Titre", converter: new TitreConverter());

                var priceHtLabel = new Label
                {
                    Text = AppResources.PayPage_priceHtLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                priceHtControl = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.BodyStyle,
                };


                var tvaLabel = new Label
                {
                    Text = AppResources.PayPage_tvaLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                tvaControl = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.BodyStyle,
                };

                var priceTtcLabel = new Label
                {
                    Text = AppResources.PayPage_priceTtcLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                priceTtcControl = new Label
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.BodyStyle,
                };

                var tarifHt = _vm.ItineraryEntry.Tarif;
                var tauxTva = _vm.ItineraryEntry.Tva;
                var tarifTtc = tarifHt * (1 + tauxTva);
                var montantTva = tarifHt * tauxTva;
                var monnaie = _vm.ItineraryEntry.Currency;

                priceHtControl.Text = String.Format("{0:0.00}", tarifHt) + " " + monnaie;
                tvaLabel.Text = AppResources.PayPage_tvaLabel + " " + (tauxTva * 100).ToString() + "%";
                tvaControl.Text = String.Format("{0:0.00}", montantTva) + " " + monnaie;
                priceTtcControl.Text = String.Format("{0:0.00}", tarifTtc) + " " + monnaie;

                var emailLabel = new Label
                {
                    Text = AppResources.PayPage_itineraryEmailLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                var cardHolderLabel = new Label
                {
                    Text = AppResources.PayPage_cardHolderLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };

                var cardNumberLabel = new Label
                {
                    Text = AppResources.PayPage_cardNumberLabel,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    Style = Device.Styles.TitleStyle,
                };



                if (_vm.UserData.CardToken == null)
                {

                    //si les données user ne sont pas enregistrées
                    emailControl = new Entry
                    {
                        Placeholder = AppResources.PayPage_itineraryEmailPlaceholder,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Keyboard = Keyboard.Email,
                        PlaceholderColor = Color.Orange
                    };
                    emailControl.SetBinding(Entry.TextProperty, "EmailControl", BindingMode.TwoWay);

                    emailControl.TextChanged += (object os, TextChangedEventArgs oe) =>
                    {
                        setEntryColor(os, checkEmail);
                    };

                    cardHolderControl = new Entry
                    {
                        Placeholder = AppResources.PayPage_cardHolderPlaceHolder,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Keyboard = Keyboard.Text,
                        PlaceholderColor = Color.Orange
                    };
                    cardHolderControl.SetBinding(Entry.TextProperty, "CardHolderControl", BindingMode.TwoWay);

                    cardHolderControl.Unfocused += (object os, FocusEventArgs oe) =>
                    {
                        setEntryColor(os, checkCardHolder);
                    };

                    cardNumberControl = new Entry
                    {
                        Placeholder = AppResources.PayPage_cardNumberPlaceHolder,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Keyboard = Keyboard.Numeric,
                        PlaceholderColor = Color.Orange
                    };
                    cardNumberControl.SetBinding(Entry.TextProperty, "CardNumberControl", BindingMode.TwoWay, converter: new CreditCardConverter());

                    cardNumberControl.Unfocused += (object os, FocusEventArgs oe) =>
                    {
                        setEntryColor(os, checkCardNumber);
                    };


                    var dateLabel = new Label
                    {
                        Text = AppResources.PayPage_cardDateLabel,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Start,
                        Style = Device.Styles.TitleStyle,
                    };

                    dateControl = new Entry
                    {
                        Placeholder = AppResources.PayPage_cardDatePlaceHolder,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Keyboard = Keyboard.Numeric,
                        PlaceholderColor = Color.Orange
                    };
                    dateControl.SetBinding(Entry.TextProperty, "DateControl", BindingMode.TwoWay, converter: new CCDateConverter());

                    dateControl.Unfocused += (object os, FocusEventArgs oe) =>
                    {
                        setEntryColor(os, checkDate);
                    };


                    var cvcLabel = new Label
                    {
                        Text = AppResources.PayPage_cardCvcLabel,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Start,
                        Style = Device.Styles.TitleStyle,
                    };

                    cvcControl = new Entry
                    {
                        Placeholder = AppResources.PayPage_cardCvcPlaceHolder,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Start,
                        Keyboard = Keyboard.Numeric,
                        PlaceholderColor = Color.Orange
                    };

                    cvcControl.SetBinding(Entry.TextProperty, "CvcControl", BindingMode.TwoWay, converter: new CvcConverter());

                    cvcControl.Unfocused += (object os, FocusEventArgs oe) =>
                    {
                        setEntryColor(os, checkCvc);
                    };


                    cardForm.Children.Add(dateLabel, 0, 6);
                    cardForm.Children.Add(dateControl, 0, 7);
                    cardForm.Children.Add(cvcLabel, 1, 6);
                    cardForm.Children.Add(cvcControl, 1, 7);

                    var rememberDataLabel = new Label
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Style = Device.Styles.SubtitleStyle,
                        Text = AppResources.PayPage_rememberData,
                    };

                    var rememberDataControl = new Switch
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        IsToggled = false,
                    };
                    rememberDataControl.SetBinding(Switch.IsToggledProperty, "RememberData", BindingMode.OneWayToSource);

                    var rememberDataLabelTapGestureRecognizer = new TapGestureRecognizer();

                    rememberDataLabelTapGestureRecognizer.Tapped += (object os, EventArgs oe) => {
                        bool actualToggleStatus = rememberDataControl.IsToggled;
                        rememberDataControl.IsToggled = !actualToggleStatus;
                    };

                    rememberDataLabel.GestureRecognizers.Add(rememberDataLabelTapGestureRecognizer);


                    approvalAndRememberButtons.Children.Add(rememberDataLabel, 0, 1);
                    approvalAndRememberButtons.Children.Add(rememberDataControl, 1, 1);
                }
                else
                {
                    emailControl = new Entry
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        IsEnabled = false
                    };
                    emailControl.SetBinding(Entry.TextProperty, "UserData.Email", BindingMode.TwoWay);

                    cardHolderControl = new Entry
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        IsEnabled = false
                    };
                    cardHolderControl.SetBinding(Entry.TextProperty, "UserData.CardHolder", BindingMode.TwoWay);

                    cardNumberControl = new Entry
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        IsEnabled = false
                    };
                    cardNumberControl.SetBinding(Entry.TextProperty, "UserData.LastFourCardNumber", BindingMode.TwoWay, converter: new CreditCardConverter());

                    var forgetCard = new Button
                    {
                        IsEnabled = true,
                        Text = AppResources.PayPage_ForgetCard,
                    };

                    forgetCard.Clicked += async (object os, EventArgs oe) =>
                    {
                        var answer = await DisplayAlert(AppResources.MessageMap_WarningLabel, AppResources.PayPage_DeleteCard, AppResources.Yes, AppResources.No);
                        if (answer)
                        {

                            await _vm._cache.InsertObject<UserData>("UserData", new UserData());
                            _vm.UserData = new UserData();
                        }
                    };

                    approvalAndRememberButtons.Children.Add(forgetCard, 0, 1);
                    Grid.SetColumnSpan(forgetCard, 2);
                }

                cardForm.Children.Add(emailLabel, 0, 0);
                Grid.SetColumnSpan(emailLabel, 2);

                cardForm.Children.Add(emailControl, 0, 1);
                Grid.SetColumnSpan(emailControl, 2);

                cardForm.Children.Add(cardHolderLabel, 0, 2);
                Grid.SetColumnSpan(cardHolderLabel, 2);

                cardForm.Children.Add(cardHolderControl, 0, 3);
                Grid.SetColumnSpan(cardHolderControl, 2);

                cardForm.Children.Add(cardNumberLabel, 0, 4);
                Grid.SetColumnSpan(cardNumberLabel, 2);

                cardForm.Children.Add(cardNumberControl, 0, 5);
                Grid.SetColumnSpan(cardNumberControl, 2);


                /*var goToCGV = new Button
                {
                    Text = AppResources.PayPage_link_CGV,
                };
                
                 goToCGV.Clicked += async (sender, args) =>
                {
                    await _vm.goToCGV();
                };*/

                var goToCGV = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Style = Device.Styles.SubtitleStyle,
                    Text = AppResources.PayPage_link_CGV,
                    TextColor = Color.DeepSkyBlue,
                };

                var goToCGVTapGestureRecognizer = new TapGestureRecognizer();
                goToCGVTapGestureRecognizer.Tapped += async (s, e) => {
                    await _vm.goToCGV();
                };

                goToCGV.GestureRecognizers.Add(goToCGVTapGestureRecognizer);

                var cgvApprovalLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Style = Device.Styles.SubtitleStyle,
                    Text = AppResources.PayPage_cgvApproval,
                };

                var cgvApprovalControl = new Switch
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    IsToggled = false,
                };
                cgvApprovalControl.SetBinding(Switch.IsToggledProperty, "CgvApproval", BindingMode.OneWayToSource);

                var cgvApprovalLabelTapGestureRecognizer = new TapGestureRecognizer();
                cgvApprovalLabelTapGestureRecognizer.Tapped += (s, e) => {
                    bool actualToggleStatus = cgvApprovalControl.IsToggled;
                    cgvApprovalControl.IsToggled = !actualToggleStatus;
                };

                cgvApprovalLabel.GestureRecognizers.Add(cgvApprovalLabelTapGestureRecognizer);

                approvalAndRememberButtons.Children.Add(goToCGV, 0, 3);
                Grid.SetColumnSpan(goToCGV, 2);

                approvalAndRememberButtons.Children.Add(cgvApprovalLabel, 0, 2);
                approvalAndRememberButtons.Children.Add(cgvApprovalControl, 1, 2);

                

                

                buyItineraryButton.Clicked += onPayButtonPressed;

                var buyButtonLayout = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) },
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    },
                };

                buyButtonLayout.Children.Add(buyItineraryButton, 0, 0);

                var mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                mainLayout.SetBinding(StackLayout.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());

                mainLayout.Children.Add(itineraryTitelLabel);
                mainLayout.Children.Add(itineraryTitelControl);
                mainLayout.Children.Add(priceHtLabel);
                mainLayout.Children.Add(priceHtControl);
                mainLayout.Children.Add(tvaLabel);
                mainLayout.Children.Add(tvaControl);
                mainLayout.Children.Add(priceTtcLabel);
                mainLayout.Children.Add(priceTtcControl);

                mainLayout.Children.Add(cardForm);

                mainLayout.Children.Add(approvalAndRememberButtons);
                mainLayout.Children.Add(buyButtonLayout);

                var scroll = new ScrollView()
                {
                    Content = mainLayout,
                    Orientation = ScrollOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                var researchLabel = new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = AppResources.ItineraryList_typeOfActivity_Itinerary,
                    Style = Device.Styles.BodyStyle
                };

                var loading = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        researchLabel
                    }
                };

                loading.SetBinding(StackLayout.IsVisibleProperty, "IsLoading");

                var finalLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        scroll,
                        loading
                    }
                };               

                this.Content = finalLayout;
            });
        }

        async void onPayButtonPressed(object sender, EventArgs events)
        {
            try
            {
                if (_vm == null)
                    return;

                if (_vm.CgvApproval.Equals(true) && checkData().Equals(true))
                {
                    buyItineraryButton.IsEnabled = false;

                    bool paymentResult = await _vm.sendPaymentRequest().ConfigureAwait(continueOnCapturedContext: false);

                        if (paymentResult)
                        {

                            DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.PayPage_paymentSuceeded);
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _vm._navService.GoToRootPage();
                            });
                                

                            }
                        else
                        {
                            buyItineraryButton.IsEnabled = true;
                            DisplayMessage(AppResources.ItineraryList_errorLabel, AppResources.PayPage_paymentFailed);
                        }

                }
                else if (_vm.CgvApproval.Equals(true) && checkData().Equals(false))
                {
                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.PayPage_dataProblem);
                }
                else if (_vm.CgvApproval.Equals(false) && checkData().Equals(true))
                {
                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.PayPage_approvalProblem);
                }
                else
                {
                    DisplayMessage(AppResources.MessageMap_InformationLabel, AppResources.PayPage_dataAnApprovalProblem);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
