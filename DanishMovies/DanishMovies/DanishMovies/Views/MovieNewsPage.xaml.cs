using DanishMovies.Models;
using DanishMovies.ViewModels;
using I18NPortable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieNewsPage : ContentPage
    {
        private MovieNewsViewModel _viewModel;

        public MovieNewsPage()
        {
            InitializeComponent();
        }

        public MovieNewsPage(MovieNewsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
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
            NewsListView.SelectedItem = null;
        }
    }
}