using System;
using DanishMovies.Interfaces.Services;
using DanishMovies.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;
using DanishMovies.Utility;
using I18NPortable;
using DanishMovies.Models;

namespace DanishMovies.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private IDataService _searchService;
        private readonly int _personId;

        #region PRIVATE METHODS

        private FormattedString BuildFormattedAge(int yearOfBirth, int yearOfDeath, int age)
        {
            var born = yearOfBirth > 0 ? yearOfBirth.ToString() : "";
            var dead = yearOfDeath > 0 ? yearOfDeath.ToString() : "";
            var agePrefix = age > 0
                ? yearOfDeath > 0
                    ? $" {"aged".Translate()} "
                    : $" {"age".Translate()} "
                : "";

            var result = new FormattedString
            {
                Spans =
                {
                    new Span { Text = yearOfBirth > 0 ? "(" : "", FontSize=13, ForegroundColor=Color.Gray },
                    new Span { Text = born, FontSize=13, ForegroundColor=Color.Gray },
                    new Span { Text = yearOfBirth > 0 ? " - " : "", FontSize=13, ForegroundColor=Color.Gray },
                    new Span { Text = dead, FontSize=13, ForegroundColor=Color.Gray },
                    new Span { Text = yearOfBirth > 0 ? ")" : "", FontSize=13, ForegroundColor=Color.Gray },
                    new Span { Text = agePrefix, FontSize=14, ForegroundColor=Color.Gray },
                    new Span { Text = age > 0 ? age.ToString() : "", FontSize=14, ForegroundColor=Color.Gray }
                }
            };
            return result;
        }

        #endregion

        #region PROPERTIES

        private PersonInfo _person;
        public PersonInfo Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        private bool _hasInfo;
        public bool HasInfo
        {
            get { return _hasInfo; }
            set { SetProperty(ref _hasInfo, value); }
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

        private bool _hasMovies;
        public bool HasMovies
        {
            get { return _hasMovies; }
            set { SetProperty(ref _hasMovies, value); }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { SetProperty(ref _shortDescription, value); }
        }

        private FormattedString _formattedAge;
        public FormattedString FormattedAge
        {
            get { return _formattedAge; }
            set { SetProperty(ref _formattedAge, value); }
        }

        #endregion

        #region COMMANDS

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand = 
            new Command(async () => await Navigation.GoBack(false, true)));

        private ICommand _showDescriptionCommand;
        public ICommand ShowDescriptionCommand => _showDescriptionCommand ?? (_showDescriptionCommand =
            new Command<PersonInfo>(async (p) => await Navigation.NavigateTo(
                "DescriptionPage", 
                new DescriptionViewModel(p.Name, p.Description, p.ImageUrl))));

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

        private ICommand _showMovieListCommand;
        public ICommand ShowMovieListCommand => _showMovieListCommand ?? (_showMovieListCommand =
            new Command<PersonInfo>(async (p) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(p.FilmCredits, SearchTypes.Movie))));

        #endregion

        #region CONSTRUCTORS

        public PersonViewModel()
        {
            Person = DesignDataHelper.GetPersonInfo();
            //HasImage = false;
            HasImage = !string.IsNullOrEmpty(Person.ImageUrl);
            HasImages = Person.Images?.Count > 0;
            HasMovies = Person.FilmCredits?.Count > 0;
            ShortDescription = StringHelper
                .BuildShortString(Person.Description, 
                                  HasImage, 
                                  Device.Idiom != TargetIdiom.Phone);
            HasInfo = !string.IsNullOrEmpty(ShortDescription) ||
                      HasImage ||
                      Person.Age > 0;
            FormattedAge = BuildFormattedAge(Person.YearOfBirth, Person.YearOfDeath, Person.Age);
        }

        public PersonViewModel(int personId)
        {
            _searchService = new DataService();
            _personId = personId;
        }

        #endregion

        #region PUBLIC METHODS

        public Task LoadPersonInfo()
        {
            return Task.Run((async () =>
            {
                if (IsBusy) return; // Ignore repetitive calls!

                IsBusy = true; // Set busy flag.

                try
                {
                    Person = await _searchService.GetPersonAsync(_personId);
                    HasImage = !string.IsNullOrEmpty(Person.ImageUrl);
                    HasImages = Person.Images?.Count > 0;
                    HasMovies = Person.FilmCredits?.Count > 0;
                    ShortDescription = StringHelper
                        .BuildShortString(Person.Description, 
                                          HasImage,
                                          Device.Idiom != TargetIdiom.Phone);
                    HasInfo = !string.IsNullOrEmpty(ShortDescription) ||
                              HasImage ||
                              Person.Age > 0;
                    FormattedAge = BuildFormattedAge(Person.YearOfBirth, Person.YearOfDeath, Person.Age);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false; // Clear busy flag.
                }
            }));
        }

        #endregion
    }
}
