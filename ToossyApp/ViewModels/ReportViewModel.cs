using Akavache;
using ToossyApp.Models;
using ToossyApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToossyApp.ViewModels
{
    public class ReportViewModel : BaseViewModel<int>
    {
        int _itineraryKey;
        public int ItineraryKey
        {
            get { return _itineraryKey; }
            set
            {
                _itineraryKey = value;
                OnPropertyChanged();
            }
        }

        string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        readonly IItineraryDataService _itineraryDataService;
        readonly IBlobCache _cache;


        Command _saveCommand;
        public Command SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new Command(async () => await ExecuteSaveCommand(), CanSave));
            }
        }

        public ReportViewModel(INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService, cache)
		{
            _itineraryDataService = itineraryDataService;
            _cache = cache;
        }

        public override async Task Init(int parameter)
        {
            _itineraryKey = parameter;
        }

        async Task ExecuteSaveCommand()
        {
            if (IsLoading)
                return;

            IsLoading = true;

            var newReport = new ItineraryReport
            {
                ItineraryKey = ItineraryKey,
                Comment = Comment
            };

            try
            {
                ItineraryReport result = new ItineraryReport();

                if((bool)Application.Current.Properties["IsOffline"]==false)
                {
                    result = await _itineraryDataService.AddReportAsync(newReport);
                }
                

                if((bool)Application.Current.Properties["IsOffline"] == true || result.IsTransmitted == false)
                {
                    var reportList = (List<ItineraryReport>)Application.Current.Properties["reportList"];
                    reportList.Add(newReport);
                    Application.Current.Properties["reportList"] = reportList;
                }
 
            }
            catch(Exception ex)
            {

            }
            finally
            {
                await NavService.GoBack();
                IsLoading = false;
            }
        }

        bool CanSave()
        {
            //return !string.IsNullOrWhiteSpace(Comment);
            return true;
        }



    }
}
