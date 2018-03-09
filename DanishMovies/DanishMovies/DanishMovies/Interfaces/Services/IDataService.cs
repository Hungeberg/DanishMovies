using DanishMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Services
{
    public interface IDataService
    {
        Task<IEnumerable<SearchResult>> SearchAsync(string searchText);
        Task<PersonInfo> GetPersonAsync(int id);
        Task<MovieInfo> GetMovieAsync(int id);
        Task<CompanyInfo> GetCompanyAsync(int id);
        string GetImageUrl(IList<ImageItem> images);
        Task<ImageItems> GetImagesAsync(string sourceUrl, bool isPerson = false);
        Task<IEnumerable<MovieTrailer>> GetMovieTrailersAsync();
        Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false, MovieNewsProviderType providerType = MovieNewsProviderType.Dfi);
    }
}
