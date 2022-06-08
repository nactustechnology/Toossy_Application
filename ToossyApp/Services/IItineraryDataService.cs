using ToossyApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToossyApp.Services
{
    public interface IItineraryDataService
    {
        //Task<TripLogApiAuthToken> GetAuthTokenAsync(string idProvider, string idProviderToken);
        Task<List<ItineraryEntry>> GetItineraryEntriesAsync(ItineraryFilter filter, GeoCoords myPosition);
        Task<MessageEntry> GetMessageEntryAsync(int messageClef);
        Task<ItineraryComment> AddCommentAsync(ItineraryComment comment);
        Task<ItineraryReport> AddReportAsync(ItineraryReport report);
        Task<TransactionResponse> transaction(ItineraryCharge charge);
        Task<List<SelectedItem>> GetTokenResult(string researchedToken);
    }
}
