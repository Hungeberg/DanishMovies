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
    public class FilmzNewsProvider : BaseNewsProvider
    {
        public override async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false)
        {
            const string dkNewsUrl = "http://filmz.dk/api-rss/index"; // The rss feed.
            //const string ukNewsUrl = "";

            HttpClient client = new HttpClient();
            var response = isEnglishLanguageSet
                ? await client.GetStreamAsync(dkNewsUrl)
                : await client.GetStreamAsync(dkNewsUrl);

            var result = await Task.Run(() => GetMovieNews(response));
            return result;
        }

        public override IEnumerable<MovieNews> GetMovieNews(Stream stream)
        {
            XDocument rss;
            try
            {
                rss = XDocument.Load(stream); // Danish news
            }
            catch (Exception)
            {
                return null;
            }
            XNamespace media = "http://search.yahoo.com/mrss/";

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
                        var publicationDateElement = item.Element("pubDate");
                        var enclosureElement = item.Element(media + "thumbnail");

                        //
                        // Validate element data...
                        //
                        if (titleElement == null) return null;
                        if (descriptionElement == null) descriptionElement = new XElement("", "");
                        if (linkElement == null) linkElement = new XElement("", "");
                        if (publicationDateElement == null) publicationDateElement = new XElement("", "");
                        var urlAttribute = enclosureElement != null
                                               ? enclosureElement.Attribute("url")
                                               : new XAttribute("", "");

                        //
                        // Prepare publication date using default if none exists...
                        //
                        DateTime publicationDate;
                        if (!DateTime.TryParse(publicationDateElement.Value, out publicationDate))
                        {
                            publicationDate = DateTime.UtcNow - new TimeSpan(300, 0, 0, 0);
                        }

                        //
                        // Return a news object for Filmz... 
                        //
                        return new MovieNews
                            {
                                Headline = titleElement.Value,
                                Content = WebStringHelper.StripHtml(descriptionElement.Value),
                                ImageUrl = urlAttribute.Value,
                                PublicationDate = publicationDate,
                                StoryUrl = linkElement.Value,
                                Author = "Filmz.dk",
                                Source = MovieNewsProviderType.Filmz.ToString().ToUpper()
                            };
                    }).Take(20);

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
