using System;

namespace DanishMovies.Models
{
    public class MovieNews
    {
        /*
            <title>Niels Arden Oplev er tilbage med Kapgang</title>
            <link>http://www.dfi.dk/Nyheder/FILMupdate/2013/juli/Niels-Arden-Oplev-er-tilbage-med-Kapgang.aspx</link>
            <guid>http://www.dfi.dk/Nyheder/FILMupdate/2013/juli/Niels-Arden-Oplev-er-tilbage-med-Kapgang.aspx</guid>
            <pubDate>Mon, 22 Jul 2013 21:03:00 GMT</pubDate>
            <description>&lt;!--50--&gt;PRODUKTION. Efter sit gennembrud i USA vender Niels Arden Oplev hjem til Danmark for at filme med bl.a. Sidse Babett Knudsen og Anders W. Berthelsen. "Kapgang" får premiere i foråret 2014.</description>
            <author>dfi@dfi.dk (Det Danske Filminstitut)</author>
            <enclosure url="http://www.dfi.dk/gimage.ashx?i=VHJ1ZV9ffHxfX2h0dHA6Ly93d3cuZGZpLmRrOjgwL34vbWVkaWEvQjczMjgwM0VBMzg3NEM1NkJGNzVFRjlENjE1M0NDRkIuYXNoeF9ffHxfXzg4X198fF9fODhfX3x8X19UcnVlX198fF9fRmFsc2VfX3x8X19GYWxzZV9ffHxfXzBfX3x8X19fX3x8X18w" length="2085383" type="image/jpeg" />
        */
        public string Headline { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublicationDate { get; set; }
        public string StoryUrl { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
    }
}
