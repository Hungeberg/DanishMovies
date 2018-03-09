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
    public class MovieNewsViewModel : BaseViewModel
    {
        private IDataService _dataService;

        private int _selectIndex;
        public int SelectIndex
        {
            get { return _selectIndex; }
            set
            {
                SetProperty(ref _selectIndex, value);
                LoadMovieNews((MovieNewsProviderType)_selectIndex);
            }
        }

        private ObservableCollection<MovieNews> _news;
        public ObservableCollection<MovieNews> News
        {
            get { return _news; }
            set
            {
                _news = value;
                OnPropertyChanged();
            }
        }

        private ICommand _showNewsDetailCommand;
        public ICommand ShowNewsDetailCommand
        {
            get
            {
                return _showNewsDetailCommand ?? (_showNewsDetailCommand =
                    new Command(async (n) =>
                    {
                        if (n != null)
                        {
                            var mn = n as MovieNews;

                            await Navigation.NavigateTo(
                                "MovieNewsDetailPage",
                                new MovieNewsDetailViewModel(mn));
                        }
                    }));
            }
        }

        public MovieNewsViewModel()
        {
            _dataService = new DataService();
            SelectIndex = 0;
        }

        #region PUBLIC METHODS

        public Task LoadMovieNews(MovieNewsProviderType newsType)
        {
            return Task.Run(async () =>
            {
                if (IsBusy) return; // Ignore repetitive calls!

                IsBusy = true; // Set busy flag.

                try
                {
                    var news = new ObservableCollection<MovieNews>();
                    var movieNews = await _dataService.GetMovieNewsAsync(false, newsType);

                    foreach (var n in movieNews)
                    {
                        news.Add(n);
                    }
                    News = news;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false; // Clear busy flag.
                }
            });
        }

        #endregion
    }
}
