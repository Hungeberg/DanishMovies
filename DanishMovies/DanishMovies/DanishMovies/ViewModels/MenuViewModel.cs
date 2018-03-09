using DanishMovies.Utility;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DanishMovies.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand RateWebCommand { get; }

        public MenuViewModel()
        {
            RateWebCommand = new Command(() => 
                Device.OpenUri(new Uri(RateHelper.GetRateUrl())));
        }
    }
}