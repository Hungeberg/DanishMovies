namespace DanishMovies.ViewModels
{
    public class VideoViewModel : BaseViewModel
    {
        private string _videoUrl;
        public string VideoUrl
        {
            get { return _videoUrl; }
            set { SetProperty(ref _videoUrl, value); }
        }

        public VideoViewModel(string videoUrl)
        {
            VideoUrl = videoUrl;
        }
    }
}
