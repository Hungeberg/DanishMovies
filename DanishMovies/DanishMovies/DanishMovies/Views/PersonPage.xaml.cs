using DanishMovies.Models;
using DanishMovies.ViewModels;
using I18NPortable;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonPage : ContentPage
    {
        private PersonViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, 
        // parameterless constructor to render a page.
        public PersonPage()
        {
            InitializeComponent();
        }

        public PersonPage(PersonViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        #region EVENT HANDLERS

        private void PersonImageTapped(object sender, EventArgs e)
        {
            if (_viewModel.HasImage)
            {
                _viewModel
                    .ShowImageCommand
                    .Execute(new ImageItem
                    {
                        Caption = _viewModel.Person.Name,
                        PathMini = _viewModel.Person.ImageUrl
                    });
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Title = "";

            if (_viewModel.Person == null)
            {
                await _viewModel.LoadPersonInfo();
                UpdateView(_viewModel.Person, _viewModel.ShortDescription, _viewModel.HasImage);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Title = "Back".Translate();
        }

        #endregion

        private void UpdateView(PersonInfo person, string shortDescription, bool hasImage)
        {
            if (person == null) return;

            PersonMainView.IsVisible = true;

            if (string.IsNullOrEmpty(shortDescription) &&
                person.Age > 0) // Only age?
            {
                PersonDescriptionLabel.IsVisible = false;
                PersonAgeLabel.VerticalOptions = LayoutOptions.StartAndExpand;
            }
        }
    }
}