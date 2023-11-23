using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace GiamminLib.Security;

/// <summary>
/// Thread safe helper class for generating random string, password, number
/// </summary>
public class RandomGenerator
{
    private readonly char[]? _lowerChars;
    private readonly char[]? _upperChars;
    private readonly char[]? _numberChars;
    private readonly char[]? _symbolChars;
    private readonly object _locker = new();

    public RandomGenerator(char[]? lowerChars = null, char[]? upperChars = null, char[]? numberChars = null, char[]? symbolChars = null)
    {
        _lowerChars = lowerChars;
        _upperChars = upperChars;
        _numberChars = numberChars;
        _symbolChars = symbolChars;
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
    public string GenerateString(int stringLength, bool useLowerCase = true, bool useUpperCase = false, 
        bool useNumbers = true, bool useSymbols = false)
    {
        if (stringLength<1)
        {
            throw new ArgumentOutOfRangeException(nameof(stringLength));
        }

        lock (_locker)
        {
            var charsToUse = new List<char>();

            if (useLowerCase)
            {
                charsToUse.AddRange(_lowerChars??Constants.LowerAsciiChars);
            }
            if (useUpperCase)
            {
                charsToUse.AddRange(_upperChars??Constants.UpperAsciiChars);
            }
            if (useNumbers)
            {
                charsToUse.AddRange(_numberChars??Constants.NumbersChars);
            }
            if (useSymbols)
            {
                charsToUse.AddRange(_symbolChars??Constants.SymbolAsciiChars);
            }

            if (charsToUse.Count==0)
            {
                throw new ArgumentException("no chars selected");
            }
            return RandomNumberGenerator.GetString(charsToUse.ToArray(), stringLength);
        }
    }
}