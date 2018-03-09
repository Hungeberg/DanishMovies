using System.Collections.Generic;
using System.Linq;

namespace DanishMovies.Models
{
    /// <summary>
    /// Film
    /// </summary>
    public class MovieInfo : BaseInfo
    {
        private string _danishTitle;
        public string DanishTitle
        {
            get { return _danishTitle;  }
            set
            {
                _danishTitle = value;
                if (!string.IsNullOrEmpty(_danishTitle))
                {
                    Title = _danishTitle;
                    Name = _danishTitle;
                }
            }
        }
        private string _originalTitle;
        public string OriginalTitle
        {
            get { return _originalTitle; }
            set
            {
                _originalTitle = value;
                if (!string.IsNullOrEmpty(_originalTitle) && 
                    string.IsNullOrEmpty(DanishTitle))
                {
                    Title = _originalTitle;
                    Name = _originalTitle;
                }
            }
        }
        public IList<string> AltTitle { get; set; }
        public IList<ForeignTitle> ForeignTitles { get; set; }
        public int LengthInMin { get; set; }
        public int LengthInMeters { get; set; }
        public string Literature { get; set; }
        public string Category { get; set; }
        public IList<MoviePremiereInfo> Premiere { get; set; }
        public MoviePremiereInfo PremiereInfo { get; set; }
        public int ProductionYear { get; set; }
        public IList<string> ProductionCountries { get; set; }
        public IList<BaseInfo> ProductionCompanies { get; set; }
        public IList<BaseInfo> DistributionCompanies { get; set; }
        public IList<BaseInfo> Requestors { get; set; }
        public string Width { get; set; }
        public string FilmFormat { get; set; }
        public string ColorSystem { get; set; }
        public string ToneSystem { get; set; }
        public string Classification { get; set; }
        public string AssesmentText { get; set; }
        public IList<string> SubCategories { get; set; }
        public IList<BaseInfo> PersonCredits { get; set; }
        public string Idea { get; set; }
        public IList<string> Genre { get; set; }
        public string FilmstribeId { get; set; }
        public bool InVideoteket { get; set; }
        public IList<string> Keywords { get; set; }
        public IList<string> DialogLanguage { get; set; }
        public string MovieTypes
        {
            get
            {
                var movieTypes = SubCategories?
                    .Aggregate("", (current, category) => current + (category + "\n"));
                if (!string.IsNullOrEmpty(movieTypes))
                {
                    movieTypes = movieTypes.TrimEnd('\n');
                }
                return movieTypes;
            }
        }
        public string Countries
        {
            get
            {
                var countries = ProductionCountries?
                    .Aggregate("", (current, country) => current + (country + ", "));
                if (!string.IsNullOrEmpty(countries))
                {
                    countries = countries.Trim().TrimEnd(',');
                }
                return countries;
            }
        }
        public string MovieDescription
        {
            get
            {
                var description = Description;
                if (string.IsNullOrEmpty(description))
                {
                    if (!string.IsNullOrEmpty(Comment))
                    {
                        description = Comment;
                    }
                    else if (!string.IsNullOrEmpty(Idea))
                    {
                        description = Idea;
                    }
                }
                return description;
            }
        }
    }
}
