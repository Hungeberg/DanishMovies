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
    public class FilmNytNewsProvider : BaseNewsProvider
    {
        public override async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false)
        {
            const string dkNewsUrl = "http://www.film-nyt.dk/feed"; // The rss feed.
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
            XNamespace dcNs = "http://purl.org/dc/elements/1.1/";
            XNamespace contentNs = "http://purl.org/rss/1.0/modules/content/";

            try
            {
                var query = rss.Descendants("item").Select(item =>
                    {
                        //
                        // Get raw element data...
                        //
                        var titleElement = item.Element("title");
                        var descriptionElement = item.Element(contentNs + "encoded");
                        var linkElement = item.Element("link");
                        var publicationDateElement = item.Element("pubDate");
                        var authorElement = item.Element(dcNs + "creator");

                        //
                        // Validate element data...
                        //
                        if (titleElement == null) return null;
                        if (descriptionElement == null) descriptionElement = new XElement("", "");
                        if (linkElement == null) linkElement = new XElement("", "");
                        if (publicationDateElement == null) publicationDateElement = new XElement("", "");
                        if (authorElement == null) authorElement = new XElement("", "");

                        //
                        // Prepare publication date using default if none exists...
                        //
                        DateTime publicationDate;
                        if (!DateTime.TryParse(publicationDateElement.Value, out publicationDate))
                        {
                            publicationDate = DateTime.UtcNow - new TimeSpan(300, 0, 0, 0); // Default is 300 days ago.
                        }

                        //
                        // Prepare content with some special parsing to strip title from content...
                        //
                        var content = WebStringHelper.StripHtml(descriptionElement.Value);
                        content = WebStringHelper.RemoveTag(content, titleElement.Value, "Film-nyt.dk.", true);

                        //
                        // Return a news object for FilmNyt... 
                        //
                        return new MovieNews
                            {
                                Headline = titleElement.Value,
                                Content = content,
                                ImageUrl = "filmnyt.png",
                                PublicationDate = publicationDate,
                                StoryUrl = linkElement.Value,
                                Author = authorElement.Value,
                                Source = MovieNewsProviderType.FilmNyt.ToString().ToUpper()
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
