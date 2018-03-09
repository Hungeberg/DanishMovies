using DanishMovies.Utility;
using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand XamarinWebCommand { get; }
        public ICommand DfiWebCommand { get; }
        public ICommand RateWebCommand { get; }

        public AboutViewModel()
        {
            XamarinWebCommand = new Command(() => 
                Device.OpenUri(new Uri("https://xamarin.com/platform")));
            DfiWebCommand = new Command(() => 
                Device.OpenUri(new Uri("http://www.dfi.dk")));
            RateWebCommand = new Command(() => 
                Device.OpenUri(new Uri(RateHelper.GetRateUrl())));
        }
    }
}