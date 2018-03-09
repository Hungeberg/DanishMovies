using System.Collections.Generic;

namespace DanishMovies.Models
{
    /// <summary>
    /// Company
    /// </summary>
    public class CompanyInfo : BaseInfo
    {
        public IList<BaseInfo> Productions { get; set; }
        public IList<BaseInfo> Distributions { get; set; }
        public IList<BaseInfo> Requestor { get; set; }
    }
}
