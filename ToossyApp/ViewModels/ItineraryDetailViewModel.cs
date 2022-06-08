using Akavache;
using ToossyApp.Models;
using ToossyApp.Resources;
using ToossyApp.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToossyApp.ViewModels
{
    public class ItineraryDetailViewModel : BaseViewModel<ItineraryEntry>
    {
        bool _imageIsLoading;
        public bool ImageIsLoading
        {
            get { return _imageIsLoading; }
            set
            {
                _imageIsLoading = value;
                OnPropertyChanged();
            }
        }

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

        string _tarifTTC;
        public string TarifTTC
        {
            get { return _tarifTTC; }
            set
            {
                _tarifTTC = value;
                OnPropertyChanged();
            }
        }

        public ItineraryDetailViewModel(INavService navService, IBlobCache cache) : base(navService, cache)
        {
            IsLoading = true;
            ImageIsLoading = true;
        }

        public override async Task Init()
        {
        }

        public override async Task Init(ItineraryEntry entry)
        {
            try
            {
                IsLoading = true;

                ItineraryEntry = entry;

                if (ItineraryEntry.Payant == 1 && ItineraryEntry.IsSelected.Equals(false))
                {
                    TarifTTC = AppResources.ItineraryDetail_Buy + " : " + String.Format("{0:0.00}", ItineraryEntry.Tarif * (1 + ItineraryEntry.Tva)) + " " + ItineraryEntry.Currency;
                }
                else if (ItineraryEntry.Payant == 1 && ItineraryEntry.IsSelected.Equals(true))
                {
                    TarifTTC = AppResources.ItineraryDetail_Paid + " " + String.Format("{0:0.00}", ItineraryEntry.Tarif * (1 + ItineraryEntry.Tva)) + " " + ItineraryEntry.Currency;
                }
                else
                {
                    TarifTTC = "-";
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        Command<ItineraryPin[]> _goToItineraryPreviewPage;
        public Command<ItineraryPin[]> GoToItineraryPreviewPage
        {
            get
            {
                return _goToItineraryPreviewPage ?? (_goToItineraryPreviewPage = new Command<ItineraryPin[]>(async (pinsArray) => await GoToItineraryPreviewPageCommand(pinsArray)));
            }
        }

        Command<int> _goToItineraryPayPage;
        public Command<int> GoToItineraryPayPage
        {
            get
            {
                return _goToItineraryPayPage ?? (_goToItineraryPayPage = new Command<int>(async (itineraryClef) => await GoToItineraryPayPageCommand(itineraryClef)));
            }
        }

        async Task GoToItineraryPreviewPageCommand(ItineraryPin[] pinsArray)
        {
            await NavService.NavigateTo<ItineraryPreviewViewModel, ItineraryPin[]>(pinsArray);
        }

        async Task GoToItineraryPayPageCommand(int itineraryClef)
        {
            await NavService.NavigateTo<ItineraryPayViewModel, ItineraryEntry>(_itineraryEntry);
        }
    }
}
