using DanishMovies.Interfaces.Services;
using DanishMovies.Models;
using DanishMovies.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private IDataService _dataService;

        bool isLoadTrailersBusy = false;
        public bool IsLoadTrailersBusy
        {
            get { return isLoadTrailersBusy; }
            set { SetProperty(ref isLoadTrailersBusy, value); }
        }

        bool isLoadNewsBusy = false;
        public bool IsLoadNewsBusy
        {
            get { return isLoadNewsBusy; }
            set { SetProperty(ref isLoadNewsBusy, value); }
        }

        private ObservableCollection<MovieTrailer> _trailers;
        public ObservableCollection<MovieTrailer> Trailers
        {
            get { return _trailers; }
            set
            {
                _trailers = value;
                OnPropertyChanged();
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

        private ICommand _showSearchCommand;
        public ICommand ShowSearchCommand
        {
            get
            {
                return _showSearchCommand ?? (_showSearchCommand =
                    new Command(async () =>
                    {
                        await Navigation.NavigateTo("SearchPage");
                    }));
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

        private ICommand _showMovieNewsCommand;
        public ICommand ShowMovieNewsCommand
        {
            get
            {
                return _showMovieNewsCommand ?? (_showMovieNewsCommand =
                    new Command(async () =>
                    {
                        await Navigation.NavigateTo(
                            "MovieNewsPage",
                            new MovieNewsViewModel());
                    }));
            }
        }

        public MainViewModel()
        {
            _dataService = new DataService();

            #region TRAILER DESIGN DATA

            /*var trailers = new List<MovieTrailer>()
            {
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Film2/V/Vold-i-k%E6rlighedens-navn.ashx?mw=386&mh=217&bc=white",
                    Title = "VOLD"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/mens-vi-lever-trailerstill.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 2"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/en-frygtelig-kvinde---still.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 3"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/Den%20k%E6mpestore%20p%E6re%20-%20still.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 4"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/V%E6bnet-med-ord-og-vinger-tr.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 5"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/En%20fremmed%20flytter%20ind%20still.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 6"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Global/Video/Qeda_trailerstill.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 7"
                },
                new MovieTrailer()
                {
                    ImageUrl = "http://www.dfi.dk/~/media/Sektioner/Filmhuset/2015/Kong-Christian-slide.ashx?mw=386&mh=217&bc=white",
                    Title = "NO. 8"
                }
            };

            foreach (var t in trailers)
            {
                Trailers.Add(t);
            }*/

            #endregion

            #region NEWS DESIGN DATA

            /*var news = new ObservableCollection<MovieNews>()
            {
                new MovieNews()
                {
                    ImageUrl = "person.png",
                    Headline = "New actor!",
                    Content = "A new actor is on the set for the new movie being shot...",
                    Author = "Movie News Media",
                    PublicationDate = new DateTime(2017, 11, 9)
                },
                new MovieNews()
                {
                    ImageUrl = "movie.png",
                    Headline = "New movie premiere!",
                    Content = "A new movie is being made and it will be very interesting...",
                    Author = "Movie News Media",
                    PublicationDate = new DateTime(2017, 11, 9)
                },
                new MovieNews()
                {
                    ImageUrl = "person.png",
                    Headline = "New actor!",
                    Content = "A new actor is on the set for the new movie being shot...",
                    Author = "Movie News Media",
                    PublicationDate = new DateTime(2017, 11, 9)
                },
                new MovieNews()
                {
                    ImageUrl = "movie.png",
                    Headline = "New movie premiere!",
                    Content = "A new movie is being made and it will be very interesting...",
                    Author = "Movie News Media",
                    PublicationDate = new DateTime(2017, 11, 9)
                }
            };
            News = news;*/

            #endregion
        }

        #region PUBLIC METHODS

        public Task LoadMovieTrailers()
        {
            return Task.Run(async () =>
            {
                if (IsLoadTrailersBusy) return; // Ignore repetitive calls!

                IsLoadTrailersBusy = true; // Set busy flag.

                try
                {
                    var trailers = new ObservableCollection<MovieTrailer>();
                    var movieTrailers = await _dataService.GetMovieTrailersAsync();

                    foreach (var t in movieTrailers)
                    {
                        trailers.Add(t);
                    }
                    //await Task.Delay(20000);
                    Trailers = trailers;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsLoadTrailersBusy = false; // Clear busy flag.
                }
            });
        }

        public Task LoadMovieNews()
        {
            return Task.Run(async () =>
            {
                if (IsLoadNewsBusy) return; // Ignore repetitive calls!

                IsLoadNewsBusy = true; // Set busy flag.

                try
                {
                    var news = new ObservableCollection<MovieNews>();
                    var movieNews = await _dataService.GetMovieNewsAsync();
                    movieNews = movieNews.Count() > 5 ? movieNews.Take(5) : movieNews;

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
                    IsLoadNewsBusy = false; // Clear busy flag.
                }
            });
        }

        #endregion
    }
}
