using Autofac;
using CarouselView.FormsPlugin.Abstractions;
using DanishMovies.Interfaces.Services;
using DanishMovies.Services;
using DanishMovies.Views;
using DanishMovies.ViewModels;
using I18NPortable;
using Plugin.MediaManager.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DanishMovies
{
    public partial class App : Application
    {
        private const string LAST_REFRESH_TIME = "last_refresh_time";
        private const int REFRESH_INTERVAL_IN_MIN = 5;
        public static DateTime LastRefresh = DateTime.Now.AddMinutes(-REFRESH_INTERVAL_IN_MIN);
        private static INavigationService _navigation;
        public static INavigationService Navigation => GetNavigation();

        private static INavigationService GetNavigation()
        {
            if (_navigation == null)
            {
                _navigation = _container.Resolve<INavigationService>();
            }
            return _navigation;
        }

        private static IContainer _container;

        public App()
        {
            // Make sure it doesn't get stripped away by the linker
            var workaround = typeof(VideoView);
            var workaround2 = typeof(CarouselViewControl);

            InitializeComponent();

            // Setup i18n localization...
            InitializeLocalization();

            // Setup IoC container...
            PrepareContainer();

            // Show main page...
            MainPage = new RootPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await SetRefreshStateAsync(true);
        }

        protected override void OnResume()
        {
            base.OnResume();
            GetRefreshState();
        }

        private static void PrepareContainer()
        {
            var containerBuilder = new ContainerBuilder();

            var navigationService = new NavigationService(
                new Dictionary<string, Type>()
                {
                    { "SearchPage", typeof(SearchPage) },
                    { "PersonPage", typeof(PersonPage) },
                    { "MoviePage", typeof(MoviePage) },
                    { "MoviePremierePage", typeof(MoviePremierePage) },
                    { "CompanyPage", typeof(CompanyPage) },
                    { "DescriptionPage", typeof(DescriptionPage) },
                    { "InfoListPage", typeof(InfoListPage) },
                    { "ImagePage", typeof(ImagePage) },
                    { "VideoPage", typeof(VideoPage) },
                    { "MovieNewsDetailPage", typeof(MovieNewsDetailPage) },
                    { "MovieNewsPage", typeof(MovieNewsPage) },
                    { "WebPage", typeof(WebPage) }
                });

            containerBuilder.RegisterInstance(navigationService).As<INavigationService>();

            _container = containerBuilder.Build();
        }

        private void InitializeLocalization()
        {
            I18N.Current
                .SetFallbackLocale("en") // Optional but recommended: locale to load in case the system locale is not supported
#if DEBUG
                .SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
#endif
                .SetLogger(text => Debug.WriteLine(text)) // action to output traces
                .Init(GetType().GetTypeInfo().Assembly); // assembly where locales live
        }

        private void GetRefreshState()
        {
            if (Current.Properties.ContainsKey(LAST_REFRESH_TIME))
            {
                var lastRefresh = Current.Properties[LAST_REFRESH_TIME] as DateTime?;
                if (lastRefresh != null)
                {
                    LastRefresh = ((DateTime)lastRefresh);
                }
            }
        }

        public static bool IsRefresh()
        {
            return (DateTime.Now - LastRefresh).TotalMinutes > REFRESH_INTERVAL_IN_MIN;
        }

        public static async Task SetRefreshStateAsync(bool isStartup = false)
        {
            if (isStartup)
            {
                LastRefresh = DateTime.Now.AddMinutes(-REFRESH_INTERVAL_IN_MIN);
            }
            else
            {
                LastRefresh = DateTime.Now;
            }
            Current.Properties["last_refresh_time"] = LastRefresh;
            await Current.SavePropertiesAsync();
        }
    }
}