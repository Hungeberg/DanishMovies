using System;

namespace DanishMovies.Models
{
    public class MovieTrailer
    {
        /*
            <title>ANTBOY</title>
            <text>I BIOGRAFEN 3. OKTOBER</text>
            <link>http://www.dfi.dk/faktaomfilm/film/da/80406.aspx?id=80406</link>
            <media type="movie">http://www.dfi.dk/~/media/Video/ANTBOY.ashx</media>
            <firstframe>http://www.dfi.dk/~/media/Video/antboystill.ashx?mw=386&amp;mh=217&amp;bc=white</firstframe>
            <googleanalytics>ANTBOY</googleanalytics>
        */
        public int Id
        {
            get
            {
                var result = 0;
                if (MovieUrl != null)
                {
                    int.TryParse(MovieUrl.Substring(MovieUrl.IndexOf("id=", StringComparison.Ordinal)+3), out result);
                }
                return result;
            }
        }
        
        public string Title { get; set; } // title
        public string Comment { get; set; } // text
        public string MovieUrl { get; set; } // link
        public string ImageUrl { get; set; } // firstframe
        public string VideoUrl { get; set; } // media
    }
}
