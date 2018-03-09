using System;
using System.Linq;
using System.Text;

namespace DanishMovies.Utility
{
    public static partial class Extension
    {
        /// <summary>
        /// Substring extension with support for making sure we do not exceed the source text length.
        /// </summary>
        /// <param name="text">The source text string</param>
        /// <param name="startIndex">The start index</param>
        /// <param name="length">The length of the substring</param>
        /// <param name="suffix">An optional suffix for the substring</param>
        /// <returns>A substring (with optional suffix) or the source text string</returns>
        public static string SubstringWithMaxLength(this string text, int startIndex, int length, string suffix = "")
        {
            return length < text.Length ? String.Format("{0}{1}", text.Substring(startIndex, length), suffix) : text;
        }
    }

    public static class StringHelper
    {
        public static string BuildShortString(string longString, bool hasImage, bool isLargeScreen = false)
        {
            if (string.IsNullOrEmpty(longString)) return "";

            var result = hasImage
                ? longString.SubstringWithMaxLength(0, isLargeScreen ? 280 : 140, "...")
                : longString.SubstringWithMaxLength(0, 280, "...");

            if (result.Split('\n').Count() > 3)
            {
                result = hasImage
                    ? result.SubstringWithMaxLength(0, isLargeScreen ? 200 : 100, "...")
                    : result.SubstringWithMaxLength(0, 150, "...");
            }
            result = result.Replace("\n\n", "\n"); // Remove any empty lines.
            return result;
        }

        /// <summary>
        /// base64 encode a given string
        /// </summary>
        /// <param name="plainText">The string to encode</param>
        /// <returns>base64 encoded string</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode a given base64 encoded string
        /// </summary>
        /// <param name="base64EncodedData">base64 encoded string</param>
        /// <returns>Decoded string</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
    }
}
