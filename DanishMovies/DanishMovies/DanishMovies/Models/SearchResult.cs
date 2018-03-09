using I18NPortable;

namespace DanishMovies.Models
{
    public class SearchResult : BaseInfo
    {
        public SearchTypes SearchType { get; set; }
        public string SearchTypeDisplay
        {
            get
            {
                return SearchType.ToString().Translate();
            }
        }
    }
}
