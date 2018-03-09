using DanishMovies.Models;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class MovieNewsDetailViewModel : BaseViewModel
    {
        private MovieNews _newsItem;
        public MovieNews NewsItem
        {
            get { return _newsItem; }
            set { SetProperty(ref _newsItem, value); }
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
                new ImageViewModel(NewsItem.Headline, NewsItem.ImageUrl))));

        private ICommand _showMovieNewsWebCommand;
        public ICommand ShowMovieNewsWebCommand => _showMovieNewsWebCommand ?? (_showMovieNewsWebCommand =
            new Command(async () => await Navigation.NavigateTo(
                "WebPage",
                new WebViewModel(NewsItem.StoryUrl))));

        public MovieNewsDetailViewModel()
        {
            NewsItem = DesignDataHelper.GetMovieNewsItem();
            HasImage = !string.IsNullOrEmpty(NewsItem.ImageUrl);
        }

        public MovieNewsDetailViewModel(MovieNews movieNews)
        {
            NewsItem = movieNews;
            HasImage = !string.IsNullOrEmpty(NewsItem.ImageUrl);
        }
    }
}
