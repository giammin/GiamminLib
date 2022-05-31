using System;
using System.ComponentModel;

namespace GiamminLib.ExtensionMethods
{
    /// <summary>
    /// Extension methods per gli enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns a custom string instead of <see cref="Enum.ToString()"/> when  <see cref="DescriptionAttribute"/> is specified.
        /// <example>[Description("testo personalizzato")] </example>
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description: enumValue.ToString();
        }
    }
}
