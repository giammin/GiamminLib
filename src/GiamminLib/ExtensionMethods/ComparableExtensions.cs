using System;

namespace GiamminLib.ExtensionMethods;

/// <summary>
/// ExtensionMethods for IComparable interface
/// </summary>
public static class ComparableExtensions
{
    /// <summary>
    /// check if <paramref name="value"/> is between <paramref name="lowerBoundary"/>  and <paramref name="upperBoundary"/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="lowerBoundary">the lower bound</param>
    /// <param name="upperBoundary">the upper bound</param>
    /// <param name="includeLowerBoundary">&gt;=  or &gt; of lower bound</param>
    /// <param name="includeUpperBoundary">&lt;= or &lt; of upper bound</param>
    /// <returns></returns>
    public static bool IsBetween(this IComparable value, IComparable lowerBoundary, IComparable upperBoundary,
        bool includeLowerBoundary = true, bool includeUpperBoundary = true)
    {
        var lower = value.CompareTo(lowerBoundary);
        var upper = value.CompareTo(upperBoundary);
        return (lower > 0 || (includeLowerBoundary && lower == 0)) && (upper < 0 || (includeUpperBoundary && upper == 0));
    }
}