using System;
using System.Text;

namespace GiamminLib.ExtensionMethods
{
    /// <summary>
    /// estension methods per i valuetype rappresentanti numeri interi
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Converts <paramref name="valueToConvert"/> to its corrispettive string in Base <paramref name="targetBase"/>.
        /// This method use the following base alphabet:
        /// if <b>targetBase</b> is less or equal 32:  Base32Hex
        /// if <b>targetBase</b> is less or equal 64:  Base64
        /// <remarks>When possible use default .net methods for converting to different Base. This method is only for special needs</remarks>
        /// </summary>
        /// <param name="valueToConvert">The value to convert.</param>
        /// <param name="targetBase">The target base.</param>
        /// <returns>a string that rappresent the passed value in selected base</returns>
        public static string ConvertToBase(this byte valueToConvert, int targetBase) => ConvertNumber(valueToConvert, targetBase);
        /// <summary>
        /// Converts <paramref name="valueToConvert"/> to its corrispettive string in Base <paramref name="targetBase"/>.
        /// This method use the following base alphabet:
        /// if <b>targetBase</b> is less or equal 32:  Base32Hex
        /// if <b>targetBase</b> is less or equal 64:  Base64
        /// <remarks>When possible use default .net methods for converting to different Base. This method is only for special needs</remarks>
        /// </summary>
        /// <param name="valueToConvert">The value to convert.</param>
        /// <param name="targetBase">The target base.</param>
        /// <returns>a string that rappresent the passed value in selected base</returns>
        public static string ConvertToBase(this short valueToConvert, int targetBase) => ConvertNumber(valueToConvert, targetBase);

        /// <summary>
        /// Converts <paramref name="valueToConvert"/> to its corrispettive string in Base <paramref name="targetBase"/>.
        /// This method use the following base alphabet:
        /// if <b>targetBase</b> is less or equal 32:  Base32Hex
        /// if <b>targetBase</b> is less or equal 64:  Base64
        /// <remarks>When possible use default .net methods for converting to different Base. This method is only for special needs</remarks>
        /// </summary>
        /// <param name="valueToConvert">The value to convert.</param>
        /// <param name="targetBase">The target base.</param>
        /// <returns>a string that rappresent the passed value in selected base</returns>
        public static string ConvertToBase(this int valueToConvert, int targetBase) => ConvertNumber(valueToConvert, targetBase);
        /// <summary>
        /// Converts <paramref name="valueToConvert"/> to its corrispettive string in Base <paramref name="targetBase"/>.
        /// This method use the following base alphabet:
        /// if <b>targetBase</b> is less or equal 32:  Base32Hex
        /// if <b>targetBase</b> is less or equal 64:  Base64
        /// <remarks>When possible use default .net methods for converting to different Base. This method is only for special needs</remarks>
        /// </summary>
        /// <param name="valueToConvert">The value to convert.</param>
        /// <param name="targetBase">The target base.</param>
        /// <returns>a string that rappresent the passed value in selected base</returns>
        public static string ConvertToBase(this long valueToConvert, int targetBase) => ConvertNumber(valueToConvert, targetBase);

        /// <summary>
        /// Converts <paramref name="valueToConvert"/> to its corrispettive string in Base <paramref name="targetBase"/>, using  <paramref name="baseChars"/> mapping
        /// </summary>
        /// <remarks>When possible use default .net methods for converting to different Base. This method is only for special needs</remarks>
        /// <param name="valueToConvert">The value to convert.</param>
        /// <param name="targetBase">The target base.</param>
        /// <param name="baseChars">The alphabet.</param>
        /// <returns>a string that rappresent the passed value in selected base</returns>
        public static string ConvertToBase(this long valueToConvert, int targetBase, char[] baseChars) => ConvertNumber(valueToConvert, targetBase, baseChars);

        private static string ConvertNumber(long valueToConvert, int targetBase)
        {
            char[] basechar = Constants.DefaultBase32HexChars;
            if (targetBase > Constants.DefaultBase32HexChars.Length)
            {
                basechar = Constants.DefaultBase64Chars;
            }
            return ConvertNumber(valueToConvert, targetBase, basechar);
        }
        private static string ConvertNumber(long valueToConvert, int targetBase, char[] baseChars)
        {
            if (targetBase > baseChars.Length)
            {
                throw new ArgumentException("baseChars must be lenght at least as toBase.", nameof(targetBase));
            }
            if (valueToConvert<0)
            {
                throw new ArgumentException("valueToConvert must be positive.", nameof(valueToConvert));
            }
            var rtn = new StringBuilder();
            var tmpValue = valueToConvert;

            do
            {
                rtn.Append(baseChars[tmpValue % targetBase]);
                tmpValue /= targetBase;
            }
            while (tmpValue > 0);
            return rtn.ToString();
        }
    }
}
