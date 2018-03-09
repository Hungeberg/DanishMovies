using DanishMovies.Models;
using DanishMovies.ViewModels;
using I18NPortable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyPage : ContentPage
    {
        private CompanyViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, 
        // parameterless constructor to render a page.
        public CompanyPage()
        {
            InitializeComponent();
        }

        public CompanyPage(CompanyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private void CompanyImageTapped(object sender, System.EventArgs e)
        {
            if (_viewModel.HasImage)
            {
                _viewModel
                    .ShowImageCommand
                    .Execute(new ImageItem
                    {
                        Caption = _viewModel.Company.Name,
                        PathMini = _viewModel.Company.ImageUrl
                    });
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "";

            if (_viewModel.Company == null)
            {
                await _viewModel.LoadCompanyInfo();
                CompanyMainView.IsVisible = true;

                if (!_viewModel.HasDescription && _viewModel.HasImage)
                {
                    CompanyImage.WidthRequest = Width;
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back".Translate();
        }
    }
}