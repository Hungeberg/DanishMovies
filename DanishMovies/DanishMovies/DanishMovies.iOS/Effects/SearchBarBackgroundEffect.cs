using DanishMovies.iOS.Effects;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(SearchBarBackgroundEffect), "SearchBarBackgroundEffect")]
namespace DanishMovies.iOS.Effects
{
    public class SearchBarBackgroundEffect : PlatformEffect
    {
        UIColor _backgroundColor;

        protected override void OnAttached()
        {
            _backgroundColor = UIColor.FromRGB(30, 30, 30);
            var searchBar = ((UISearchBar)Control);
            //searchBar.EnablesReturnKeyAutomatically = true;
            var searchField = (UITextField)searchBar
                .ValueForKey(new Foundation.NSString("_searchField"));
            searchField.BackgroundColor = _backgroundColor;
            searchField.TextColor = UIColor.White;
            searchField.AttributedPlaceholder = new Foundation.NSAttributedString(
                searchBar.Placeholder, 
                foregroundColor: UIColor.White, 
                font: UIFont.FromName("HelveticaNeue", 16));
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            /*if (args.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                if (Control.BackgroundColor == _backgroundColor)
                {
                    Control.BackgroundColor = UIColor.FromRGB(28, 28, 28);
                }
                else
                {
                    Control.BackgroundColor = _backgroundColor;
                }
            }*/
        }

        protected override void OnDetached()
        {
            
        }
    }
}
