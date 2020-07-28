using MozillaObservatory.Serializables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MozillaObservatory
{
    public class ObservatoryClient : IDisposable
    {
        #region Initialisation
        public const string MOZILLA_PRODUCTION_ENDPOINT = "https://http-observatory.security.mozilla.org/api/v1/";

        public Uri ApiEndpoint { get; private set; }

        HttpClient _httpClient;

        public ObservatoryClient(string apiEndpoint)
        {
            _httpClient = new HttpClient();
            ApiEndpoint = new Uri(apiEndpoint) ?? throw new ArgumentNullException(nameof(apiEndpoint));
        }

        public ObservatoryClient() : this(MOZILLA_PRODUCTION_ENDPOINT)
        {
        }
        #endregion

        #region API functions
        public async Task<Scan> InvokeAccessment(string host, bool hideFromPublicResults = false, bool forceRescan = false)
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "analyze"))
            {
                Query = $"host={Uri.EscapeUriString(host)}&hidden={hideFromPublicResults}&rescan={forceRescan}"
            };
            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);
            // TODO: error handling
            return JsonConvert.DeserializeObject<Scan>(responseContent);
        }

        public async Task<Scan> GetAccessment(string host, bool hideFromPublicResults, bool forceRescan)
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "analyze"))
            {
                Query = $"host={Uri.EscapeUriString(host)}"
            };
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("hidden", hideFromPublicResults.ToString()),
                new KeyValuePair<string, string>("rescan", forceRescan.ToString())
            });
            var response = await _httpClient.PostAsync(uriBuilder.Uri, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            // TODO: error handling
            return JsonConvert.DeserializeObject<Scan>(responseContent);
        }

        public async Task<TestResults> GetScanResults(long scanId)
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "getScanResults"))
            {
                Query = $"scan={scanId}"
            };
            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);
            // TODO: error handling
            return JsonConvert.DeserializeObject<TestResults>(responseContent);
        }

        public async Task<IList<RecentScan>> GetRecentScans(long? minimumScore = null, long? maximumScore = null)
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "getRecentScans"));
            if (minimumScore != null && maximumScore != null)
                uriBuilder.Query = $"min={minimumScore}&max={maximumScore}";
            else if (minimumScore != null)
                uriBuilder.Query = $"min={minimumScore}";
            else if (maximumScore != null)
                uriBuilder.Query = $"max={maximumScore}";

            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);
            // TODO: error handling
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            return dict.Select(x => new RecentScan
            {
                Hostname = x.Key,
                Grade = x.Value.ToEnum<Grades>()
            }).ToList();
        }

        public async Task<IList<HistoricalScan>> GetHostHistory(string host)
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "getHostHistory"))
            {
                Query = $"host={Uri.EscapeUriString(host)}"
            };

            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);
            // TODO: error handling
            return JsonConvert.DeserializeObject<IList<HistoricalScan>>(responseContent);
        }

        public async Task<Dictionary<Grades, long>> GetGradeDistribution()
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "getGradeDistribution"));
            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);

            // TODO: error handling
            return JsonConvert.DeserializeObject<Dictionary<Grades, long>>(responseContent);
        }

        public async Task<Dictionary<ScanStates, long>> GetScannerStates()
        {
            var uriBuilder = new UriBuilder(new Uri(ApiEndpoint, "getScannerStates"));
            var responseContent = await _httpClient.GetStringAsync(uriBuilder.Uri);

            // TODO: error handling
            return JsonConvert.DeserializeObject<Dictionary<ScanStates, long>>(responseContent);
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
