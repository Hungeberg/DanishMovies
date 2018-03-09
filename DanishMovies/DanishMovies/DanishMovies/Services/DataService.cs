using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DanishMovies.Models;
using DanishMovies.Utility;
using DanishMovies.Repositories;
using DanishMovies.Interfaces.Services;
using DanishMovies.Interfaces.Repositories;

namespace DanishMovies.Services
{
    public class DataService : IDataService
    {
        private readonly IRepository<PersonInfo, SearchResult> _personRepo;
        private readonly IRepository<MovieInfo, SearchResult> _movieRepo;
        private readonly IRepository<CompanyInfo, SearchResult> _companyRepo;
        private readonly IImageRepository _imageRepo;
        private readonly IMovieTrailerRepository _movieTrailerRepo;
        private readonly IMovieNewsRepository _movieNewsRepo;

        public DataService()
        {
            _personRepo = new PersonRepository();
            _movieRepo = new MovieRepository();
            _companyRepo = new CompanyRepository();
            _imageRepo = new ImageRepository();
            _movieTrailerRepo = new MovieTrailerRepository();
            _movieNewsRepo = new MovieNewsRepository();
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return null;
            var result = new List<SearchResult>();

            // Encode the search text...
            var encodedSearchText = WebStringHelper.UrlEncodeSpecialCharacters(searchText);

            // Search movie characters (aka persons) ...
            result = (await _personRepo.SearchAsync(searchText)).ToList();

            // Search movies...
            var movieResult = (await _movieRepo.SearchAsync(searchText)).ToList();
            if (movieResult.Count > 0)
            {
                result.AddRange(movieResult);
            }

            // Search movie companies...
            var companyResult = (await _companyRepo.SearchAsync(searchText)).ToList();
            if (companyResult.Count > 0)
            {
                result.AddRange(companyResult);
            }

            // Order search result by name...
            result = result.OrderBy(x => x.Name).ToList();

            return result;
        }

        public async Task<PersonInfo> GetPersonAsync(int id)
        {
            var result = await _personRepo.GetInfoAsync(id);
            result.Images = _imageRepo.FilterImages(result.Images, true, result.Name);
            //result.Images = await GetImagesAsync(result.ImagesUrl, true);
            result.ImageUrl = GetImageUrl(result.Images);
            return result;
        }

        public async Task<MovieInfo> GetMovieAsync(int id)
        {
            var result = await _movieRepo.GetInfoAsync(id);
            result.Images = _imageRepo.FilterImages(result.Images);
            //result.Images = await GetImagesAsync(result.ImagesUrl);
            result.ImageUrl = GetImageUrl(result.Images);
            return result;
        }

        public async Task<CompanyInfo> GetCompanyAsync(int id)
        {
            var result = await _companyRepo.GetInfoAsync(id);
            result.Images = _imageRepo.FilterImages(result.Images);
            //result.Images = await GetImagesAsync(result.ImagesUrl);
            result.ImageUrl = GetImageUrl(result.Images);
            return result;
        }

        public string GetImageUrl(IList<ImageItem> images)
        {
            var image = images?
                .FirstOrDefault();
            var imageUrl = image?.PathMini;
            return imageUrl;
        }

        public async Task<ImageItems> GetImagesAsync(string sourceUrl, bool isPerson = false)
        {
            var images = await _imageRepo.GetImagesAsync(sourceUrl, isPerson);
            return images;
        }

        public async Task<IEnumerable<MovieTrailer>> GetMovieTrailersAsync()
        {
            var result = (await _movieTrailerRepo.GetMovieTrailersAsync()).ToList();
            return result;
        }

        public async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false, MovieNewsProviderType providerType = MovieNewsProviderType.Dfi)
        {
            var result = (await _movieNewsRepo.GetMovieNewsAsync(isEnglishLanguageSet, providerType)).ToList();
            return result;
        }
    }
}
