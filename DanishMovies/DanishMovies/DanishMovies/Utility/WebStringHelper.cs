using System;
using System.Text;

namespace DanishMovies.Utility
{
    public static class WebStringHelper
    {
        /// <summary>
        /// Converts any found special characters to there Url Encoding codes
        /// </summary>
        /// <param name="text">String to look in.</param>
        /// <returns>converted string</returns>
        public static string UrlEncodeSpecialCharacters(string text)
        {
            if (String.IsNullOrEmpty(text)) return "";

            var parsedText = text;

            if (parsedText.Contains("æ"))
            {
                parsedText = parsedText.Replace("æ", "%e6");
            }

            if (parsedText.Contains("Æ"))
            {
                parsedText = parsedText.Replace("Æ", "%c6");
            }

            if (parsedText.Contains("ø"))
            {
                parsedText = parsedText.Replace("ø", "%f8");
            }

            if (parsedText.Contains("Ø"))
            {
                parsedText = parsedText.Replace("Ø", "%d8");
            }

            if (parsedText.Contains("å"))
            {
                parsedText = parsedText.Replace("å", "%e5");
            }

            if (parsedText.Contains("Å"))
            {
                parsedText = parsedText.Replace("Å", "%c5");
            }

            if (parsedText.Contains("ü"))
            {
                parsedText = parsedText.Replace("ü", "%fc");
            }

            if (parsedText.Contains("Ü"))
            {
                parsedText = parsedText.Replace("Ü", "%dc");
            }

            if (parsedText.Contains("ö"))
            {
                parsedText = parsedText.Replace("ö", "%f6");
            }

            if (parsedText.Contains("Ö"))
            {
                parsedText = parsedText.Replace("Ö", "%d6");
            }

            if (parsedText.Contains("ÿ"))
            {
                parsedText = parsedText.Replace("ÿ", "%ff");
            }

            if (parsedText.Contains("ä"))
            {
                parsedText = parsedText.Replace("ä", "%e4");
            }

            if (parsedText.Contains("Ä"))
            {
                parsedText = parsedText.Replace("Ä", "%c4");
            }

            if (parsedText.Contains("ë"))
            {
                parsedText = parsedText.Replace("ë", "%eb");
            }

            if (parsedText.Contains("Ë"))
            {
                parsedText = parsedText.Replace("Ë", "%cb");
            }

            if (parsedText.Contains("…"))
            {
                parsedText = parsedText.Replace("…", "%e2%80%a6");
            }

            if (parsedText.Contains("–"))
            {
                parsedText = parsedText.Replace("–", "%96");
            }

            return parsedText;
        }

        /// <summary>
        /// UrlEncodes a string without the requirement for System.Web
        /// </summary>
        /// <param name="text">String to encode.</param>
        /// <returns>encoded string</returns>
        public static string UrlEncode(string text)
        {
            string reservedCharacters = "æÆøØåÅüÜöÖÿäÄëË…– ";

            if (String.IsNullOrEmpty(text))
                return String.Empty;

            var sb = new StringBuilder();

            foreach (char @char in text)
            {
                if (reservedCharacters.IndexOf(@char) == -1)
                    sb.Append(@char);
                else
                    sb.AppendFormat("%{0:X2}", (int)@char);
            }
            return sb.ToString();

            // System.Uri provides reliable parsing
            //return Uri.EscapeDataString(text);
        }

        /// <summary>
        /// UrlDecodes a string without requiring System.Web
        /// </summary>
        /// <param name="text">String to decode.</param>
        /// <returns>decoded string</returns>
        public static string UrlDecode(string text)
        {
            // pre-process for + sign space formatting since System.Uri doesn't handle it
            // plus literals are encoded as %2b normally so this should be safe
            text = text.Replace("+", " ");
            return Uri.UnescapeDataString(text);
        }

        /// <summary>
        /// Retrieves a value by key from a UrlEncoded string.
        /// </summary>
        /// <param name="urlEncoded">UrlEncoded String</param>
        /// <param name="key">Key to retrieve value for</param>
        /// <returns>returns the value or "" if the key is not found or the value is blank</returns>
        public static string GetUrlEncodedKey(string urlEncoded, string key)
        {
            urlEncoded = "&" + urlEncoded + "&";
            var index = urlEncoded.IndexOf("&" + key + "=", StringComparison.OrdinalIgnoreCase);

            if (index < 0)
                return "";

            var lnStart = index + 2 + key.Length;

            var index2 = urlEncoded.IndexOf("&", lnStart, StringComparison.Ordinal);

            if (index2 < 0)
                return "";

            return UrlDecode(urlEncoded.Substring(lnStart, index2 - lnStart));
        }

