using DanishMovies.Interfaces.Repositories;
using DanishMovies.Models;
using DanishMovies.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Repositories
{
    public class PersonRepository : BaseRepository, IRepository<PersonInfo, SearchResult>
    {
        private const string PERSON_INFO_ENDPOINT = @"person/{id}";
        private const string PERSON_SEARCH_ENDPOINT = @"person?name={searchstring}";

        public async Task<PersonInfo> GetInfoAsync(int id)
        {
            // Prepare the url...
            var personInfoUrl = PERSON_INFO_ENDPOINT.Replace("{id}", id.ToString());

            // Get the data...
            var personInfo = await GetAsync<PersonInfo>(personInfoUrl);

            // Trim text data...
            if (personInfo != null)
            {
                personInfo.Description = !string.IsNullOrEmpty(personInfo.Description)
                    ? WebStringHelper.StripHtml(personInfo.Description)
                    : string.Empty;
            }

            // Return the result.
            return personInfo;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string searchText)
        {
            var result = new List<SearchResult>();

            // Prepare the url...
            var searchUrl = PERSON_SEARCH_ENDPOINT.Replace("{searchstring}", $"%{searchText}%");

            // Get the data...
            var searchData = await GetAsync<SearchData>(searchUrl);

            // Full name search ?
            if (searchData.ResultCount == 0 && searchText.Contains(" "))
            {
                var lastName = searchText.Substring(searchText.LastIndexOf(" ")).Trim();
                var firstName = searchText.Substring(0, searchText.Length - lastName.Length).Trim();
                searchUrl = PERSON_SEARCH_ENDPOINT
                    .Replace("name=", "firstname=")
                    .Replace("{searchstring}", $"{firstName}&lastname={lastName}");
                searchData = await GetAsync<SearchData>(searchUrl);
            }

            // Create search result instances for all the found data list items...
            foreach (var d in searchData.PersonList)
            {
                var searchResult = new SearchResult
                {
                    SearchType = SearchTypes.Person,
                    Id = d.Id,
                    Name = d.Name,
                    FirstName = d.FirstName,
                    LastName = d.LastName
                };
                result.Add(searchResult);
            }

            // Return the search result collection...
            return result;
        }
    }
}
