using DanishMovies.ViewModels;
using I18NPortable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MainViewModel();
            SizeChanged += MainPage_SizeChanged;
        }

        private async void MainPage_SizeChanged(object sender, System.EventArgs e)
        {
            if (App.IsRefresh())
            {
                await _viewModel.LoadMovieTrailers();
                await _viewModel.LoadMovieNews();
                await App.SetRefreshStateAsync();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "Danish_Movies".Translate();

            if (App.IsRefresh())
            {
                if (Device.RuntimePlatform != Device.UWP)
                {
                    await _viewModel.LoadMovieTrailers();
                    await _viewModel.LoadMovieNews();
                }
                await App.SetRefreshStateAsync();
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                await _viewModel.LoadMovieTrailers();
                MovieTrailersCarousel.ShowArrows = true;
                await _viewModel.LoadMovieNews();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back".Translate();
        }
    }
}