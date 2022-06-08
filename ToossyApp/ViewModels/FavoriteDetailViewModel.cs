using System.Threading.Tasks;
using ToossyApp.Services;
using ToossyApp.Models;
using System;
using System.Collections.Generic;
using Akavache;
using System.Reactive.Linq;

namespace ToossyApp.ViewModels
{
    public class FavoriteDetailViewModel : BaseViewModel<MessageEntry>
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

        MessageEntry _favoriteEntry;
        public MessageEntry FavoriteEntry
        {
            get { return _favoriteEntry; }
            set
            {
                _favoriteEntry = value;
                OnPropertyChanged();
            }
        }


        readonly IBlobCache _cache;

        public FavoriteDetailViewModel(INavService navService, IBlobCache cache) : base(navService, cache)
        {
            IsLoading = true;
            ImageIsLoading = true;

            _cache = cache;
        }

        public async override Task Init(MessageEntry favorite)
        {
            FavoriteEntry = favorite;
        }

        public async Task removeFromFavorites()
        {
            try
            {
                await _cache.InsertObject(FavoriteEntry.Clef.ToString(), FavoriteEntry, DateTimeOffset.Now.AddDays(2));


                var msgFavorites = await _cache.GetObject<List<int>>("Favorites");

                if (msgFavorites != null && msgFavorites.FindAll(item => item == FavoriteEntry.Clef).Count != 0)
                {
                    msgFavorites.RemoveAll(item => item == FavoriteEntry.Clef);
                }

                await _cache.InsertObject("Favorites", msgFavorites);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
