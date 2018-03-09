using DanishMovies.Interfaces.Repositories;
using DanishMovies.Models;
using DanishMovies.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DanishMovies.Repositories
{
    public class MovieTrailerRepository : IMovieTrailerRepository
    {
        private const string TRAILERS_URL = @"http://www.dfi.dk/netmester/mediacarousel/service/slides.asmx/GetSlidesAsXmlString?guid=90C67E03-8FC4-484F-B630-1109D78C86C3";

        public async Task<IEnumerable<MovieTrailer>> GetMovieTrailersAsync()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStreamAsync(TRAILERS_URL);
            var result = await Task.Run(() => GetMovieTrailers(response));
            return result;
        }

        private IEnumerable<MovieTrailer> GetMovieTrailers(Stream stream)
        {
            var rss = XDocument.Load(stream);

            try
            {
                if (rss.Root == null) return null;

                var doc = XDocument.Parse(rss.Root.Value);

                var query = doc.Descendants("slide").Select(slide =>
                {
                    var titleElement = slide.Element("title");
                    var textElement = slide.Element("text");
                    var linkElement = slide.Element("link");
                    var mediaElement = slide.Element("media");
                    var firstFrameElement = slide.Element("firstframe");

                    if (firstFrameElement == null) return null;
                    if (titleElement == null) titleElement = new XElement("", "");
                    if (textElement == null) textElement = new XElement("", "");
                    if (linkElement == null) linkElement = new XElement("", "");
                    if (mediaElement == null) mediaElement = new XElement("", "");

                    return new MovieTrailer
                    {
                        Title = titleElement.Value,
                        Comment = textElement.Value,
                        ImageUrl = WebStringHelper.UrlEncode(firstFrameElement.Value),
                        MovieUrl = WebStringHelper.UrlEncode(linkElement.Value),
                        VideoUrl = WebStringHelper.UrlEncode(mediaElement.Value)
                    };
                });

                return query;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetMovieTrailers() failed with exception:\n{ex.Message}");
            }
            return null;
        }
    }
}
