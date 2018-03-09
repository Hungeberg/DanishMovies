using Xamarin.Forms;

namespace DanishMovies.Utility
{
    public static class RateHelper
    {
        /// <summary>
        /// Get the Rate url for the current runtime platform.
        /// </summary>
        /// <returns>App Store Rate url</returns>
        public static string GetRateUrl()
        {
            var url = string.Empty;
            if (Device.RuntimePlatform == Device.iOS)
            {
                var appId = "1335092182";
                url = $"itms-apps://itunes.apple.com/app/id{appId}?action=write-review";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                var appId = "com.millersystems.danishmovies";
                url = $"https://play.google.com/store/apps/details?id={appId}";
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                var appId = "9MX42XZCKNL8";
                url = $"ms-windows-store://pdp/?ProductId={appId}";
            }
            return url;
        }
    }
}