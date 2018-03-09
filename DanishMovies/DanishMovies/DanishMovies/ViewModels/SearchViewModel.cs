using DanishMovies.Interfaces.Services;
using DanishMovies.Models;
using DanishMovies.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private IDataService _searchService;

        public SearchViewModel()
        {
            _searchService = new DataService();
        }

        public ObservableCollection<SearchResult> SearchResults { get; set; } = 
            new ObservableCollection<SearchResult>();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = 
                    new Command(async () => await ExecuteSearchCommand(SearchText)));
            }
        }

        private ICommand _cancelSearchCommand;
        public ICommand CancelSearchCommand
        {
            get
            {
                return _cancelSearchCommand ?? (_cancelSearchCommand =
                    new Command(async () => await Navigation.GoBack(true)));
            }
        }

        private ICommand _itemSelectedCommand;
        public ICommand ItemSelectedCommand
        {
            get
            {
                return _itemSelectedCommand ?? (_itemSelectedCommand =
                    new Command<SearchResult>(async (s) =>
                    {
                        if (s == null) return;

                        switch (s.SearchType)
                        {
                            case SearchTypes.na:
                            case SearchTypes.Movie:
                                await Navigation.NavigateTo(
                                    "MoviePage",
                                    new MovieViewModel(s.Id));
                                break;
                            case SearchTypes.Person:
                                await Navigation.NavigateTo(
                                    "PersonPage",
                                    new PersonViewModel(s.Id));
                                break;
                            case SearchTypes.Company:
                                await Navigation.NavigateTo(
                                    "CompanyPage",
                                    new CompanyViewModel(s.Id));
                                break;
                            default:
                                break;
                        }
                    }));
            }
        }

        private async Task ExecuteSearchCommand(string searchText)
        {
            if (IsBusy) return; // Ignore repetitive calls!

            IsBusy = true; // Set busy flag.

            try
            {
                SearchResults.Clear(); // Clear any previous search result...

                // Search using the search service...
                var searchResult = await _searchService.SearchAsync(searchText);

                // Update search results collection...
                foreach (var s in searchResult)
                {
                    SearchResults.Add(s);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false; // Clear busy flag.
            }
        }
    }
}
