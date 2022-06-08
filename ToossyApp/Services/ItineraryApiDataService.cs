using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToossyApp.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace ToossyApp.Services
{
    //BaseHttpService,
    public class ItineraryApiDataService : BaseHttpService, IItineraryDataService
    {
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;

        public ItineraryApiDataService(Uri baseUri)
        {
            _baseUri = baseUri;

            _headers = new Dictionary<string, string>();
            //_headers.Add("Host", "vps433240.ovh.net:3240");
            _headers.Add("Host", _baseUri.Host+":"+ _baseUri.Port);
            _headers.Add("Connection", "Close");
            _headers.Add("Accept", "application/json");
        }

        public async Task<MessageEntry> GetMessageEntryAsync(int messageClef)
        {
            var url = new Uri(_baseUri, string.Format("/API/v1/message/{0}", messageClef));
            var response = await SendRequestAsync<Object[]>(url, HttpMethod.Get, _headers);

            MessageEntry result = new MessageEntry((JObject)response[0]);

            return result;
        }

        private double[] getBoundingBox(double pLatitude, double pLongitude, double pDistanceInMeters)
        {
            double[] boundingBox = new double[4];

            double latRadian = pLatitude * Math.PI / 180;

            double degLatKm = 110.574235;
            double degLongKm = 110.572833 * Math.Cos(latRadian);
            double deltaLat = pDistanceInMeters / degLatKm;
            double deltaLong = pDistanceInMeters / degLongKm;

            double minLat = pLatitude - deltaLat;
            double minLong = pLongitude - deltaLong;
            double maxLat = pLatitude + deltaLat;
            double maxLong = pLongitude + deltaLong;

            boundingBox[0] = minLat;
            boundingBox[1] = minLong;
            boundingBox[2] = maxLat;
            boundingBox[3] = maxLong;

            return boundingBox;
        }


        public async Task<List<ItineraryEntry>> GetItineraryEntriesAsync(ItineraryFilter filter, GeoCoords myPosition)
        {
            var limitBox = getBoundingBox(myPosition.Latitude, myPosition.Longitude, double.Parse(filter.FiltreDistance[0], new CultureInfo("en-US")));

            string[] queryParameter = new String[10] {
                    "theme="+System.Net.WebUtility.UrlEncode(String.Join(",",filter.FiltreTheme)),
                    "duree="+System.Net.WebUtility.UrlEncode( String.Join(",",filter.FiltreDuree)),
                    "typeparcours="+System.Net.WebUtility.UrlEncode( String.Join(",",filter.FiltreTypeParcours)),
                    "note="+System.Net.WebUtility.UrlEncode( String.Join(",",filter.FiltreNote)),
                    "tarif="+System.Net.WebUtility.UrlEncode(String.Join(",", filter.FiltreGratuitTarif)),
                    "langue="+System.Net.WebUtility.UrlEncode( String.Join(",",filter.FiltreLangue)),
                    "stltt="+limitBox[0].ToString(),
                    "endltt="+limitBox[2].ToString(),
                    "stlgt="+limitBox[1].ToString(),
                    "endlgt="+limitBox[3].ToString()
                };

            var URLqueryParameter = String.Join("&", queryParameter);
            var url = new Uri(_baseUri, string.Format("/API/v1/itineraries?{0}", URLqueryParameter));


            var response = await SendRequestAsync<Object[]>(url, HttpMethod.Get, _headers);

            var itineraryList = new List<ItineraryEntry>();

            foreach (JObject res in response)
            {
                ItineraryEntry result = new ItineraryEntry(res);

                itineraryList.Add(result);
            }

            return itineraryList;
        }

        public async Task<ItineraryComment> AddCommentAsync(ItineraryComment comment)
        {
            try
            {
                var url = new Uri(_baseUri, "/API/v1/comment");
                var response = await SendRequestAsync<ItineraryComment>(url, HttpMethod.Post, _headers, comment);

                return response;
            }
            catch (Exception ex)
            {
                return new ItineraryComment();
            }
        }

        public async Task<ItineraryReport> AddReportAsync(ItineraryReport report)
        {
            try
            {
                var url = new Uri(_baseUri, "/API/v1/report");
                var response = await SendRequestAsync<ItineraryReport>(url, HttpMethod.Post, _headers, report);

                return response;
            }
            catch (Exception ex)
            {
                return new ItineraryReport();
            }
        }

        public async Task<TransactionResponse> transaction(ItineraryCharge charge)
        {
            try
            {
                var url = new Uri(_baseUri, "/API/v1/transaction");
                var response = await SendRequestAsync<TransactionResponse>(url, HttpMethod.Post, _headers, charge);


                return response;
            }
            catch (Exception ex)
            {
                return new TransactionResponse { Status = "failed" };
            }
        }

        public async Task<List<SelectedItem>> GetTokenResult(string researchedToken)
        {
            var url = new Uri(_baseUri, string.Format("/API/v1/subscriptionChecking?researchedToken={0}", researchedToken.ToString()));
            var response = await SendRequestAsync<Object[]>(url, HttpMethod.Get, _headers);

            var tokenList = new List<SelectedItem>();

            foreach (JObject res in response)
            {
                SelectedItem result = new SelectedItem(res);

                tokenList.Add(result);
            }

            return tokenList;

        }
    }
}
