using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DanishMovies.Interfaces.Repositories;
using DanishMovies.Models;
using DanishMovies.Utility;

namespace DanishMovies.Repositories
{
    public class MovieRepository : BaseRepository, IRepository<MovieInfo, SearchResult>
    {
        private const string MOVIE_INFO_STRING = @"film/{id}";
        private const string MOVIE_SEARCH_STRING = @"film?title={searchstring}";

        public async Task<MovieInfo> GetInfoAsync(int id)
        {
            // Prepare the url...
            var movieInfoUrl = MOVIE_INFO_STRING.Replace("{id}", id.ToString());

            // Get the data...
            var movieInfo = await GetAsync<MovieInfo>(movieInfoUrl, enableContentCorrection:true);

            // Trim text data...
            if (movieInfo != null)
            {
                movieInfo.PremiereInfo = movieInfo.Premiere?.FirstOrDefault(x => x.PremiereType == "bp");
                movieInfo.Description = !string.IsNullOrEmpty(movieInfo.Description)
                    ? WebStringHelper.StripHtml(movieInfo.Description)
                    : string.Empty;
                movieInfo.Comment = !string.IsNullOrEmpty(movieInfo.Comment)
                    ? WebStringHelper.StripHtml(movieInfo.Comment)
                    : string.Empty;
            }

            // Return the result.
            return movieInfo;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string searchText)
        {
            var result = new List<SearchResult>();

            // Prepare the url...
            var searchUrl = MOVIE_SEARCH_STRING.Replace("{searchstring}", searchText);

            // Get the data...
            var searchData = await GetAsync<SearchData>(searchUrl);

            // Retry with wild cards if we did not get any data in first try...
            if (searchData.ResultCount == 0)
            {
                searchUrl = MOVIE_SEARCH_STRING.Replace("{searchstring}", $"%_{searchText}%");
                searchData = await GetAsync<SearchData>(searchUrl);
            }

            // Create search result instances for all the found data list items...
            foreach (var d in searchData.FilmList)
            {
                var searchResult = new SearchResult
                {
                    SearchType = SearchTypes.Movie,
                    Id = d.Id,
                    Name = d.Title,
                    ReleaseYear = d.ReleaseYear
                };
                result.Add(searchResult);
            }

            // Return the search result collection...
            return result;
        }
    }
}
