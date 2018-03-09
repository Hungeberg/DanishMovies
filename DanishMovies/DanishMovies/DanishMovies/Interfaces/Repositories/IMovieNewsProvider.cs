using DanishMovies.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Repositories
{
    public interface IMovieNewsProvider
    {
        Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false);
        IEnumerable<MovieNews> GetMovieNews(Stream stream);
    }
}
