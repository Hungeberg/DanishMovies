namespace DanishMovies.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new DanishMovies.App());
        }
    }
}