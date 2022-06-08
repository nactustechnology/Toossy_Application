using Akavache;
using ToossyApp.Models;
using ToossyApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToossyApp.ViewModels
{
    public class RatingViewModel : BaseViewModel<int>
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

        double _rating;
        public double Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
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

        public RatingViewModel(INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService, cache)
        {
            _itineraryDataService = itineraryDataService;
            _cache = cache;

            Task.Delay(2000).Wait();
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

            var newComment = new ItineraryComment
            {
                ItineraryKey = ItineraryKey,
                Comment = Comment,
                Rating = Rating
            };

            try
            {
                ItineraryComment result = new ItineraryComment();

                if ((bool)Application.Current.Properties["IsOffline"] == false)
                {
                    result = await _itineraryDataService.AddCommentAsync(newComment);
                }


                if ((bool)Application.Current.Properties["IsOffline"] == true || result.IsTransmitted == false)
                {
                    var commentList = (List<ItineraryComment>)Application.Current.Properties["commentList"];
                    commentList.Add(newComment);
                    Application.Current.Properties["commentList"] = commentList;
                }


                await NavService.GoBack();

            }
            finally
            {
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
