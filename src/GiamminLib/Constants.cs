using System;
using System.Collections.Frozen;
using System.Collections.Generic;

namespace GiamminLib;

public static class Constants
{
    //private static readonly char[] lowerAsciiChars = Enumerable.Range(97, 26).Select(x => (char)x).ToArray();
    //private static readonly char[] upperAsciiChars = Enumerable.Range(65, 26).Select(x => (char)x).ToArray();
    //private static readonly char[] numbersChars = Enumerable.Range(48, 10).Select(x => (char)x).ToArray();
    ////private static readonly int[] SymbolsAsciiChars = [.. Enumerable.Range(33, 15), .. Enumerable.Range(58, 7), .. Enumerable.Range(91, 6), .. Enumerable.Range(123, 4)];
    
    public static readonly char[] LowerAsciiChars = {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    };

    public static readonly char[] UpperAsciiChars = {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    };
    public static readonly char[] NumbersChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public static readonly char[] SymbolAsciiChars = {
        '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@',
        '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'
    };

    //public static ReadOnlySpan<char> UpperAsciiChars => upperAsciiChars;
    //public static ReadOnlySpan<char> LowerAsciiChars => lowerAsciiChars;
    //public static ReadOnlySpan<char> NumberChars => numbersChars;
    //public static ReadOnlySpan<char> SymbolAsciiChars => symbolAsciiChars;

    public const int DefaultSaltSize = 16;

   
    /// <summary>
    /// The unix epoch ticks
    /// </summary>
    public const long UnixEpochTicks = 621355968000000000; //(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks; 


    #region signatures
    public static readonly FrozenDictionary<string, List<byte[]>>FileSignatures = new Dictionary<string, List<byte[]>>
    {
        { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
        { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
        { ".jpeg",
            new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            }
        },
        { ".jpg",
            new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
            }
        },
        { ".cvs", new List<byte[]> { Array.Empty<byte>() } },
        { ".txt", new List<byte[]> { Array.Empty<byte>() } },
        { ".doc",
            new List<byte[]>
            {
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
            }
        },
        {
            ".pdf",
            new List<byte[]>
            {
                new byte[] { 0x25, 0x50, 0x44, 0x46 }
            }
        },
        {
            ".pps",
            new List<byte[]>
            {
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
            }
        },
        {
            ".ppt",
            new List<byte[]>
            {
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
            }
        },
        {
            ".xls",
            new List<byte[]>
            {
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
            }
        },
        {
            ".docx",
            new List<byte[]>
            {
                new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
            }
        },
        {
            ".xlsx",
            new List<byte[]>
            {
                new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
            }
        },
        {
            ".pptx",
            new List<byte[]>
            {
                new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
            }
        },
        {
            ".zip",
            new List<byte[]>
            {
                new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
            }
        },
    }.ToFrozenDictionary();
    #endregion
}