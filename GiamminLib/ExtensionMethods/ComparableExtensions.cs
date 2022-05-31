using System;

namespace GiamminLib.ExtensionMethods
{
    /// <summary>
    /// ExtensionMethods for IComparable interface
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// calcola se l'oggetto è compreso nelle nei due limiti specificati
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lowerBoundary">il limite inferiore</param>
        /// <param name="upperBoundary">il limite superiore</param>
        /// <param name="includeLowerBoundary">se &gt;=  o &gt; del limite inferiore</param>
        /// <param name="includeUpperBoundary">se &lt;= o &lt; del limite inferiore</param>
        /// <returns></returns>
        public static bool Between(this IComparable value, IComparable lowerBoundary, IComparable upperBoundary,
            bool includeLowerBoundary = true, bool includeUpperBoundary = true)
        {
            var lower = value.CompareTo(lowerBoundary);
            var upper = value.CompareTo(upperBoundary);
            return (lower > 0 || (includeLowerBoundary && lower == 0)) && (upper < 0 || (includeUpperBoundary && upper == 0));
        }
    }
}