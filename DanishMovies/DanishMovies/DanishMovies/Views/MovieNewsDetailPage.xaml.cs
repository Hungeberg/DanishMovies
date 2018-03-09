using DanishMovies.Models;
using DanishMovies.ViewModels;
using I18NPortable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieNewsDetailPage : ContentPage
    {
        private MovieNewsDetailViewModel _viewModel;

        public MovieNewsDetailPage()
        {
            InitializeComponent();
        }

        public MovieNewsDetailPage(MovieNewsDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;

            if (_viewModel.HasImage)
            {
                if (_viewModel.NewsItem.Source == MovieNewsProviderType.FilmNyt.ToString().ToUpper())
                {
                    NewsImage.HeightRequest = 50;
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = "Movie_News".Translate();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back".Translate();
        }
    }
}