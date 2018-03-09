using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanishMovies.Interfaces.Services
{
    public interface INavigationService
    {
        Task GoBack(bool toRoot = false, bool toSearch = false);
        Task NavigateTo(string pageKey);
        Task NavigateTo(string pageKey, object parameter);
        Task NavigateTo(string pageKey, object[] parameters);
        string CurrentPageKey { get; }
        Page CurrentPage { get; set; }
    }
}
