using Xamarin.Forms.Platform.Android.AppCompat;
using Xamarin.Forms;
using DanishMovies.Droid.Renderers;
using Android.Graphics;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace DanishMovies.Droid.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                _toolbar = (Android.Support.V7.Widget.Toolbar)child;
                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();

            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var font = Typeface.Create("sans-serif-condensed", TypefaceStyle.Bold);
                textView.Typeface = font;
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}