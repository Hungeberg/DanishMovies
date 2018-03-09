using DanishMovies.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {
        private DescriptionViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, 
        // parameterless constructor to render a page.
        public DescriptionPage()
        {
            InitializeComponent();
        }

        public DescriptionPage(DescriptionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back";
        }
    }
}