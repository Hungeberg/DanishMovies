using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class WebViewModel : BaseViewModel
    {
        private string _webUrl;
        public string WebUrl
        {
            get { return _webUrl; }
            set { SetProperty(ref _webUrl, value); }
        }

        private ICommand _openInWebBrowserCommand;
        public ICommand OpenInWebBrowserCommand => 
            _openInWebBrowserCommand ?? (_openInWebBrowserCommand =
                new Command(() => 
                {

                }));

        public WebViewModel()
        {
            WebUrl = "http://www.dfi.dk/Nyheder/FILMupdate/2017/November/Premiere-paa-Thelma.aspx";
        }

        public WebViewModel(string url)
        {
            WebUrl = url;
        }
    }
}
