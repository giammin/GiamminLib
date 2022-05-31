using System;
using System.Security.Cryptography;
using System.Text;

namespace GiamminLib.Security
{
    /// <summary>
    /// Thread safe helper class for generating random string, password, number
    /// </summary>
    public class RandomGenerator
    {
        private const int DefaultLength = 6;
        /// <summary>
        /// caratteri minuscoli da utilizzare
        /// </summary>
        public string LowerChars  {get;set;}
        /// <summary>
        /// caratteri maiuscoli da utilizzare
        /// </summary>
        public string UpperChars  {get;set;}
        /// <summary>
        /// numeri da utilizzare
        /// </summary>
        public string NumberChars   {get;set;}
        /// <summary>
        /// simboli da utilizzare
        /// </summary>
        public string SymbolsChars { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Random"/> class.
        /// </summary>
        public RandomGenerator()
        {
            LowerChars = Constants.LowerChars;
            UpperChars = Constants.UpperChars;
            NumberChars = Constants.NumberChars;
            SymbolsChars = Constants.SymbolsChars;
        }

        /// <summary>
        /// Generates a random string with specified string length.
        /// </summary>
        /// <param name="stringLength">Length of the string to generate.</param>
        /// <param name="useLowerCase">if set to <c>true</c> use lower case chars.</param>
        /// <param name="useUpperCase">if set to <c>true</c> use upper case chars.</param>
        /// <param name="useNumbers">if set to <c>true</c> use numbers chars.</param>
        /// <param name="useSymbols">if set to <c>true</c> use symbols chars.</param>
        /// <returns></returns>
        public string GenerateString(int stringLength = DefaultLength, bool useLowerCase = true, bool useUpperCase = false, bool useNumbers = true, bool useSymbols = false)
        {
            var rtn = new StringBuilder();
            var chars = new StringBuilder();

            if (useLowerCase)
            {
                chars.Append(LowerChars);
            }
            if (useUpperCase)
            {
                chars.Append(UpperChars);
            }
            if (useNumbers)
            {
                chars.Append(NumberChars);
            }
            if (useSymbols)
            {
                chars.Append(SymbolsChars);
            }

            for (int i = 0; i < stringLength; i++)
            {
#if NET5_0_OR_GREATER
                rtn.Append(chars[RandomNumberGenerator.GetInt32(chars.Length)]);
#else
                rtn.Append(chars[GenerateNumber(chars.Length - 1, true)]);
#endif
            }

            return rtn.ToString();
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Generates a positive number.
        /// </summary>
        /// <param name="maxNumber">The max number.</param>
        /// <param name="includeZero">if set to <c>true</c> zero could be generated.</param>
        /// <returns></returns>
        [Obsolete("lasciato per compatibilità usare direttamente RandomNumberGenerator.GetInt32(minNumber, maxNumber);")]
        public int GenerateNumber(int maxNumber,bool includeZero = false)
        {
            int minNumber = includeZero ? 0 : 1;
            if (maxNumber < minNumber)
            {
                throw new ArgumentException(string.Concat("The maxNumber value should be greater than ", minNumber),nameof(maxNumber));
            }
            return RandomNumberGenerator.GetInt32(minNumber, maxNumber);
        }
#else
        /// <summary>
        /// Generates a positive number.
        /// </summary>
        /// <param name="maxNumber">The max number.</param>
        /// <param name="includeZero">if set to <c>true</c> zero could be generated.</param>
        /// <returns></returns>
        public int GenerateNumber(int maxNumber, bool includeZero = false)
        {
            int minNumber = includeZero ? 0 : 1;
            if (maxNumber < minNumber)
            {
                throw new ArgumentException(string.Concat("The maxNumber value should be greater than ", minNumber), nameof(maxNumber));
            }
            var b = new byte[4];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(b);
            //tolgo i byte che non servono per avere un int
            int seed = (b[0] & 0x7f) << 24 | b[1] << 16 | b[2] << 8 | b[3];
            var random = new Random(seed);
            return random.Next(minNumber, maxNumber);
        }
#endif
    }
}