        /// <summary>
        /// Remove/Replace HTML p, /p, br elements.
        /// </summary>
        /// <param name="source">Source HTML string</param>
        /// <returns>returns a string with replaced tags</returns>
        public static string StripHtml(string source)
        {
            if (string.IsNullOrEmpty(source)) return "";
            var result = source
                .Replace("<h1>", "")
                .Replace("</h1>", "")
                .Replace("<h2>", "")
                .Replace("</h2>", "")
                .Replace("<h3>", "")
                .Replace("</h3>", "")
                .Replace("<h4>", "")
                .Replace("</h4>", "")
                .Replace("<ul>", "")
                .Replace("</ul>", "")
                .Replace("<li>", "")
                .Replace("</li>", "")
                .Replace("<p>", "")
                .Replace("<P>", "")
                .Replace("</p>", "\n")
                .Replace("</P>", "\n")
                .Replace("<span>", "")
                .Replace("</span>", "")
                .Replace("<strong>", "")
                .Replace("</strong>", "")
                .Replace("<br>", "\n")
                .Replace("<BR>", "\n")
                .Replace("<br />", "\n")
                .Replace("<BR />", "\n")
                .Replace("&lt;p&gt;", "\n")
                .Replace("&lt;/p&gt;", "\n")
                .Replace("<![CDATA[", "")
                .Replace("]]>", "")
                .Replace("&lt;/div&gt;", "")
                .Replace("</div>", "")
                .Replace("&amp;nbsp;", " ")
                .Replace("&nbsp;", " ")
                .Replace("&amp;", "&")
                .Replace("&#8211;", "–")
                .Replace("&#8217;", "’")
                .Replace("&#038;", "&")
                .Replace("<em>", "")
                .Replace("</em>", "")
                .Replace("</iframe>", "")
                .Replace("</a>", "");

            result = RemoveTag(result, "&lt;div ", "&gt;");
            result = RemoveTag(result, "<div ", ">");
            result = RemoveTag(result, "<a href", ">");
            result = RemoveTag(result, "<a rel", ">");
            result = RemoveTag(result, "<img src", ">");
            result = RemoveTag(result, "<iframe src", ">");
            result = RemoveTag(result, "<iframe width", ">");
            result = RemoveTag(result, "<p class", ">");

            result = result.Trim();
            return result;
        }

        /// <summary>
        /// Removes all string tags from a given source string.
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="tagPrefix">Tag prefix string</param>
        /// <param name="tagSuffix">Tag suffix string</param>
        /// <param name="isOneShot">Flag for one shot tag removal</param>
        /// <returns>returns a string without the given tag</returns>
        public static string RemoveTag(string source, string tagPrefix, string tagSuffix, bool isOneShot = false)
        {
            if (string.IsNullOrEmpty(source)) return "";
            if (!source.Contains(tagPrefix)) return source;
            if (!source.Contains(tagSuffix)) return source;
            var result = source;

            while (result.Contains(tagPrefix))
            {
                var startIndex = result.IndexOf(tagPrefix, StringComparison.Ordinal);
                var endIndex = result.IndexOf(tagSuffix, startIndex + tagPrefix.Length, StringComparison.Ordinal);
                var count = (endIndex + tagSuffix.Length) - startIndex;

                if ((startIndex > -1) & (count > -1))
                {
                    result = result.Remove(startIndex, count);
                }
                else
                {
                    break; // start or count are not within range so we break!
                } 

                if (isOneShot) break; // Stop further tag removals !
            }
            return result;
        }

        /// <summary>
        /// Remove a specified prefix from a text string.
        /// </summary>
        /// <param name="prefix">The prefix string to remove</param>
        /// <param name="text">The text string to parse</param>
        /// <returns>returns a string with the prefix removed</returns>
        public static string RemovePrefixFromText(string prefix, string text)
        {
            var result = string.IsNullOrEmpty(text) 
                ? text 
                : text.Replace(prefix, "");

            if (!string.IsNullOrEmpty(result))
            {
                var i = result.IndexOf(". ", StringComparison.Ordinal);

                if (i > -1 & i < 20)
                {
                    result = result.Substring(i + 2);
                }
            }

            return result;
        }

        /// <summary>
        /// Remove a specified suffix from a text string.
        /// </summary>
        /// <param name="suffix">The suffix string to remove</param>
        /// <param name="text">The text string to parse</param>
        /// <returns>returns a string with the suffix removed</returns>
        public static string RemoveSuffixFromText(string suffix, string text)
        {
            var result = text;

            if (!string.IsNullOrEmpty(text))
            {
                var i = text.IndexOf(suffix, StringComparison.Ordinal);

                if (i > -1)
                {
                    result = text.Substring(0, text.Length - (text.Length - i));
                }
            }
            return result;
        }
    }
}
