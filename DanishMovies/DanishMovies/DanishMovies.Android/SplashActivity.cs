using Android.App;
using Android.OS;

namespace DanishMovies.Droid
{
    [Activity(
        Theme = "@style/MyTheme.Splash", 
        MainLauncher = true, 
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
        }
    }
}