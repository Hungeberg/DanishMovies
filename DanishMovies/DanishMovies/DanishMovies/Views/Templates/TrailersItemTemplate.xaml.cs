using DanishMovies.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrailersItemTemplate : ContentView
    {
        private TrailersItemViewModel _viewModel;

        public TrailersItemTemplate()
        {
            InitializeComponent();
            _viewModel = new TrailersItemViewModel();
            MoviePlayGrid.BindingContext = _viewModel;
            MovieInfoStack.BindingContext = _viewModel;
        }
    }
}