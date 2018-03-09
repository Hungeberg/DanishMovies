using DanishMovies.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoListPage : ContentPage
    {
        private InfoListViewModel _viewModel;

        public InfoListPage()
        {
            InitializeComponent();
        }

        public InfoListPage(InfoListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            InfoListView.SelectedItem = null;
        }
    }
}