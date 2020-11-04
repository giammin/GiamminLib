
namespace GiamminLib
{

    /// <summary>
    /// This enum identifies all kind of char according to: http://msdn.microsoft.com/it-it/library/system.globalization.unicodecategory.aspx
    /// </summary>
    public enum CharType
    {
        /// <summary>
        /// Indicates whether a specified Unicode character is categorized as a control character.
        /// </summary>
        Control,
        /// <summary>
        /// Indicates whether a Unicode character is categorized as a decimal digit.
        /// </summary>
        Digit,
        /// <summary>
        /// Indicates whether a Unicode character is categorized as an alphabetic letter.
        /// </summary>
        Letter,
        /// <summary>
        /// Indicates whether a Unicode character is categorized as a punctuation mark.
        /// </summary>
        Punctuation,
        /// <summary>
        /// Indicates whether a Unicode character is categorized as a separator character.
        /// </summary>
        Separator,
        /// <summary>
        /// Indicates whether the specified Unicode character is categorized as a symbol character.
        /// </summary>
        Symbol,
        /// <summary>
        /// Indicates whether a Unicode character is categorized as white space.
        /// </summary>
        Whitespace,
        /// <summary>
        /// If it does not below to other categories
        /// </summary>
        Unknown,
        /// <summary>
        /// <see cref="CharExtensions.IsPrintable"/>
        /// </summary>
        Printable
    }
}
