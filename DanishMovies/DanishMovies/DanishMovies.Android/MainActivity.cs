using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Plugin.MediaManager.Forms.Android;

namespace DanishMovies.Droid
{
    [Activity(
        Label = "Danish Movies", 
        Icon = "@drawable/icon", 
        Theme = "@style/MyTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CarouselViewRenderer.Init();
            VideoViewRenderer.Init();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}