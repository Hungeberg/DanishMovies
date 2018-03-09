using DanishMovies.Models;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class MoviePremiereViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _hasComment;
        public bool HasComment
        {
            get { return _hasComment; }
            set { SetProperty(ref _hasComment, value); }
        }

        private bool _hasTheaters;
        public bool HasTheaters
        {
            get { return _hasTheaters; }
            set { SetProperty(ref _hasTheaters, value); }
        }

        private MoviePremiereInfo _premiereInfo;
        public MoviePremiereInfo Premiere
        {
            get { return _premiereInfo; }
            set { SetProperty(ref _premiereInfo, value); }
        }

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        public MoviePremiereViewModel()
        {
            Title = DesignDataHelper.GetName();
            Premiere = DesignDataHelper.GetMoviePremiereInfo();
            HasComment = !string.IsNullOrEmpty(Premiere.PremiereComment);
            HasTheaters = Premiere.PremiereTheatres != null && Premiere.PremiereTheatres.Count > 0;
        }

        public MoviePremiereViewModel(string title, MoviePremiereInfo premiereInfo)
        {
            Title = title;
            Premiere = premiereInfo;
            HasComment = !string.IsNullOrEmpty(Premiere.PremiereComment);
            HasTheaters = Premiere.PremiereTheatres != null && Premiere.PremiereTheatres.Count > 0;
        }
    }
}
