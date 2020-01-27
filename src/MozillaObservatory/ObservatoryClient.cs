using MozillaObservatory.Serializables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task<Scan> Analyze(string host, bool hideFromPublicResults, bool forceRescan)
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
