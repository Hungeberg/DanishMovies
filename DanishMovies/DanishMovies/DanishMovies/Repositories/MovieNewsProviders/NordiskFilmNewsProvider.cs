using DanishMovies.Models;
using DanishMovies.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DanishMovies.Repositories.MovieNewsProviders
{
    public class NordiskFilmNewsProvider : BaseNewsProvider
    {
        public override async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false)
        {
            const string dkNewsUrl = "http://www.nordiskfilm.com/Feed/Index.aspx?pid=2160&epslanguage=da"; // The danish rss feed.
            const string ukNewsUrl = "http://www.nordiskfilm.com/Feed/Index.aspx?pid=2160&epslanguage=uk"; // The english rss feed.

            HttpClient client = new HttpClient();
            var response = isEnglishLanguageSet
                ? await client.GetStreamAsync(ukNewsUrl)
                : await client.GetStreamAsync(dkNewsUrl);

            var result = await Task.Run(() => GetMovieNews(response));
            return result;
        }

        public override IEnumerable<MovieNews> GetMovieNews(Stream stream)
        {
            const string baseUrl = "http://www.nordiskfilm.com";

            XDocument rss;
            try
            {
                rss = XDocument.Load(stream); // Danish news
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                var query = rss.Descendants("item").Select(item =>
                    {
                        //
                        // Get raw element data...
                        //
                        var titleElement = item.Element("title");
                        var descriptionElement = item.Element("description");
                        var linkElement = item.Element("link");

                        //
                        // Validate element data...
                        //
                        if (titleElement == null) return null;
                        if (descriptionElement == null) descriptionElement = new XElement("", "");
                        if (linkElement == null) linkElement = new XElement("", "");

                        var content = WebStringHelper
                            .StripHtml(descriptionElement.Value);
                        var imageUrl = WebStringHelper
                            .RemovePrefixFromText(
                                "<img src=\"", 
                                descriptionElement.Value);
                        if (imageUrl == descriptionElement.Value) imageUrl = string.Empty;
                        imageUrl = WebStringHelper
                            .RemoveSuffixFromText(
                                "\" alt=", 
                                imageUrl);
                        imageUrl = string.IsNullOrEmpty(imageUrl) ? string.Empty : baseUrl + imageUrl;

                        //
                        // Return a news object for Nordisk Film... 
                        //
                        return new MovieNews
                            {
                                Headline = titleElement.Value,
                                Content = content,
                                ImageUrl = string.IsNullOrEmpty(imageUrl) ? "nordisk.png" : imageUrl,
                                PublicationDate = DateTime.UtcNow - new TimeSpan(300, 0, 0, 0),
                                StoryUrl = linkElement.Value,
                                Author = "Nordisk Film",
                                Source = MovieNewsProviderType.Nordisk.ToString().ToUpper()
                            };
                    }).Take(20);

                return query;
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
