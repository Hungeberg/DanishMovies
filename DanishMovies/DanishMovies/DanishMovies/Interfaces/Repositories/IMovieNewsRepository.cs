using DanishMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Repositories
{
    public interface IMovieNewsRepository
    {
        Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false, MovieNewsProviderType providerType = MovieNewsProviderType.Dfi);
    }
}
