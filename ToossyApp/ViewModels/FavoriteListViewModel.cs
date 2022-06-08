using System.Threading.Tasks;
using Akavache;
using ToossyApp.Models;
using ToossyApp.Services;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Collections.Generic;
using System;

namespace ToossyApp.ViewModels
{
    public class FavoriteListViewModel : BaseViewModel
    {
        List<int> _favoriteList;
        public List<int> FavoriteList
        {
            get { return _favoriteList; }
            set
            {
                _favoriteList = value;
                OnPropertyChanged();
            }
        }

        List<MessageEntry> _favoriteEntries;
        public List<MessageEntry> FavoriteEntries
        {
            get { return _favoriteEntries; }
            set
            {
                _favoriteEntries = value;
                OnPropertyChanged();
            }
        }

        readonly IBlobCache _cache;

        public FavoriteListViewModel(INavService navService, IBlobCache cache) : base(navService)
        {
            _cache = cache;
            
        }

        public async override Task Init()
        {
            IsLoading = true;

            try
            {
                _favoriteList = await _cache.GetOrCreateObject("Favorites", () => new List<int>());
                _favoriteEntries = new List<MessageEntry>();

                foreach (int favorite in FavoriteList)
                {
                    var message = await _cache.GetObject<MessageEntry>(favorite.ToString());
                    _favoriteEntries.Add(message);
                }

                FavoriteEntries = _favoriteEntries;
            }
            catch(Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
            
        }


       Command<MessageEntry> _goToFavoriteDetailPage;
        public Command<MessageEntry> GoToFavoriteDetailPage
        {
            get
            {
                return _goToFavoriteDetailPage ?? (_goToFavoriteDetailPage = new Command<MessageEntry>(async (messageDetails) => await GoToFavoriteDetailPageCommand(messageDetails)));
            }
        }

        async Task GoToFavoriteDetailPageCommand(MessageEntry messageDetails)
        {
            await NavService.NavigateTo<FavoriteDetailViewModel, MessageEntry>(messageDetails);
        }
    }
}
