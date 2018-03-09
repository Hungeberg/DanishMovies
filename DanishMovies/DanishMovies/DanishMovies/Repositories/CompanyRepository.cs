using System.Collections.Generic;
using System.Threading.Tasks;
using DanishMovies.Interfaces.Repositories;
using DanishMovies.Models;
using DanishMovies.Utility;

namespace DanishMovies.Repositories
{
    public class CompanyRepository : BaseRepository, IRepository<CompanyInfo, SearchResult>
    {
        private const string COMPANY_INFO_STRING = @"company/{id}";
        private const string COMPANY_SEARCH_STRING = @"company?name={searchstring}";

        public async Task<CompanyInfo> GetInfoAsync(int id)
        {
            // Prepare the url...
            var companyInfoUrl = COMPANY_INFO_STRING.Replace("{id}", id.ToString());

            // Get the data...
            var companyInfo = await GetAsync<CompanyInfo>(companyInfoUrl);

            // Trim text data...
            if (companyInfo != null)
            {
                companyInfo.Description = !string.IsNullOrEmpty(companyInfo.Description)
                    ? WebStringHelper.StripHtml(companyInfo.Description)
                    : string.Empty;
                companyInfo.ImagesUrl = $"https://api.dfi.dk/v1/images/{companyInfo.Id}";
            }

            // Return the result.
            return companyInfo;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string searchText)
        {
            var result = new List<SearchResult>();

            // Prepare the url...
            var searchUrl = COMPANY_SEARCH_STRING.Replace("{searchstring}", $"%{searchText}%");

            // Get the data...
            var searchData = await GetAsync<SearchData>(searchUrl);

            // Create search result instances for all the found data list items...
            foreach (var d in searchData.CompanyList)
            {
                var searchResult = new SearchResult
                {
                    SearchType = SearchTypes.Company,
                    Id = d.Id,
                    Name = d.Name
                };
                result.Add(searchResult);
            }

            // Return the search result collection...
            return result;
        }
    }
}
