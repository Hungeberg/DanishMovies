using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Repositories
{
    public interface IRepository<T, S>
    {
        Task<T> GetInfoAsync(int id);
        Task<IEnumerable<S>> SearchAsync(string searchText);
    }
}
