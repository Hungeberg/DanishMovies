using DanishMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private SearchViewModel _viewModel;

        public SearchPage()
        {
            InitializeComponent();

            // Bind view model to view.
            BindingContext = _viewModel = new SearchViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Hide cancel button on iOS since it has its own.
            if (Device.RuntimePlatform == Device.iOS)
            {
                SearchSearchBar.WidthRequest = Width;
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                SearchSearchBar.HeightRequest = 35;
                SearchSearchBar.WidthRequest = Width;
            }

            if (_viewModel.SearchResults.Count == 0)
            {
                // Focus the search bar so keyboard pops up.
                SearchSearchBar.Focus();
            }

            // To workaround problem with initial progress color.
            SearchResultListView.BeginRefresh();
            SearchResultListView.EndRefresh();            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SearchResultListView.SelectedItem = null;
        }
    }
}