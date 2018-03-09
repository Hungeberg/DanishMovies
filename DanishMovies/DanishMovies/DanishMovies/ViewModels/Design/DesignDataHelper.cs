using DanishMovies.Models;
using System;
using System.Collections.Generic;

namespace DanishMovies.ViewModels.Design
{
    public static class DesignDataHelper
    {
        public static PersonInfo GetPersonInfo()
        {
            return new PersonInfo
            {
                Id = 1099,
                Name = "Jannik Hastrup",
                Description = 
                    "Tegnefilminstruktør, født 4/5 1941 i Næstved. " +
                    "Blev i 1960 ansat hos Bent Barfoed og udlært som animator. " +
                    "Her mødte han Flemming Quist Møller, som han lavede en række " +
                    "korte tegnefilm med og siden skulle komme til at arbejde " +
                    "sammen med på flere film. " +
                    "Op gennem 1960'erne arbejdede Hastrup også på reklamefilm, " +
                    "og sammen med sin daværende kone Hanne Hastrup skabte han " +
                    "Cirkeline-filmene, 19 i alt, til Danmarks Radio, " +
                    "som 30 år senere har udmyntet sig i tre nye Cirkeline-film " +
                    "til biografvisning." +
                    "Efter \"Bennys badekar\" (1971, sammen med Quist Møller), " +
                    "som i dag har klassikerstatus, lavede han den første tegnefilm " +
                    "i spillefilmslængde siden 1946, \"Samson og Sally\" (1984). " +
                    "Siden er det blevet til en række spillefilm og adskillige " +
                    "kortere animationsfilm." +
                    "Som adskillige af de store animatorer i dansk film har musikken " +
                    "spillet en stor rolle for Jannik Hastrup, der selv spiller trompet. " +
                    "Hans film er præget af stor musikalitet og en sikker sans for rytme.",
                //Url = "http://www.dfi.dk/faktaomfilm/nationalfilmografien/nfperson.aspx?id=1099",
                YearOfBirth = 1941,
                YearOfDeath = 0,
                ImageUrl = GetImageUrl(SearchTypes.Person),
                FilmCredits = new List<BaseInfo>() { new BaseInfo() }
            };
        }

        public static MovieInfo GetMovieInfo()
        {
            return new MovieInfo
            {
                Id = 24,
                Title = "Bennys Badekar",
                Description = 
                    "Tegnefilm for både børn og voksne om drengen Benny, " +
                    "der keder sig i højhusbebyggelsen, hvor forældrene ikke " +
                    "har tid til at snakke med ham. Han går ud og fanger en haletudse, " +
                    "som han tager med hjem i badeværelset. " +
                    "Haletudsen er en fortryllet prins, og sammen med ham dykker " +
                    "Benny ned på bunden af badekarret, der bliver til det store ocean. " +
                    "Her oplever han en masse af de spændende og eventyrlige ting, " +
                    "han savner i sin hverdag.",
                OriginalTitle = "Bennys Badekar",
                //Url = "http://www.dfi.dk/faktaomfilm/nationalfilmografien/nffilm.aspx?id=24",
                ImageUrl = GetImageUrl(SearchTypes.Movie),
                ProductionCountries = new List<string>() { "DK" },
                LengthInMin = 41,
                ProductionYear = 1971,
                Category = "DK/Kort fiktion",
                Comment = "\"Bennys badekar\" var den første lange tegnefilm i en årrække og blev positivt modtaget. Man roste filmens underfundige poesi, fortælleglæde og mangel på pædagogiske pegefingre.",
                FilmFormat = "Widescreen",
                SubCategories = new List<string>() { "Børnefilm", "Animation" },
                PersonCredits = new List<BaseInfo>() { new BaseInfo() },
                ProductionCompanies = new List<BaseInfo>() { new BaseInfo() },
                DistributionCompanies = new List<BaseInfo>() { new BaseInfo() },
                Classification = "Tilladt for alle"
            };
        }

        public static MoviePremiereInfo GetMoviePremiereInfo()
        {
            return new MoviePremiereInfo
            {
                PremiereComment = "samt 50 biografer i provinsen.",
                PremiereDate = new DateTime(2000, 12, 8),
                PremiereTheatres = new List<string>()
                {
                    "Dagmar",
                    "Palads",
                    "Palladium",
                    "Lyngby",
                    "Scala",
                    "Gladsaxe Bio",
                    "BioCity (Tåstrup)",
                    "Kastrup Bio",
                    "Albertslund Bio",
                    "CinemaxX"
                }
            };
        }

        public static CompanyInfo GetCompanyInfo()
        {
            return new CompanyInfo
            {
                Id = 1114,
                Name = "Fiasco Film",
                //Url = "http://www.dfi.dk/faktaomfilm/nationalfilmografien/nfselskab.aspx?id=1114",
                Productions = new List<BaseInfo>() { new BaseInfo() },
                Distributions = new List<BaseInfo>() { new BaseInfo() },
                Requestor = new List<BaseInfo>() { new BaseInfo() }
            };
        }

        public static string GetDescription()
        {
            return "Tegnefilm for både børn og voksne om drengen Benny, " +
                   "der keder sig i højhusbebyggelsen, hvor forældrene ikke " +
                   "har tid til at snakke med ham. Han går ud og fanger en haletudse, " +
                   "som han tager med hjem i badeværelset. " +
                   "Haletudsen er en fortryllet prins, og sammen med ham dykker " +
                   "Benny ned på bunden af badekarret, der bliver til det store ocean. " +
                   "Her oplever han en masse af de spændende og eventyrlige ting, " +
                   "han savner i sin hverdag.";
        }

        public static string GetName()
        {
            return "Bennys Badekar";
        }

        public static string GetImageUrl(SearchTypes imageType)
        {
            var result = "";

            switch (imageType)
            {
                case SearchTypes.na:
                case SearchTypes.Movie:
                    result = "movie.png";
                    break;
                case SearchTypes.Person:
                    result = "person.png";
                    break;
                default:
                    break;
            }
            return result;
        }

        public static MovieNews GetMovieNewsItem()
        {
            var isLongContentTest = true; // Set to false for short content test!
            const string contentShort = 
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.";
            const string contentLong = 
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.\n\n"+
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.\n\n"+
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.\n\n" +
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.\n\n" +
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.\n\n" +
                "Hvordan kan biblioteker, museer og andre kulturinstitutioner " +
                "bruge digitale medier i formidlingen til børn og unge? Slots- " +
                "og Kulturstyrelsen og Det Danske Filminstitut inviterer til " +
                "konference den 6. december, hvor kulturminister Mette Bock " +
                "holder åbningstale.";

            return new MovieNews
            {
                Headline = "Få inspiration til at formidle til børn og unge",
                Content = isLongContentTest ? contentLong : contentShort,
                Author = "dfi@dfi.dk (Det Danske Filminstitut)",
                ImageUrl = "movie.png",
                PublicationDate = DateTime.Now,
                StoryUrl = "http://www.dfi.dk"
            };
        }

    }
}
