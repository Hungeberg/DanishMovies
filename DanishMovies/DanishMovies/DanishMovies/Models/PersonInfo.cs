using System;
using System.Collections.Generic;

namespace DanishMovies.Models
{
    /// <summary>
    /// Person
    /// </summary>
    public class PersonInfo : BaseInfo
    {
        public int YearOfBirth { get; set; }
        public int YearOfDeath { get; set; }
        public int Age
        {
            get
            {
                var age = YearOfDeath > 0
                    ? YearOfDeath - YearOfBirth
                    : YearOfBirth > 0
                        ? DateTime.Now.Year - YearOfBirth
                        : 0;
                return age;
            }
        }
        public IList<BaseInfo> FilmCredits { get; set; }
        public IList<BaseInfo> OtherNameIds { get; set; }
    }
}
