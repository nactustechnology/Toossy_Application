using ToossyApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToossyApp.Services;
using Stripe;
using Xamarin.Forms;
using ToossyApp.Helper;
using Akavache;
using System.Reactive.Linq;
using ToossyApp.Resources;

namespace ToossyApp.ViewModels
{
    public class ItineraryPayViewModel : BaseViewModel<ItineraryEntry>
    {
        ItineraryEntry _itineraryEntry;
        public ItineraryEntry ItineraryEntry
        {
            get { return _itineraryEntry; }
            set
            {
                _itineraryEntry = value;
                OnPropertyChanged();
            }
        }

        string _emailControl;
        public string EmailControl
        {
            get { return _emailControl; }
            set
            {
                _emailControl = value;
            }
        }

        string _cardHolderControl;
        public string CardHolderControl
        {
            get { return _cardHolderControl; }
            set
            {
                _cardHolderControl = value;
            }
        }

        string _cardNumberControl;
        public string CardNumberControl
        {
            get { return _cardNumberControl; }
            set
            {
                _cardNumberControl = value;
            }
        }

        string _dateControl;
        public string DateControl
        {
            get { return _dateControl; }
            set
            {
                _dateControl = value;
            }
        }

        string _cvcControl;
        public string CvcControl
        {
            get { return _cvcControl; }
            set
            {
                _cvcControl = value;
            }
        }

        bool _rememberData;
        public bool RememberData
        {
            get { return _rememberData; }
            set
            {
                _rememberData = value;
            }
        }


       

        bool _cgvApproval;
        public bool CgvApproval
        {
            get { return _cgvApproval; }
            set
            {
                _cgvApproval = value;

            }
        }


        string _cardToken;
        public string CardToken
        {
            get { return _cardToken; }
            set
            {
                _cardToken = value;
            }
        }

        string _lastFourCardNumber;
        public string LastFourCardNumber
        {
            get { return _lastFourCardNumber; }
            set
            {
                _lastFourCardNumber = value;
            }
        }

        UserData _userData;
        public UserData UserData
        {
            get { return _userData; }
            set
            {
                _userData = value;
                OnPropertyChanged();
            }
        }

        readonly IItineraryDataService _itineraryDataService;
        public readonly IBlobCache _cache;
        public readonly INavService _navService;

        public ItineraryPayViewModel(INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService, cache)
        {
            _itineraryDataService = itineraryDataService;
            _cache = cache;
            _navService = navService;

            
        }

        async Task<UserData> getUserData()
        {
            return await _cache.GetOrCreateObject<UserData>("UserData", () => new UserData());
        }

        public async override Task Init(ItineraryEntry itineraryEntry)
        {
            IsLoading = true;

            ItineraryEntry = itineraryEntry;

            UserData = HelperMethods.syncGetMethod<UserData>(getUserData);

        
            //********Valeurs tests
            #if DEBUG
                EmailControl = "jfvoelker@nactustechnology.com";
                CardHolderControl = "Voelker";
                CardNumberControl = "4000002500000003";
                DateControl = "1019";
                CvcControl = "456";
            #endif

            IsLoading = false;
        }


        public async Task goToCGV()
        {
            Device.OpenUri(new Uri(AppResources.DocumentEndPoint_CGV));
        }



        public async Task<bool> sendPaymentRequest()
        {
            string email = _emailControl;
            string cardHolder = _cardHolderControl;
            string cardNumber = _cardNumberControl;
            string cardExpMonth = _dateControl.Substring(0,2);
            string cardExpYear = _dateControl.Substring(2, 2);
            string cardCVC = _cvcControl;

            bool rememberData = _rememberData;
            bool cgvApproval = _cgvApproval;

            

            try
            {
                

                //var description = "Achat parcours " + System.Net.WebUtility.HtmlDecode(ItineraryEntry.Titre) + " - " + ItineraryEntry.Clef.ToString();

                var charge = new ItineraryCharge
                {
                    itineraryId = ItineraryEntry.Clef,
                    Description = EmailControl,
                    RememberData = rememberData,
                    CgvApproval = cgvApproval
                };

                if(UserData.CardToken != null)
                {
                    charge.Token = UserData.CardToken;
                    
                }
                else
                {
                #if DEBUG
                    StripeConfiguration.SetApiKey("pk_test_pkNQf9te1KBYMPT4MqOrkVZM");
                #else
                    StripeConfiguration.SetApiKey("pk_live_PPuZFEYsjjwhoXQ86m0eExOT");
                #endif

                    var tokenOptions = new StripeTokenCreateOptions()
                    {
                        Card = new StripeCreditCardOptions()
                        {
                            Name = cardHolder,
                            Number = cardNumber,
                            ExpirationYear = int.Parse("20" + cardExpYear),
                            ExpirationMonth = int.Parse(cardExpMonth),
                            Cvc = cardCVC
                        }
                    };

                    var tokenService = new StripeTokenService();

                    StripeToken stripeToken = tokenService.Create(tokenOptions);

                    charge.Token = stripeToken.Id;
                    LastFourCardNumber = stripeToken.StripeCard.Last4;
                }
                    


                var transactionResult= new TransactionResponse();

                transactionResult = await _itineraryDataService.transaction(charge);

                if (transactionResult.Status == "succeeded")
                {
                    if (RememberData.Equals(true))
                    {
                        UserData = new UserData
                        {
                            Email = EmailControl,
                            CardHolder = CardHolderControl,
                            LastFourCardNumber = LastFourCardNumber,
                            CardToken = transactionResult.RememberData,
                        };

                        await _cache.InsertObject("UserData", UserData);
                    }

                    var VisitesPayees = new List<TransactionResponse>();

                    if (Application.Current.Properties.ContainsKey("VisitesPayees"))
                    {
                        VisitesPayees = (List<TransactionResponse>)Application.Current.Properties["VisitesPayees"];
                    }

                    VisitesPayees.Add(transactionResult);

                    Application.Current.Properties["VisitesPayees"] = VisitesPayees;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
