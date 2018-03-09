using I18NPortable;
using System;
using System.Collections.Generic;

namespace DanishMovies.Models
{
    public class BaseInfo
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(Title))
                {
                    _name = Title;
                }
                return _name;
            }
            set { _name = value; }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string ImagesUrl { get; set; }
        public bool HasAdditionalData { get; set; }
        public DateTime Updated { get; set; }
        public int ReleaseYear { get; set; }
        public string Type { get; set; }
        public string TypeCode { get; set; }
        public string TypeId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public IList<ImageItem> Images { get; set; }
        public string TypeDescription
        {
            get
            {
                var asString = "as".Translate(); // This can be translated!
                var releaseYear = ReleaseYear > 0 ? $"{ReleaseYear}" : "";
                var description = string.IsNullOrEmpty(Description)
                    ? ""
                    : $" {asString} '{Description}'";
                var seperator = 
                    ((!string.IsNullOrEmpty(Type) || 
                      !string.IsNullOrEmpty(description)) && 
                     !string.IsNullOrEmpty(releaseYear))
                        ? " - "
                        : "";

                return $"{releaseYear}{seperator}{Type}{description}";
            }
        }
    }
}
