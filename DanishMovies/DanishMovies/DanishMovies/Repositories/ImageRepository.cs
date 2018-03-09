using System.Threading.Tasks;
using DanishMovies.Interfaces.Repositories;
using System;
using System.Diagnostics;
using DanishMovies.Models;
using System.Linq;
using System.Collections.Generic;

namespace DanishMovies.Repositories
{
    public class ImageRepository : BaseRepository, IImageRepository
    {
        private const string CAPTION_FILTER = "dummy video";
        private const string CATEGORY_FILTER_1 = "dk/spillefilm";
        private const string CATEGORY_FILTER_2 = "plakater";

        private string GetImageUrlFromHtml(string html)
        {
            const string START = "<meta name=\"image\" content=\"";
            const string END = "\"";
            const string SIZE_TAG_SOURCE = "/micr/";
            const string SIZE_TAG_TARGET = "/mini/";

            var result = string.Empty;
            if (string.IsNullOrEmpty(html)) return result;
            if (!html.Contains(START)) return result;

            try
            {
                var indexStart = html.IndexOf(START) + START.Length;
                var indexEnd = html.IndexOf(END, indexStart);
                var imageUrl = html.Substring(indexStart, indexEnd - indexStart);

                if (imageUrl.Contains(SIZE_TAG_SOURCE))
                {
                    imageUrl = imageUrl.Replace(SIZE_TAG_SOURCE, SIZE_TAG_TARGET);
                }

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    result = imageUrl;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HTML parsing failed with exception:\n{ex.Message}");
            }
            return result;
        }

        public async Task<string> GetImageUrlAsync(string sourceUrl)
        {
            var result = string.Empty;
            if (string.IsNullOrEmpty(sourceUrl)) return result;

            var images = await GetAsync<ImageItems>(sourceUrl, true);

            var image = images
                .Images?
                .Where(x => x.Caption != "Dummy video")
                .OrderBy(o => Convert.ToInt32(o.Urgency))
                .FirstOrDefault();

            if (image != null)
            {
                result = image.PathMini;
            }
            return result;
        }

        public async Task<ImageItems> GetImagesAsync(string sourceUrl, bool isPerson = false)
        {
            var result = new ImageItems();
            if (string.IsNullOrEmpty(sourceUrl)) return null;

            var images = await GetAsync<ImageItems>(sourceUrl, true);

            if (images != null)
            {
                if (isPerson)
                {
                    result.Images = images
                        .Images?
                        .Where(x => x.Caption != "Dummy video" &&
                                    x.Categori != "dk/spillefilm" &&
                                    x.Categori != "plakater")
                        .OrderBy(o => Convert.ToInt32(o.Urgency))
                        .ToList();
                }
                else
                {
                    result.Images = images
                        .Images?
                        .Where(x => x.Caption != "Dummy video")
                        .OrderBy(o => Convert.ToInt32(o.Urgency))
                        .ToList();
                }
            }
            return result;
        }

        public IList<ImageItem> FilterImages(IList<ImageItem> images, bool isPerson = false, string caption = "")
        {
            var result = images;

            if (result != null)
            {
                if (isPerson)
                {
                    result = result?
                        .Where(x => x.Caption?.ToLower() != CAPTION_FILTER &&
                                    x.Categori?.ToLower() != CATEGORY_FILTER_1 &&
                                    x.Categori?.ToLower() != CATEGORY_FILTER_2 &&
                                    x.Category?.ToLower() != CATEGORY_FILTER_1 &&
                                    x.Category?.ToLower() != CATEGORY_FILTER_2)
                        .OrderBy(o => Convert.ToInt32(o.Urgency))
                        .ToList();
                }
                else
                {
                    result = result?
                        .Where(x => x.Caption?.ToLower() != CAPTION_FILTER)
                        .OrderBy(o => Convert.ToInt32(o.Urgency))
                        .ToList();
                }
            }
            return result;
        }
    }
}
