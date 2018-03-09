using CarouselView.FormsPlugin.iOS;
using Foundation;
using Plugin.MediaManager.Forms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: ResolutionGroupName("DanishMovies")]
namespace DanishMovies.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // SearchBar styling
            UISearchBar.Appearance.BarTintColor = UIColor.FromRGB(7, 7, 7);
            UISearchBar.Appearance.BackgroundColor = UIColor.FromRGB(7, 7, 7);

            // NavigationBar styling
            UINavigationBar
                .Appearance
                .SetTitleTextAttributes(
                    new UITextAttributes()
                    {
                        Font = UIFont.FromName("HelveticaNeue-CondensedBlack", 21)
                    });

            // RefreshControl styling
            UIRefreshControl.Appearance.TintColor = UIColor.White;

            // TableView styling
            UITableView.Appearance.SeparatorColor = UIColor.FromRGB(7, 7, 7);

            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();
            VideoViewRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
