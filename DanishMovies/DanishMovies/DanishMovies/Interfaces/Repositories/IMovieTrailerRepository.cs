using DanishMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Repositories
{
    public interface IMovieTrailerRepository
    {
        Task<IEnumerable<MovieTrailer>> GetMovieTrailersAsync();
    }
}
