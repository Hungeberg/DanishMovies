using Version.Plugin;
using Xamarin.Forms;

namespace DanishMovies.Utility
{
    public static class VersionHelper
    {
        /// <summary>
        /// Get the App version like "1.0, 1.1" etc.
        /// </summary>
        /// <returns>App version</returns>
        public static string GetAppVersion()
        {
            var version = CrossVersion.Current.Version;

            if (Device.RuntimePlatform == Device.iOS)
            {
                version = version.Replace(".1.0", "");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                version = version.Replace(".0.0", "");
            }
            return version;
        }
    }
}