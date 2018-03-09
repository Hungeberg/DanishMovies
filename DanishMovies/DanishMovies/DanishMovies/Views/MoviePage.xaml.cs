using DanishMovies.Models;
using DanishMovies.ViewModels;
using I18NPortable;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviePage : ContentPage
    {
        private MovieViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, 
        // parameterless constructor to render a page.
        public MoviePage()
        {
            InitializeComponent();
        }

        public MoviePage(MovieViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        #region EVENT HANDLERS

        private void MovieImageTapped(object sender, EventArgs e)
        {
            if (_viewModel.HasImage)
            {
                _viewModel
                    .ShowImageCommand
                    .Execute(new ImageItem
                    {
                        Caption = _viewModel.Movie.Title,
                        PathMini = _viewModel.Movie.ImageUrl
                    });
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "";

            if (_viewModel.Movie == null)
            {
                await _viewModel.LoadMovieInfo();
                UpdateView(_viewModel.Movie, _viewModel.ShortDescription, _viewModel.HasImage);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back".Translate();
        }

        #endregion

        private void UpdateView(MovieInfo movie, string shortDescription, bool hasImage)
        {
            if (movie == null) return;
            
            MovieMainView.IsVisible = true;
        }
    }
}