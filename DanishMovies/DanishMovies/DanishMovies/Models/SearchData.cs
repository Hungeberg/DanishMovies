using System.Collections.Generic;

namespace DanishMovies.Models
{
    /// <summary>
    /// DFIListData
    /// </summary>
    public class SearchData
    {
        public int ResultCount { get; set; }
        public int TotalResultCount { get; set; }
        public IList<BaseInfo> FilmList { get; set; }
        public IList<BaseInfo> PersonList { get; set; }
        public IList<BaseInfo> CompanyList { get; set; }
    }
}
