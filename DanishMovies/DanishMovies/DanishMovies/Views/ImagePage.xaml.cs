using DanishMovies.Controls;
using DanishMovies.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        private ImageViewModel _viewModel;

        public ImagePage()
        {
            InitializeComponent();
        }

        public ImagePage(ImageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
    }
}