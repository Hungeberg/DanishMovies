using System;
using DanishMovies.Interfaces.Services;
using DanishMovies.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;
using DanishMovies.Utility;
using DanishMovies.Models;

namespace DanishMovies.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        private IDataService _searchService;
        private readonly int _movieId;

        #region PROPERTIES

        private MovieInfo _movie;
        public MovieInfo Movie
        {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        private bool _hasInfo;
        public bool HasInfo
        {
            get { return _hasInfo; }
            set { SetProperty(ref _hasInfo, value); }
        }

        private bool _hasDescription;
        public bool HasDescription
        {
            get { return _hasDescription; }
            set { SetProperty(ref _hasDescription, value); }
        }

        private bool _hasImage;
        public bool HasImage
        {
            get { return _hasImage; }
            set { SetProperty(ref _hasImage, value); }
        }

        private bool _hasImages;
        public bool HasImages
        {
            get { return _hasImages; }
            set { SetProperty(ref _hasImages, value); }
        }

        private bool _hasReleaseYear;
        public bool HasReleaseYear
        {
            get { return _hasReleaseYear; }
            set { SetProperty(ref _hasReleaseYear, value); }
        }

        private bool _hasMovieLength;
        public bool HasMovieLength
        {
            get { return _hasMovieLength; }
            set { SetProperty(ref _hasMovieLength, value); }
        }

        private bool _hasPremiereInfo;
        public bool HasPremiereInfo
        {
            get { return _hasPremiereInfo; }
            set { SetProperty(ref _hasPremiereInfo, value); }
        }

        private bool _hasCategory;
        public bool HasCategory
        {
            get { return _hasCategory; }
            set { SetProperty(ref _hasCategory, value); }
        }

        private bool _hasMovieTypes;
        public bool HasMovieTypes
        {
            get { return _hasMovieTypes; }
            set { SetProperty(ref _hasMovieTypes, value); }
        }

        private bool _hasOriginalTitle;
        public bool HasOriginalTitle
        {
            get { return _hasOriginalTitle; }
            set { SetProperty(ref _hasOriginalTitle, value); }
        }

        private bool _hasComment;
        public bool HasComment
        {
            get { return _hasComment; }
            set { SetProperty(ref _hasComment, value); }
        }

        private bool _hasMovieFormat;
        public bool HasMovieFormat
        {
            get { return _hasMovieFormat; }
            set { SetProperty(ref _hasMovieFormat, value); }
        }

        private bool _hasClassification;
        public bool HasClassification
        {
            get { return _hasClassification; }
            set { SetProperty(ref _hasClassification, value); }
        }

        private bool _hasCountries;
        public bool HasCountries
        {
            get { return _hasCountries; }
            set { SetProperty(ref _hasCountries, value); }
        }

        private bool _hasCredits;
        public bool HasCredits
        {
            get { return _hasCredits; }
            set { SetProperty(ref _hasCredits, value); }
        }

        private bool _hasPersonCredits;
        public bool HasPersonCredits
        {
            get { return _hasPersonCredits; }
            set { SetProperty(ref _hasPersonCredits, value); }
        }

        private bool _hasProductionCompanies;
        public bool HasProductionCompanies
        {
            get { return _hasProductionCompanies; }
            set { SetProperty(ref _hasProductionCompanies, value); }
        }

        private bool _hasDistributionCompanies;
        public bool HasDistributionCompanies
        {
            get { return _hasDistributionCompanies; }
            set { SetProperty(ref _hasDistributionCompanies, value); }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { SetProperty(ref _shortDescription, value); }
        }

        #endregion

        #region COMMANDS

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        private ICommand _showDescriptionCommand;
        public ICommand ShowDescriptionCommand => _showDescriptionCommand ?? (_showDescriptionCommand =
            new Command<MovieInfo>(async (m) => await Navigation.NavigateTo(
                "DescriptionPage",
                new DescriptionViewModel(m.Title, m.MovieDescription, m.ImageUrl))));

        private ICommand _showCreditsCommand;
        public ICommand ShowCreditsCommand => _showCreditsCommand ?? (_showCreditsCommand =
            new Command<MovieInfo>(async (m) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(m.PersonCredits, SearchTypes.Person))));

        private ICommand _showProductionCompaniesCommand;
        public ICommand ShowProductionCompaniesCommand => _showProductionCompaniesCommand ?? (_showProductionCompaniesCommand =
            new Command<MovieInfo>(async (m) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(m.ProductionCompanies, SearchTypes.Company))));

        private ICommand _showDistributionCompaniesCommand;
        public ICommand ShowDistributionCompaniesCommand => _showDistributionCompaniesCommand ?? (_showDistributionCompaniesCommand =
            new Command<MovieInfo>(async (m) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(m.DistributionCompanies, SearchTypes.Company))));

        private ICommand _showPremiereCommand;
        public ICommand ShowPremiereCommand => _showPremiereCommand ?? (_showPremiereCommand =
            new Command<MovieInfo>(async (m) => await Navigation.NavigateTo(
                "MoviePremierePage",
                new MoviePremiereViewModel(m.Title, m.PremiereInfo))));

        private ICommand _showImageCommand;
        public ICommand ShowImageCommand => _showImageCommand ?? (_showImageCommand =
            new Command<ImageItem>(async (i) =>
            {
                if (i != null)
                {
                    await Navigation.NavigateTo(
                        "ImagePage",
                        new ImageViewModel(i.Caption, i.PathMini));
                }
            }));

        #endregion

        #region CONSTRUCTORS

        public MovieViewModel()
        {
            Movie = DesignDataHelper.GetMovieInfo();
            HasImage = !string.IsNullOrEmpty(Movie.ImageUrl);
            ShortDescription = StringHelper
                .BuildShortString(Movie.MovieDescription, 
                                  HasImage,
                                  Device.Idiom != TargetIdiom.Phone);
            HasDescription = !string.IsNullOrEmpty(ShortDescription);
            HasInfo = HasImage || HasDescription;
            HasImages = Movie.Images?.Count > 0;
            HasCategory = !string.IsNullOrEmpty(Movie.Category);
            HasClassification = !string.IsNullOrEmpty(Movie.Classification);
            HasComment = !string.IsNullOrEmpty(Movie.Comment);
            HasCountries = !string.IsNullOrEmpty(Movie.Countries);
            HasMovieFormat = !string.IsNullOrEmpty(Movie.FilmFormat);
            HasMovieLength = Movie.LengthInMin > 0;
            HasMovieTypes = !string.IsNullOrEmpty(Movie.MovieTypes);
            HasOriginalTitle = !string.IsNullOrEmpty(Movie.OriginalTitle);
            HasPremiereInfo = Movie.PremiereInfo != null;
            HasReleaseYear = Movie.ReleaseYear > 0;
            HasPersonCredits = Movie.PersonCredits?.Count > 0;
            HasProductionCompanies = Movie.ProductionCompanies?.Count > 0;
            HasDistributionCompanies = Movie.DistributionCompanies?.Count > 0;
            HasCredits = HasPersonCredits || HasProductionCompanies || HasDistributionCompanies;
        }

        public MovieViewModel(int movieId)
        {
            _searchService = new DataService();
            _movieId = movieId;
        }

        #endregion

        #region PUBLIC METHODS

        public Task LoadMovieInfo()
        {
            return Task.Run(async () =>
            {
                if (IsBusy) return; // Ignore repetitive calls!

                IsBusy = true; // Set busy flag.

                try
                {
                    Movie = await _searchService.GetMovieAsync(_movieId);
                    HasImage = !string.IsNullOrEmpty(Movie.ImageUrl);
                    ShortDescription = StringHelper
                        .BuildShortString(Movie.MovieDescription, 
                                          HasImage,
                                          Device.Idiom != TargetIdiom.Phone);
                    HasDescription = !string.IsNullOrEmpty(ShortDescription);
                    HasInfo = HasImage || HasDescription;
                    HasImages = Movie.Images?.Count > 0;
                    HasCategory = !string.IsNullOrEmpty(Movie.Category);
                    HasClassification = !string.IsNullOrEmpty(Movie.Classification);
                    HasComment = !string.IsNullOrEmpty(Movie.Comment);
                    HasCountries = !string.IsNullOrEmpty(Movie.Countries);
                    HasMovieFormat = !string.IsNullOrEmpty(Movie.FilmFormat);
                    HasMovieLength = Movie.LengthInMin > 0;
                    HasMovieTypes = !string.IsNullOrEmpty(Movie.MovieTypes);
                    HasOriginalTitle = !string.IsNullOrEmpty(Movie.OriginalTitle);
                    HasPremiereInfo = Movie.PremiereInfo != null;
                    HasReleaseYear = Movie.ReleaseYear > 0;
                    HasPersonCredits = Movie.PersonCredits?.Count > 0;
                    HasProductionCompanies = Movie.ProductionCompanies?.Count > 0;
                    HasDistributionCompanies = Movie.DistributionCompanies?.Count > 0;
                    HasCredits = HasPersonCredits || HasProductionCompanies || HasDistributionCompanies;
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
