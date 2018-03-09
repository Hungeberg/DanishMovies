using DanishMovies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanishMovies.Services
{
    public class NavigationService : INavigationService
    {
        private IDictionary<string, Type> _pages;
        private string _currentPageKey;

        public INavigation Navigation
        {
            get
            {
                return ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation;
            }
        }

        public NavigationService(IDictionary<string, Type> pages)
        {
            _pages = pages;
        }

        #region INavigationService implementation

        public async Task GoBack(bool toRoot = false, bool toSearch = false)
        {
            if (toRoot)
            {
                await Navigation.PopToRootAsync(true);
            }
            else if (toSearch)
            {
                // Register all the pages that we need to remove...
                var pagesToRemove = new List<Page>();
                for (int i = 2; i < Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = Navigation.NavigationStack[i];
                    pagesToRemove.Add(page);
                }

                // Lets remove all pages between search and current page...
                foreach (var p in pagesToRemove)
                {
                    Navigation.RemovePage(p);
                }

                // Lets pop the current page so we get a nice return to search.
                await Navigation.PopAsync(true);
            }
            else
            {
                await Navigation.PopAsync(true);
            }
        }

        public async Task NavigateTo(string pageKey)
        {
            await NavigateTo(pageKey, null);
        }

        public async Task NavigateTo(string pageKey, object parameter)
        {
            try
            {
                object[] parameters = null;
                if (parameter != null)
                {
                    parameters = new object[] { parameter };
                }
                Page displayPage = (Page)Activator.CreateInstance(_pages[pageKey], parameters);
                _currentPageKey = pageKey;
                CurrentPage = displayPage;
                await Navigation.PushAsync(displayPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task NavigateTo(string pageKey, object[] parameters)
        {
            try
            {
                Page displayPage = (Page)Activator.CreateInstance(_pages[pageKey], parameters);
                _currentPageKey = pageKey;
                CurrentPage = displayPage;
                await Navigation.PushAsync(displayPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public string CurrentPageKey
        {
            get
            {
                return _currentPageKey;
            }
        }

        public Page CurrentPage { get; set; }

        #endregion
    }
}
