using DanishMovies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DanishMovies.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<string> GetImageUrlAsync(string sourceUrl);
        Task<ImageItems> GetImagesAsync(string sourceUrl, bool isPerson = false);
        IList<ImageItem> FilterImages(IList<ImageItem> images, bool isPerson = false, string caption = "");
    }
}
