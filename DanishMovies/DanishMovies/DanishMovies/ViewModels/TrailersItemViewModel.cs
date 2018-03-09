using DanishMovies.Models;
using I18NPortable;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class TrailersItemViewModel : BaseViewModel
    {
        private ICommand _playTrailerCommand;
        public ICommand PlayTrailerCommand
        {
            get
            {
                return _playTrailerCommand ?? (_playTrailerCommand =
                    new Command(async (t) =>
                    {
                        if (t != null)
                        {
                            var mt = t as MovieTrailer;

                            await Navigation.NavigateTo(
                                "VideoPage",
                                new VideoViewModel(mt.VideoUrl));
                        }
                    }));
            }
        }

        private ICommand _showMovieInfoCommand;
        public ICommand ShowMovieInfoCommand
        {
            get
            {
                return _showMovieInfoCommand ?? (_showMovieInfoCommand =
                    new Command(async (t) =>
                    {
                        if (t != null)
                        {
                            var mt = t as MovieTrailer;

                            if (mt.Id > 0)
                            {
                                await Navigation.NavigateTo(
                                    "MoviePage",
                                    new MovieViewModel(mt.Id));
                            }
                            else
                            {
                                await Navigation.CurrentPage.DisplayAlert(
                                    "Sorry".Translate(),
                                    "There is no movie info for this movie yet.".Translate(),
                                    "OK");
                            }
                        }
                    }));
            }
        }
    }
}
