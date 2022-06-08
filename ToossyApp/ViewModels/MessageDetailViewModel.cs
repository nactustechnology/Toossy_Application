using System;
using System.Threading.Tasks;
using Akavache;
using ToossyApp.Models;
using ToossyApp.Services;
using System.Reactive.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ToossyApp.ViewModels
{
    public class MessageDetailViewModel : BaseViewModel<MessagePin>
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

        MessageEntry _messageEntry;
        public MessageEntry MessageEntry
        {
            get { return _messageEntry; }
            set
            {
                _messageEntry = value;
                OnPropertyChanged();
            }
        }

        bool _isTelechargeable;
        public bool IsTelechargeable
        {
            get { return _isTelechargeable; }
            set
            {
                _isTelechargeable = value;
                OnPropertyChanged();
            }
        }

        readonly IItineraryDataService _itineraryDataService;
        readonly IBlobCache _cache;

        public MessageDetailViewModel(INavService navService, IItineraryDataService itineraryDataService, IBlobCache cache) : base(navService, cache)
        {
            IsLoading = true;
            ImageIsLoading = true;

            _itineraryDataService = itineraryDataService;
            _cache = cache;


        }

        public override async Task Init(MessagePin messagePin)
        {
            try
            {
                IsLoading = true;

                int messageClef = messagePin.Id_Message;
                MessageEntry = await LoadMessage(messageClef);

                bool isMessageTelechargeable = messagePin.IsTelechargeable;
                IsTelechargeable = isMessageTelechargeable;



            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }



        async Task<MessageEntry> LoadMessage(int messageClef)
        {
            try
            {
                return await _cache.GetOrFetchObject(messageClef.ToString(), async () => await getMessageEntry(messageClef), DateTimeOffset.Now.AddDays(2));
            }
            catch (Exception ex)
            {
                return new MessageEntry();
            }
        }

        async Task<MessageEntry> getMessageEntry(int messageClef)
        {
            try
            {
                return await _itineraryDataService.GetMessageEntryAsync(messageClef).ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception ex)
            {
                return new MessageEntry();
            }
        }

        public async Task saveMessageToFavorites()
        {
            try
            {
                await _cache.InsertObject(MessageEntry.Clef.ToString(), MessageEntry);

                var msgFavorites = await _cache.GetOrCreateObject<List<int>>("Favorites", () => new List<int>());

                if (msgFavorites.FindAll(item => item == MessageEntry.Clef).Count == 0)
                {
                    msgFavorites.Add(MessageEntry.Clef);
                }

                await _cache.InsertObject("Favorites", msgFavorites);
            }
            catch (Exception ex)
            {

            }
        }

        Command<int> _goToReportPage;
        public Command<int> GoToReportPage
        {
            get
            {
                return _goToReportPage ?? (_goToReportPage = new Command<int>(async (messageId) => await GoToReportPageCommand(messageId)));
            }
        }

        public async Task GoToReportPageCommand(int messageId)
        {
            await NavService.NavigateTo<ReportViewModel, int>(messageId);
        }
    }
}
