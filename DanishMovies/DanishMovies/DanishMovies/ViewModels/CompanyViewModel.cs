using System;
using DanishMovies.Interfaces.Services;
using DanishMovies.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using DanishMovies.ViewModels.Design;
using System.Windows.Input;
using Xamarin.Forms;
using DanishMovies.Models;

namespace DanishMovies.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        private IDataService _searchService;
        private readonly int _companyId;

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

        private bool _hasDescription;
        public bool HasDescription
        {
            get { return _hasDescription; }
            set { SetProperty(ref _hasDescription, value); }
        }

        private bool _hasProductions;
        public bool HasProductions
        {
            get { return _hasProductions; }
            set { SetProperty(ref _hasProductions, value); }
        }

        private bool _hasDistributions;
        public bool HasDistributions
        {
            get { return _hasDistributions; }
            set { SetProperty(ref _hasDistributions, value); }
        }

        private bool _hasRequests;
        public bool HasRequests
        {
            get { return _hasRequests; }
            set { SetProperty(ref _hasRequests, value); }
        }

        private bool _hasFilmography;
        public bool HasFilmography
        {
            get { return _hasFilmography; }
            set { SetProperty(ref _hasFilmography, value); }
        }

        private CompanyInfo _company;
        public CompanyInfo Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
        }

        private ICommand _homeCommand;
        public ICommand HomeCommand => _homeCommand ?? (_homeCommand =
            new Command(async () => await Navigation.GoBack(false, true)));

        private ICommand _showProductionsCommand;
        public ICommand ShowProductionsCommand => _showProductionsCommand ?? (_showProductionsCommand =
            new Command<CompanyInfo>(async (c) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(c.Productions, SearchTypes.Movie))));

        private ICommand _showDistributionsCommand;
        public ICommand ShowDistributionsCommand => _showDistributionsCommand ?? (_showDistributionsCommand =
            new Command<CompanyInfo>(async (c) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(c.Distributions, SearchTypes.Movie))));

        private ICommand _showRequestsCommand;
        public ICommand ShowRequestsCommand => _showRequestsCommand ?? (_showRequestsCommand =
            new Command<CompanyInfo>(async (c) => await Navigation.NavigateTo(
                "InfoListPage",
                new InfoListViewModel(c.Requestor, SearchTypes.Movie))));

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

        public CompanyViewModel()
        {
            Company = DesignDataHelper.GetCompanyInfo();
            HasImage = !string.IsNullOrEmpty(Company.ImageUrl);
            HasDescription = !string.IsNullOrEmpty(Company.Description);
            HasInfo = HasImage || HasDescription;
            HasImages = Company.Images?.Count > 0;
            HasProductions = Company.Productions?.Count > 0;
            HasDistributions = Company.Distributions?.Count > 0;
            HasRequests = Company.Requestor?.Count > 0;
            HasFilmography = HasProductions || HasDistributions || HasRequests;
        }

        public CompanyViewModel(int companyId)
        {
            _searchService = new DataService();
            _companyId = companyId;
        }

        public Task LoadCompanyInfo()
        {
            return Task.Run(async () =>
            {
                if (IsBusy) return; // Ignore repetitive calls!

                IsBusy = true; // Set busy flag.

                try
                {
                    Company = await _searchService.GetCompanyAsync(_companyId);
                    HasImage = !string.IsNullOrEmpty(Company.ImageUrl);
                    HasDescription = !string.IsNullOrEmpty(Company.Description);
                    HasInfo = HasImage || HasDescription;
                    HasImages = Company.Images?.Count > 0;
                    HasProductions = Company.Productions?.Count > 0;
                    HasDistributions = Company.Distributions?.Count > 0;
                    HasRequests = Company.Requestor?.Count > 0;
                    HasFilmography = HasProductions || HasDistributions || HasRequests;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false; // Clear busy flag.
                }
            });
        }
    }
}
