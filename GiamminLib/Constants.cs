using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GiamminLib;



#region temporaneo per bug framework

//https://stackoverflow.com/a/62656145/921690
[EditorBrowsable(EditorBrowsableState.Never)]
public record IsExternalInit;

#endregion

/// <summary>
/// costanti utilizzate nel progetto
/// </summary>
public static class Constants
{
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!
    //UTILIZZARE CONST INVECE DI READONLY SOLO DOVE SI È SICURI CHE IL VALORE NON CAMBIERÀ MAI!!!!

    /// <summary>
    /// default Base32Hex, Base16, Base8, Base2 chars mapping
    /// </summary>
    public static readonly char[] DefaultBase32HexChars = { '0','1','2','3','4','5','6','7','8','9',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V'};

    /// <summary>
    /// default Base64 chars mapping
    /// </summary>
    public static readonly char[] DefaultBase64Chars = {
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','0','1','2','3','4','5','6','7','8','9','+','/'};


    /// <summary>
    /// elenco di tutti i caratteri minuscoli in ordine alfabetico
    /// </summary>
    public const string LowerChars = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// elenco di tutti i caratteri maiuscoli in ordine alfabetico
    /// </summary>
    public const string UpperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    /// <summary>
    /// elenco di tutti i caratteri rappresentanti numeri
    /// </summary>
    public const string NumberChars = "0123456789";

    public const int DefaultSaltSize = 16;

    /// <summary>
    /// elenco di tutti i simboli della tastiera
    /// </summary>
    public static readonly string SymbolsChars = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
    /// <summary>
    /// ne mancano tanti https://docs.microsoft.com/en-us/dotnet/api/system.char.isseparator?redirectedfrom=MSDN&view=net-6.0#System_Char_IsSeparator_System_Char_
    /// </summary>
    public static readonly string SeparatorChars = " \t\n";
    /// <summary>
    /// The unix epoch ticks
    /// </summary>
    public const long UnixEpochTicks = 621355968000000000; //(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks; 

    /// <summary>
    /// caratteri non consentiti su filesystem windows
    /// </summary>
    public static readonly IList<char> FileSystemSpecialChar = new List<char> { '<', '>', ':', '"', '/', '\\', '|', '?', '*', '\t' };
    /// <summary>
    /// caratteri non consentiti ad un file o immagine in ambiente web. vanno abbinati ai <see cref="FileSystemSpecialChar"/>
    /// </summary>
    public static readonly IList<char> WebFileSpecialChar = new List<char> { '&', ';', '%', ',', '\'', '+', '=', '$', '#', '@' };

    #region signatures
    public static readonly IReadOnlyDictionary<string, List<byte[]>> FileSignatures = new Dictionary<string, List<byte[]>>()
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
    };
    #endregion
}