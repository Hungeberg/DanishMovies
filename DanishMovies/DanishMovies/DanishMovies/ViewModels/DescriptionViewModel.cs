using DanishMovies.Models;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class DescriptionViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private bool _hasImage;
        public bool HasImage
        {
            get { return _hasImage; }
            set { SetProperty(ref _hasImage, value); }
        }

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        private ICommand _showImageCommand;
        public ICommand ShowImageCommand => _showImageCommand ?? (_showImageCommand =
            new Command(async () => await Navigation.NavigateTo(
                "ImagePage", 
                new ImageViewModel(Name, ImageUrl))));

        public DescriptionViewModel()
        {
            Name = DesignDataHelper.GetName();
            Description = DesignDataHelper.GetDescription();
            ImageUrl = DesignDataHelper.GetImageUrl(SearchTypes.na);
            HasImage = !string.IsNullOrEmpty(ImageUrl);
        }

        public DescriptionViewModel(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            HasImage = !string.IsNullOrEmpty(ImageUrl);
        }
    }
}
