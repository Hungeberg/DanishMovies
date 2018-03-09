using PCLStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DanishMovies.Utility
{
    /// <summary>
    /// Helper class for downloading streamed videos.
    /// </summary>
    public static class VideoDownloadHelper
    {
        private const string TEMP_VIDEO_FOLDER_NAME = "VideoFiles";
        private const string TEMP_VIDEO_FILE_NAME = "temp.mp4";
        private const string TEMP_VIDEO_FILE_NAME_2 = "temp2.mp4";

        /// <summary>
        /// Method that will download a file with a given url and return the file name. 
        /// </summary>
        /// <param name="url">Url for file to download</param>
        /// <returns>Filename of downloaded file</returns>
        public static async Task<string> DownloadVideoFileAsync(string url)
        {
            if (string.IsNullOrEmpty(url)) return url;
            var result = string.Empty;

            try
            {
                var httpClient = new HttpClient();
                var videoBytes = await httpClient.GetByteArrayAsync(url);

                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.CreateFolderAsync(TEMP_VIDEO_FOLDER_NAME,
                    CreationCollisionOption.OpenIfExists);

                IFile file;

                #region WINDOWS WORKAROUND
                //
                // This is a simple workaround for windows video playback to make it work.
                // The workaround is to switch between two temp files so that the previous
                // temp file can be deleted and hence used again.
                //
                if (Device.RuntimePlatform == Device.UWP)
                {
                    file = await FileSystem.Current.GetFileFromPathAsync(PortablePath.Combine(folder.Path, TEMP_VIDEO_FILE_NAME));
                    if (file != null) // Use file 2 ?
                    {
                        await file.DeleteAsync(); // Delete file 1.
                        file = await folder.CreateFileAsync(TEMP_VIDEO_FILE_NAME_2,
                            CreationCollisionOption.ReplaceExisting);
                    }
                    else // Use file 1 ?
                    {
                        var file2 = await FileSystem.Current.GetFileFromPathAsync(PortablePath.Combine(folder.Path, TEMP_VIDEO_FILE_NAME_2));
                        if (file2 != null)
                        {
                            await file2.DeleteAsync(); // Delete file 2.
                        }
                        file = await folder.CreateFileAsync(TEMP_VIDEO_FILE_NAME,
                            CreationCollisionOption.ReplaceExisting);
                    }
                }
                #endregion
                else
                {
                    file = await folder.CreateFileAsync(TEMP_VIDEO_FILE_NAME,
                        CreationCollisionOption.ReplaceExisting);
                }

#if DEBUG
                var files = await folder.GetFilesAsync();
                foreach (var f in files)
                {
                    Debug.WriteLine(f.Path);
                }
#endif

                using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
                {
                    stream.Write(videoBytes, 0, videoBytes.Length);
                }
                result = "file://" + file.Path;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to download video from source!\nURL:\n{url}\nException:\n{ex.Message}");
            }
            return result; // Return the filename of our downloaded file.
        }
    }
}
