using DanishMovies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using I18NPortable;
using DanishMovies.Utility;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel _viewModel;

        public AboutPage()
        {
            InitializeComponent();
        }

        public AboutPage(AboutViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            var appName = "Danish_Movies".Translate();
            AppNameLabel.Text = $"{appName} v{VersionHelper.GetAppVersion()}";
            AppInfoLabel.Text = "app_written".Translate();
            AppCodeShareLabel.Text = "code_share".Translate();
            AppDfiUseLabel.Text = "dfi_use".Translate();
        }
    }
}
