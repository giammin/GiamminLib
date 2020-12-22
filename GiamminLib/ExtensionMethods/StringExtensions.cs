using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace GiamminLib.ExtensionMethods
{
	///<summary>
	/// Extension Methods per le stringhe
	///</summary>
	public static class StringExtensions
	{
        /// <summary>
        /// Extension Methods per string.IsNullOrEmptyOrWhiteSpace(strToConvert) ? nullString : strToConvert
        /// </summary>
        /// <param name="nullString">la stringa da ritornare se <paramref name="strToConvert"/> è nulla o vuota</param>
        /// <param name="strToConvert">la stringa da controllare</param>
        /// <returns><paramref name="strToConvert"/> se non è nulla o vuota altrimenti <paramref name="nullString"/></returns>
        public static string ConvertNullOrEmptyTo(this string strToConvert, string nullString) 
            => string.IsNullOrEmpty(strToConvert.ToStringSafe().Trim()) ? nullString : strToConvert;
        /// <summary>
        /// gestisce se l'oggetto è nullo convertendolo in <see cref="string.Empty"/>
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string ToStringSafe(this object obj) => (obj ?? string.Empty).ToString();

	    /// <summary>
		/// converte la stringa in un array di byte senza doversi curare dell'encoding.
		/// Va usata quando le stringe rimangono nello stesso sistema dotnet
        /// equivale ad usare System.Text.Encoding.Unicode.GetBytes()
		/// http://stackoverflow.com/questions/472906/converting-a-string-to-byte-array
		/// </summary>
		public static byte[] GetBytes(this string str)
		{
			var rtn = new byte[str.Length * sizeof(char)];
			Buffer.BlockCopy(str.ToCharArray(), 0, rtn, 0, rtn.Length);
			return rtn;
		}
        /// <summary>
        /// converte un array di byte in una stringa senza problemi di encoding
        /// Va usata quando le stringe rimangono nello stesso sistema dotnet
        /// equivale ad usare System.Text.Encoding.Unicode.GetString()
        /// http://stackoverflow.com/questions/472906/converting-a-string-to-byte-array
        /// </summary>
		public static string GetString(this byte[] bytes)
		{
			var chars = new char[bytes.Length / sizeof(char)];
			Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
		/// <summary>
		/// conta le parole contenute nella stringa corrente, controllare se sono inseriti tutti i separatori
		/// <remarks> i separatori usati sono: ' ', '.', ',', ':', ';', '?', '!'</remarks>
		/// </summary>
		/// <returns>count of word</returns>
		public static int WordsCount(this String str)
		{
			var separator = new[] { " ", ".", ",", ":", ";", "?", "!" };
			return str.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length;
		}
        

		/// <summary>
		/// Determina se la stringa ha tutti i caratteri appartenenti al "ASCII printable characters" ASCII 128
		/// http://en.wikipedia.org/wiki/ASCII#ASCII_printable_characters
		/// </summary>
		/// <param name="str">la stringa</param>
		/// <returns>
		///     <c>true</c> se [se la stringa ha tutti i caratteri appartenenti al "ASCII printable characters" ASCII 128] ; altrimenti, <c>false</c>.
		/// </returns>
		public static Boolean IsPrintable(this string str)
		{
			bool rtn = true;

			for (int i = 0; i < str.Length; i++)
			{
				if (!str[i].IsPrintable())
				{
					rtn = false;
					break;
				}
			}
			return rtn;
		}


#nullable enable
		/// <summary>
		/// Tronca un dato testo per un dato numero di caratteri preservando le parole
		/// </summary>
		/// <param name="fullText">Il testo da troncare</param>
		/// <param name="maxLength">La lunghezza massima</param>
		/// <param name="appendText">Il testo da appendere in caso di troncamento (es. punti di sospensione)</param>
		/// <returns></returns>
		public static string Truncate(this string fullText, int maxLength, string? appendText = null)
		{
			string rtn = fullText.ToStringSafe();

			if (fullText.Length > maxLength)
			{
				//sostituito fullText.LastIndexOf(" ", 0, maxLenght) perché in alcuni casi dava eccezione ArgumentOutOfRangeException
				rtn = fullText.Substring(0, maxLength);

			    if (appendText!=null)
			    {
			        rtn = string.Concat(rtn.TrimEnd(' ', '.'), appendText);
			    }
			}
			return rtn;
		}

#nullable disable
		/// <summary>
		/// Removes the HTML/XML/XHTML tags from the string
		/// </summary>
		/// <param name="fullText">The string to parse.</param>
		/// <returns></returns>
		public static string RemoveHtmlTags(this string fullText)
		{
			var regexStripHtml = new Regex("<[^>]+>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return regexStripHtml.Replace(fullText, string.Empty);
		}

        /// <summary>
        /// catch any kind of whitespace (e.g. tabs, newlines, etc.) and replace them with a single space.
        /// </summary>
        /// <param name="fullText">The string to parse.</param>
        /// <returns></returns>
        public static string RemoveSpaces(this string fullText)
        {
            var regexStripHtml = new Regex(@"\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return regexStripHtml.Replace(fullText, " ");
        }
        /// <summary>
        /// Transforme the string in the TitleCase version
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        /// <summary>
        /// converts to lower only the first char
        /// </summary>
        public static string ToCamelCase(this string str)
        {
            string rtn=str;

            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]) )
            {
                rtn = Char.ToLowerInvariant(str[0]).ToString(CultureInfo.InvariantCulture);
                if (str.Length>1)
                {
                    rtn = string.Concat(rtn, str[1..]);
                }
            }
            return rtn;
        }

		/// <summary>
		/// Convert string to enum of type T
		/// Volutamente lancia un'eccezione se la string non è un valido enum
		/// </summary>
		/// <typeparam name="T">deve essere un enum</typeparam>
		public static T ToEnum<T>(this string enumString) where T : struct // enum non si può mettere... prima di .net 4
		{
			if (String.IsNullOrEmpty(enumString) || !typeof(T).IsEnum)
			{
				throw new Exception("Type given must be an Enum");
			}
			return (T) Enum.Parse(typeof (T), enumString, true);
		}

		/// <summary>
		/// Replace the given <paramref name="stringsToRemove"/> from string with <paramref name="newString"/>.
		/// </summary>
		/// <param name="str">current string</param>
		/// <param name="stringsToRemove">The string to replace.</param>
		/// <param name="newString">the new string</param>
		/// <returns></returns>
		public static string Replace(this string str, IEnumerable<string> stringsToRemove, string newString = "")
		{
		    var rtn = new StringBuilder(str);

		    foreach (string item in stringsToRemove)
		    {
		        rtn.Replace(item, newString);
		    }
			return rtn.ToString();
		}

		/// <summary>
		/// Replace the given <paramref name="charsToRemove"/> from string with <paramref name="newString"/>.
		/// </summary>
		/// <param name="str">current string</param>
		/// <param name="charsToRemove">The char to replace.</param>
		/// <param name="newString">the new string</param>
		/// <returns></returns>
		public static string Replace(this string str, IEnumerable<char> charsToRemove, string newString = "")
		{
            var rtn = new StringBuilder();
            var hs = new HashSet<char>(charsToRemove);
            foreach (char item in str)
            {
                if (hs.Contains(item))
                {
                    rtn.Append(newString);
                }
                else
                {
                    rtn.Append(item);
                }
            }
            return rtn.ToString();
		}

        /// <summary>
        /// Encodes a string to be represented as a string literal. The format is essentially a JSON string.
        /// </summary>
        /// <param name="str">string to encode for javascript</param>
        /// <returns>encoded string with outer quotes Example Output: 'Hello \"Rick\"!\r\nRock on'</returns>
        public static string EncodeJsString(this string str)
        {
            var rtn = new StringBuilder("'");

            foreach (char item in str)
            {
                switch (item)
                {
                    case '\'':
                        rtn.Append("\\'");
                        break;
                    case '\"':
                        rtn.Append("\\\"");
                        break;
                    case '\\':
                        rtn.Append("\\\\");
                        break;
                    case '\b':
                        rtn.Append("\\b");
                        break;
                    case '\f':
                        rtn.Append("\\f");
                        break;
                    case '\n':
                        rtn.Append("\\n");
                        break;
                    case '\r':
                        rtn.Append("\\r");
                        break;
                    case '\t':
                        rtn.Append("\\t");
                        break;
                    default:
                        int i = item;
                        if (i < 32 || i > 127)
                        {
                            rtn.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            rtn.Append(item);
                        }
                        break;
                }
            }
            rtn.Append("'");

            return rtn.ToString();
        }
	}
}
