using DanishMovies.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        private WebViewModel _viewModel;
        private bool isFirstLoad = true;
        private bool isSecondLoad = false;

        public WebPage()
        {
            InitializeComponent();
        }

        public WebPage(WebViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private void WebOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            Browser.IsVisible = false;
            ProgressIndicator.IsVisible = true;
            ProgressIndicator.IsRunning = true;
            BackImage.IsEnabled = false;
            ForwardImage.IsEnabled = false;
            BackImage.Opacity = 0.1;
            ForwardImage.Opacity = 0.1;
        }

        private void WebOnEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            ProgressIndicator.IsVisible = false;
            ProgressIndicator.IsRunning = false;
            if (isFirstLoad)
            {
                BackImage.IsEnabled = false;
                isFirstLoad = false;
                isSecondLoad = true;
            }
            else
            {
                if (isSecondLoad)
                {
                    BackImage.IsEnabled = true;
                    isSecondLoad = false;
                }
                else
                {
                    BackImage.IsEnabled = Browser.CanGoBack;
                }
            }
            ForwardImage.IsEnabled = Browser.CanGoForward;
            BackImage.Opacity = BackImage.IsEnabled ? 1.0 : 0.1;
            ForwardImage.Opacity = ForwardImage.IsEnabled ? 1.0 : 0.1;
            Browser.IsVisible = true;
        }

        private void BackTapped(object sender, EventArgs e)
        {
            Browser.GoBack();
        }

        private void ForwardTapped(object sender, EventArgs e)
        {
            Browser.GoForward();
        }
    }
}