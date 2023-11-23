using System;
using System.Collections.Generic;
using System.Linq;

namespace GiamminLib.ExtensionMethods;

public static class EnumerableExtensions
{

    /// <summary>
    /// compare 2 lists using the delegates passed as argument and returns the list of differences
    /// </summary>
    /// <typeparam name="TNewList"></typeparam>
    /// <typeparam name="TOldList"></typeparam>
    /// <param name="oldList">parent list</param>
    /// <param name="newList">modified list</param>
    /// <param name="isSameElement">comparison for identify if the item is the same entity (if a db row it should be the identity check poco.Id==poco2.id)</param>
    /// <param name="isEqual">reference equal comparison</param>
    /// <returns></returns>
    public static CompareResult<TOldList, TNewList> GetDifferences<TOldList, TNewList>(this IEnumerable<TOldList> oldList, IEnumerable<TNewList> newList, Func<TOldList, TNewList, bool> isEqual, Func<TOldList, TNewList, bool> isSameElement)
        where TOldList : notnull
        where TNewList : notnull
    {
        var rtn = new CompareResult<TOldList, TNewList>();

        var oldItems = oldList.ToList();
        var newItems = newList.ToList();

        foreach (var oldItem in oldItems)
        {
            var findItem = newItems.FirstOrDefault(x => isSameElement(oldItem, x));
            if (findItem == null)
            {
                rtn.Removed.Add(oldItem);
            }
            else
            {
                if (isEqual(oldItem, findItem))
                {
                    rtn.UnModified.Add(oldItem, findItem);
                }
                else
                {
                    rtn.Modified.Add(oldItem, findItem);
                }

                newItems.Remove(findItem);
            }
        }
        rtn.Added.AddRange(newItems);

        return rtn;
    }

    public record CompareResult<TOldList, TNewList>
        where TOldList : notnull
        where TNewList : notnull
    {
        public Dictionary<TOldList, TNewList> UnModified { get; } = [];

        public Dictionary<TOldList, TNewList> Modified { get; } = [];
        public List<TOldList> Removed { get; } = [];
        public List<TNewList> Added { get; } = [];

        public bool AreEqual => Removed.Count + Added.Count + Modified.Count == 0;
    }
}