using DanishMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    public enum MenuType
    {
        HomeMenu,
        AboutMenu
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        private Color _unselectColor = (Color)Application.Current.Resources["BackgroundColor"];
        private Color _selectColor = (Color)Application.Current.Resources["LighterBackgroundColor"];
        private TapGestureRecognizer _homeTapped;
        private TapGestureRecognizer _aboutTapped;
        private NavigationPage _homeNavPage;
        private NavigationPage _aboutNavPage;
        private MenuType _currentMenuType;

        public RootPage()
        {
            InitializeComponent();

            // Initialize menu pages...
            _homeNavPage = new NavigationPage(new MainPage());
            _aboutNavPage = new NavigationPage(new AboutPage(new AboutViewModel()));
            Detail = _homeNavPage;
            _currentMenuType = MenuType.HomeMenu;
            App.Navigation.CurrentPage = _homeNavPage;

            // Initialize menu event handlers...
            _homeTapped = new TapGestureRecognizer();
            _homeTapped.Tapped += HomeTapped;
            menuPage.HomeMenuItem.GestureRecognizers.Add(_homeTapped);
            _aboutTapped = new TapGestureRecognizer();
            _aboutTapped.Tapped += AboutTapped;
            menuPage.AboutMenuItem.GestureRecognizers.Add(_aboutTapped);
        }

        private void HomeTapped(object sender, System.EventArgs e)
        {
            menuPage.AboutMenuItem.BackgroundColor = _unselectColor;
            menuPage.HomeMenuItem.BackgroundColor = _selectColor;
            if (_currentMenuType != MenuType.HomeMenu) // Not Home?
            {
                // Switch to home page...
                _currentMenuType = MenuType.HomeMenu;
                Detail = _homeNavPage;
                App.Navigation.CurrentPage = _homeNavPage;
            }
            IsPresented = false;
        }

        private void AboutTapped(object sender, System.EventArgs e)
        {
            menuPage.AboutMenuItem.BackgroundColor = _selectColor;
            menuPage.HomeMenuItem.BackgroundColor = _unselectColor;
            if (_currentMenuType != MenuType.AboutMenu) // Not About?
            {
                // Switch to about page...
                _currentMenuType = MenuType.AboutMenu;
                Detail = _aboutNavPage;
                App.Navigation.CurrentPage = _aboutNavPage;
            }
            IsPresented = false;
        }
    }
}