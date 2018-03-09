using DanishMovies.ViewModels;

namespace DanishMovies
{
    public static class ViewModelLocator
    {
        private static PersonViewModel _personViewModel;
        public static PersonViewModel PersonViewModel =>
            _personViewModel ?? (_personViewModel = new PersonViewModel());

        private static MovieViewModel _movieViewModel;
        public static MovieViewModel MovieViewModel =>
            _movieViewModel ?? (_movieViewModel = new MovieViewModel());

        private static MoviePremiereViewModel _moviePremiereViewModel;
        public static MoviePremiereViewModel MoviePremiereViewModel =>
            _moviePremiereViewModel ?? (_moviePremiereViewModel = new MoviePremiereViewModel());

        private static CompanyViewModel _companyViewModel;
        public static CompanyViewModel CompanyViewModel =>
            _companyViewModel ?? (_companyViewModel = new CompanyViewModel());

        private static DescriptionViewModel _descriptionViewModel;
        public static DescriptionViewModel DescriptionViewModel =>
            _descriptionViewModel ?? (_descriptionViewModel = new DescriptionViewModel());

        private static InfoListViewModel _infoListViewModel;
        public static InfoListViewModel InfoListViewModel =>
            _infoListViewModel ?? (_infoListViewModel = new InfoListViewModel());

        private static ImageViewModel _imageViewModel;
        public static ImageViewModel ImageViewModel =>
            _imageViewModel ?? (_imageViewModel = new ImageViewModel());

        private static MovieNewsDetailViewModel _movieNewsDetailViewModel;
        public static MovieNewsDetailViewModel MovieNewsDetailViewModel =>
            _movieNewsDetailViewModel ?? (_movieNewsDetailViewModel = new MovieNewsDetailViewModel());

        private static WebViewModel _webViewModel;
        public static WebViewModel WebViewModel =>
            _webViewModel ?? (_webViewModel = new WebViewModel());
    }
}
