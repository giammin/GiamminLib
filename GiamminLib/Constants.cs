using System;
using System.Collections.Generic;

namespace GiamminLib
{
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

        /// <summary>
        /// elenco di tutti i simboli della tastiera
        /// </summary>
        public static readonly string SymbolsChars = "`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?";
        /// <summary>
        /// The unix epoch ticks
        /// </summary>
        public const long UnixEpochTicks = 621355968000000000; //(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks; 
        /// <summary>
        /// Il minimo valore di data ammesso su Sql Server
        /// </summary>
        public static readonly DateTime MinSqlValue = new DateTime(1753, 1, 1);

        /// <summary>
        /// caratteri non consentiti su filesystem windows
        /// </summary>
        public static readonly IList<char> FileSystemSpecialChar = new List<char> { '<', '>', ':', '"', '/', '\\', '|', '?', '*', '\t' };
        /// <summary>
        /// caratteri non consentiti ad un file o immagine in ambiente web. vanno abbinati ai <see cref="FileSystemSpecialChar"/>
        /// </summary>
        public static readonly IList<char> WebFileSpecialChar = new List<char> { '&', ';', '%', ',', '\'', '+', '=', '$', '#', '@' };
    }
}
