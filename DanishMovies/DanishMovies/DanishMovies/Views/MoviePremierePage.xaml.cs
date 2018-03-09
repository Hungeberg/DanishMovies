using DanishMovies.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviePremierePage : ContentPage
    {
        private MoviePremiereViewModel _viewModel;

        public MoviePremierePage()
        {
            InitializeComponent();
        }

        public MoviePremierePage(MoviePremiereViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
    }
}