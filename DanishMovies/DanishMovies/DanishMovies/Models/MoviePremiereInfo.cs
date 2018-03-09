using System;
using System.Collections.Generic;

namespace DanishMovies.Models
{
    /// <summary>
    /// PremiereItem
    /// </summary>
    public class MoviePremiereInfo
    {
        public string PremiereType { get; set; }
        public DateTime PremiereDate { get; set; }
        public string PremiereComment { get; set; }
        public IList<string> PremiereTheatres { get; set; }
    }
}
