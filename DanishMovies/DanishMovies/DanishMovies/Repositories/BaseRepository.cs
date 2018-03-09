using DanishMovies.Utility;
using I18NPortable;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DanishMovies.Repositories
{
    public class BaseRepository
    {
        private const string BASE_URL = "https://api.dfi.dk";
        private const string USER = "PHMO";
        private const string PASS = "W7qzK695";
        private const string LANG_DK = "da-DK";
        private const string LANG_EN = "en-US";
        private const string VERSION = "v1";

        private HttpClient CreateHttpsClient()
        {
            var httpClient = new HttpClient();
            httpClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient
                .DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue(
                    "Basic", 
                    StringHelper.Base64Encode($"{USER}:{PASS}"));
            var language = I18N.Current.Locale != LANG_DK ? LANG_EN : LANG_DK;
            httpClient
                .DefaultRequestHeaders
                .AcceptLanguage
                .Add(new StringWithQualityHeaderValue(language));
            return httpClient;
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        /// <summary>
        /// Get data from a given Web API url
        /// </summary>
        /// <typeparam name="T">The data type to convert JSON data into</typeparam>
        /// <param name="url">The Web API endpoint url</param>
        /// <returns>Data object or objects from JSON data</returns>
        protected async Task<T> GetAsync<T>(string url, bool isFullUrl = false, bool enableContentCorrection = false) where T : new()
        {
            var httpsClient = CreateHttpsClient();
            T result;
            var apiUrl = url;
            if (!isFullUrl)
            {
                apiUrl = $"{BASE_URL}/{VERSION}/{url}";
            }

            try
            {
                var response = await httpsClient.GetStringAsync(apiUrl);

                if (enableContentCorrection) // Workaround to fix certain content...
                {
                    //
                    // Fix premiere item content.
                    //
                    const string PREMIERE_ITEM_TEXT = "\"Premiere\":{";
                    if (response.Contains(PREMIERE_ITEM_TEXT))
                    {
                        // -1 on index here because we want to insert array open bracket 
                        // before the open brace.
                        var startIndex = response.IndexOf(PREMIERE_ITEM_TEXT) + (PREMIERE_ITEM_TEXT.Length-1);
                        // +2 on index here because we want to insert array close bracket
                        // after the close brace and its 2 because we are 
                        // inserting 2 characters in total.
                        var endIndex = response.IndexOf("}", startIndex) + 2;

                        // Insert the content in the right places based on the indexes
                        // calculated above. 
                        response = response.Insert(startIndex, "[");
                        response = response.Insert(endIndex, "]");
                    }
                }
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get data!\nURL:\n{apiUrl}\nException:\n{ex.Message}");
                result = new T();
            }
            return result;
        }

        /// <summary>
        /// Get plain text data (HTML etc) from a given url
        /// </summary>
        /// <param name="url">The url to load</param>
        /// <returns>Raw string data</returns>
        protected async Task<string> GetAsync(string url)
        {
            var httpClient = CreateHttpClient();
            var result = string.Empty;

            try
            {
                result = await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load html source!\nURL:\n{url}\nException:\n{ex.Message}");
            }
            return result;
        }
    }
}
