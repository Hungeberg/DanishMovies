using DanishMovies.Interfaces.Repositories;
using DanishMovies.Models;
using DanishMovies.Repositories.MovieNewsProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanishMovies.Repositories
{
    public class MovieNewsRepository : IMovieNewsRepository
    {
        public async Task<IEnumerable<MovieNews>> GetMovieNewsAsync(bool isEnglishLanguageSet = false, MovieNewsProviderType providerType = MovieNewsProviderType.Dfi)
        {
            var dfiNewsProvider = new DfiNewsProvider();
            var nordiskFilmNewsProvider = new NordiskFilmNewsProvider();
            var filmNytNewsProvider = new FilmNytNewsProvider();
            var filmzNewsProvider = new FilmzNewsProvider();

            switch (providerType)
            {
                case MovieNewsProviderType.Dfi:
                    return await dfiNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                case MovieNewsProviderType.Nordisk:
                    return await nordiskFilmNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                case MovieNewsProviderType.FilmNyt:
                    return await filmNytNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                case MovieNewsProviderType.Filmz:
                    return await filmzNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                case MovieNewsProviderType.All:
                    var dfiNews = await dfiNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                    var nordiskFilmNews = await nordiskFilmNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                    var filmNytNews = await filmNytNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);
                    var filmzNews = await filmzNewsProvider.GetMovieNewsAsync(isEnglishLanguageSet);

                    var news = new List<MovieNews>();

                    if (dfiNews != null)
                    {
                        var dfiNewsList = dfiNews as IList<MovieNews> ?? dfiNews.ToList();
                        if (dfiNewsList.Any())
                        {
                            news.AddRange(dfiNewsList);
                        }
                    }

                    if (filmNytNews != null)
                    {
                        var filmNytNewsList = filmNytNews as IList<MovieNews> ?? filmNytNews.ToList();
                        if (filmNytNewsList.Any())
                        {
                            news.AddRange(filmNytNewsList);
                        }
                    }

                    if (filmzNews != null)
                    {
                        var filmzNewsList = filmzNews as IList<MovieNews> ?? filmzNews.ToList();
                        if (filmzNewsList.Any())
                        {
                            news.AddRange(filmzNewsList);
                        }
                    }

                    if (nordiskFilmNews != null)
                    {
                        var nordiskFilmNewsList = nordiskFilmNews as IList<MovieNews> ?? nordiskFilmNews.ToList();
                        if (nordiskFilmNewsList.Any())
                        {
                            news.AddRange(nordiskFilmNewsList);
                        }
                    }
                    news = news.OrderByDescending(x => x.PublicationDate).ToList();

                    return news;
            }
            return null;
        }
    }
}
