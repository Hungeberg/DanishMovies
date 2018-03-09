using DanishMovies.Utility;
using DanishMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public StackLayout HomeMenuItem { get { return homeMenuItem; } }
        public StackLayout AboutMenuItem { get { return aboutMenuItem; } }

        public MenuPage()
        {
            InitializeComponent();
            AppVersionLabel.Text = $"Version {VersionHelper.GetAppVersion()}";
            BindingContext = new MenuViewModel();
        }
    }
}