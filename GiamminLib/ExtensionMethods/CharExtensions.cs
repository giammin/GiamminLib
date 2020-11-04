using System.Globalization;

namespace GiamminLib.ExtensionMethods
{
    ///<summary>
    /// Estende le funzionalità standard del tipo Chart di .NET
    ///</summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Dice a quale categoria Unicode appartiene il carattere
        /// </summary>
        /// <returns>custom enum CharType</returns>
        public static CharType GetCharType(this char c)
        {
            if (char.IsControl(c))
                return CharType.Control;
            if (char.IsDigit(c))
                return CharType.Digit;
            if (char.IsLetter(c))
                return CharType.Letter;
            if (char.IsPunctuation(c))
                return CharType.Punctuation;
            if (char.IsSeparator(c))
                return CharType.Separator;
            if (char.IsSymbol(c))
                return CharType.Symbol;
            if (char.IsWhiteSpace(c))
                return CharType.Whitespace;
            if (c.IsPrintable())
                return CharType.Printable;
            
            return CharType.Unknown;
        }


        /// <summary>
        /// Determina se [il carattere specificato] [e' stampabile] o se appartiene al range dei carattere ASCII 128
        /// http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters
        /// </summary>
        /// <param name="c">Il carattere c.</param>
        /// <returns>
        /// 	<c>true</c> se [il carattere e' stampabile] ; altrimenti, <c>false</c>.
        /// </returns>
        public static bool IsPrintable(this char c)
        {
            int code = c;
            return code >= 32 && code <= 126;
        }

        /// <summary>
        /// Determina se il carattere corrisponde ad un carattere speciale di windows con il quale e' impossibile salvare i file.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static bool IsWinFileSystemSpecialChar(this char c) => Constants.FileSystemSpecialChar.Contains(c);
        /// <summary>
        /// Remaps an international character to ASCII.
        /// Es: à --> a
        /// <remarks>http://meta.stackexchange.com/questions/7435/non-us-ascii-characters-dropped-from-full-profile-url/7696#7696</remarks>
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static string RemapInternationalCharToAscii(this char c)
        {
            //
            string s = c.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            if ("èéêëę".Contains(s))
            {
                return "e";
            }
            if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            if ("żźž".Contains(s))
            {
                return "z";
            }
            if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            if ("ñń".Contains(s))
            {
                return "n";
            }
            if ("ýÿ".Contains(s))
            {
                return "y";
            }
            if ("ğĝ".Contains(s))
            {
                return "g";
            }
            if (c == 'ř')
            {
                return "r";
            }
            if (c == 'ł')
            {
                return "l";
            }
            if (c == 'đ')
            {
                return "d";
            }
            if (c == 'ß')
            {
                return "ss";
            }
            if (c == 'Þ')
            {
                return "th";
            }
            if (c == 'ĥ')
            {
                return "h";
            }
            if (c == 'ĵ')
            {
                return "j";
            }
            return s;
        }
    }
}
