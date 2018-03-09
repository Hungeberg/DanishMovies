using DanishMovies.Utility;
using DanishMovies.ViewModels;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DanishMovies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        private bool _isControlsVisible = false;
        private VideoViewModel _viewModel;

        private IPlaybackController PlaybackController => 
            CrossMediaManager.Current.PlaybackController;

        public VideoPage ()
        {
            InitializeComponent ();
        }

        public VideoPage(VideoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            MediaVideoView.Source = null;
            await PlaybackController.Stop();
            CrossMediaManager.Current.StatusChanged += CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged += OnPlayingChanged;
            StatusLabel.Text = "Loading";

            var url = _viewModel.VideoUrl;

            // Download video file using url ...
            _viewModel.IsBusy = true;
            var videoFile = await VideoDownloadHelper.DownloadVideoFileAsync(url);
            _viewModel.IsBusy = false;

            MediaVideoView.Source = videoFile;
            //MediaVideoView.Source = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";
            if (string.IsNullOrEmpty(videoFile))
            {
                StatusLabel.Text = "Video download failed!";
            }

            /*PlayButton.Clicked += OnPlayClicked;
            StopButton.Clicked += OnStopClicked;
            PauseButton.Clicked += OnPauseClicked;*/
            MediaVideoView.AspectMode = VideoAspectMode.AspectFit;
            await ShowHideControls(true);
        }

        private void OnPlayingChanged(object sender, PlayingChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ProgressProgressBar.Progress = e.Progress;
            });
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await PlaybackController.Stop();
            /*PlayButton.Clicked -= OnPlayClicked;
            StopButton.Clicked -= OnStopClicked;
            PauseButton.Clicked -= OnPauseClicked;*/
            CrossMediaManager.Current.StatusChanged -= CurrentOnStatusChanged;
            CrossMediaManager.Current.PlayingChanged -= OnPlayingChanged;
            MediaVideoView.Source = null;
        }

        private void CurrentOnStatusChanged(object sender, StatusChangedEventArgs statusChangedEventArgs)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var status = statusChangedEventArgs.Status;
                switch (status)
                {
                    case MediaPlayerStatus.Stopped:
                        /*PauseButton.IsEnabled = false;
                        StopButton.IsEnabled = false;
                        PlayButton.IsEnabled = true;*/
                        PauseButton.IsVisible = false;
                        StopButton.IsVisible = false;
                        PlayButton.IsVisible = true;
                        StatusLabel.Text = "Stopped";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Paused:
                        /*PauseButton.IsEnabled = false;
                        StopButton.IsEnabled = true;
                        PlayButton.IsEnabled = true;*/
                        PauseButton.IsVisible = false;
                        StopButton.IsVisible = true;
                        PlayButton.IsVisible = true;
                        StatusLabel.Text = "Paused";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Playing:
                        /*PauseButton.IsEnabled = true;
                        StopButton.IsEnabled = true;
                        PlayButton.IsEnabled = false;*/
                        PauseButton.IsVisible = true;
                        StopButton.IsVisible = true;
                        PlayButton.IsVisible = false;
                        await StatusLabel.FadeTo(0);
                        break;
                    case MediaPlayerStatus.Loading:
                        /*PauseButton.IsEnabled = false;
                        StopButton.IsEnabled = false;
                        PlayButton.IsEnabled = false;*/
                        PauseButton.IsVisible = false;
                        StopButton.IsVisible = false;
                        PlayButton.IsVisible = false;
                        StatusLabel.Text = "Loading";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Buffering:
                        /*PauseButton.IsEnabled = false;
                        StopButton.IsEnabled = false;
                        PlayButton.IsEnabled = false;*/
                        PauseButton.IsVisible = false;
                        StopButton.IsVisible = false;
                        PlayButton.IsVisible = false;
                        StatusLabel.Text = "Buffering";
                        await StatusLabel.FadeTo(1);
                        break;
                    case MediaPlayerStatus.Failed:
                        /*PauseButton.IsEnabled = false;
                        StopButton.IsEnabled = false;
                        PlayButton.IsEnabled = false;*/
                        PauseButton.IsVisible = false;
                        StopButton.IsVisible = false;
                        PlayButton.IsVisible = false;
                        StatusLabel.Text = "";
                        await StatusLabel.FadeTo(1);
                        break;
                    default:
                        break;
                }
            });
        }

        private async void OnPlayClicked(object sender, EventArgs e)
        {
            await PlaybackController.Play();
            await ShowHideControls(true);
            _isControlsVisible = false;
        }

        private async void OnPauseClicked(object sender, EventArgs e)
        {
            await PlaybackController.Pause();
        }

        private async void OnStopClicked(object sender, EventArgs e)
        {
            await PlaybackController.Stop();
        }

        private async void OnVideoTapped(object sender, EventArgs e)
        {
            await ShowHideControls(_isControlsVisible);
            _isControlsVisible = !_isControlsVisible;
        }

        private async Task ShowHideControls(bool isHide)
        {
            if (isHide)
            {
                // Hide controls...
                await VideoControlsGrid.FadeTo(0.0, 100, Easing.CubicOut);
                await VideoControlsLayout.FadeTo(0.0, 100, Easing.CubicOut);
                await StatusLabel.FadeTo(0.0, 100, Easing.CubicOut);
            }
            else
            {
                // Show controls...
                await VideoControlsGrid.FadeTo(0.5, 150, Easing.CubicIn);
                await VideoControlsLayout.FadeTo(1.0, 150, Easing.CubicIn);
                await StatusLabel.FadeTo(1.0, 150, Easing.CubicIn);
            }
        }
    }
}