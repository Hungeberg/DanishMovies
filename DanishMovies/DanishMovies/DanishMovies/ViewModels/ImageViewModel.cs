using DanishMovies.Models;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        public ImageViewModel()
        {
            Name = DesignDataHelper.GetName();
            ImageUrl = DesignDataHelper.GetImageUrl(SearchTypes.Movie);
        }

        public ImageViewModel(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
