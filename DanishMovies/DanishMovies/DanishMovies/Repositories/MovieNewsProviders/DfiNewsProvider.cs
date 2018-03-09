using DanishMovies.Models;
using DanishMovies.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace DanishMovies.Repositories.MovieNewsProviders
{
    public class DfiNewsProvider : BaseNewsProvider
    {
        public override async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false)
        {
            const string dkNewsUrl = "http://www.dfi.dk/?rss=1&filterid={2D85B64B-BDD6-4356-8030-FDBAC909F522}"; // The danish rss feed.
            const string ukNewsUrl = "http://www.dfi.dk/?rss=1&filterid={B261A243-6824-4DDB-B6F9-29E2C7EFBF48}"; // The english rss feed.

            HttpClient client = new HttpClient();
            var response = isEnglishLanguageSet
                ? await client.GetStreamAsync(ukNewsUrl)
                : await client.GetStreamAsync(dkNewsUrl);

            var result = await Task.Run(() => GetMovieNews(response));
            return result;
        }

        public override IEnumerable<MovieNews> GetMovieNews(Stream stream)
        {
            var result = new List<MovieNews>();

            XDocument rss;
            try
            {
                rss = XDocument.Load(stream); // Danish news
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to load news rss source!\nException:\n{ex.Message}");
                return result;
            }

            try
            {
                var query = rss.Descendants("item").Select(item =>
                    {
                        //
                        // Get raw element data...
                        //
                        var enclosureElement = item.Element("enclosure");
                        var titleElement = item.Element("title");
                        var descriptionElement = item.Element("description");
                        var publicationDateElement = item.Element("pubDate");
                        var linkElement = item.Element("link");
                        var authorElement = item.Element("author");

                        //
                        // Validate element data...
                        //
                        if (titleElement == null) return null;
                        if (descriptionElement == null) descriptionElement = new XElement("", "");
                        if (publicationDateElement == null) publicationDateElement = new XElement("", "");
                        if (linkElement == null) linkElement = new XElement("", "");
                        if (authorElement == null) authorElement = new XElement("", "");
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
                        // Return a news object for Dfi... 
                        //
                        var content = WebStringHelper
                            .RemovePrefixFromText(
                                "<!--50-->", 
                                descriptionElement.Value);
                        content = WebStringHelper
                            .RemoveSuffixFromText(
                                "<br>",
                                content);

                        return enclosureElement != null
                                   ? new MovieNews
                                       {
                                           Headline = titleElement.Value,
                                           Content = content,
                                           ImageUrl = urlAttribute != null ? urlAttribute.Value : "",
                                           PublicationDate = publicationDate,
                                           StoryUrl = linkElement.Value,
                                           Author = authorElement.Value,
                                           Source = MovieNewsProviderType.Dfi.ToString().ToUpper()
                                       }
                                   : new MovieNews { Headline = "n/a" };
                    });

                return query;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to parse news rss source!\nException:\n{ex.Message}");
                return result;
            }
        }
    }
}
