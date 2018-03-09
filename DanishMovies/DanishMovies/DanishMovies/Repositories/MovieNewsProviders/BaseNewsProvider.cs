using DanishMovies.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using DanishMovies.Models;
using System.IO;

namespace DanishMovies.Repositories.MovieNewsProviders
{
    public abstract class BaseNewsProvider : IMovieNewsProvider
    {
        public abstract IEnumerable<MovieNews> GetMovieNews(Stream stream);
        public abstract Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false);
    }
}
