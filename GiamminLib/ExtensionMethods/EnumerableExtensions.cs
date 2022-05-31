using System;
using System.Collections.Generic;
using System.Linq;

namespace GiamminLib.ExtensionMethods;

public static class EnumerableExtensions
{

    /// <summary>
    /// paragona 2 liste e in base alle funzioni passate per il confronto degli elementi crea un risultato con le differenze
    /// </summary>
    /// <typeparam name="TNewList"></typeparam>
    /// <typeparam name="TOldList"></typeparam>
    /// <param name="oldList">list vecchia</param>
    /// <param name="newList">lista nuova</param>
    /// <param name="isSameElement">funzione usata per capire se è lo stesso elemento (solitamente si usa l'id istanza)</param>
    /// <param name="isEqual">funzione usata per capire se le 2 istanze sono identiche</param>
    /// <returns></returns>
    public static CompareResult<TOldList, TNewList> Compare<TOldList, TNewList>(this IEnumerable<TOldList> oldList, IEnumerable<TNewList> newList, Func<TOldList, TNewList, bool> isEqual, Func<TOldList, TNewList, bool> isSameElement)
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
                    rtn.Equal.Add(oldItem, findItem);
                }
                else
                {
                    rtn.Different.Add(oldItem, findItem);
                }

                newItems.Remove(findItem);
            }
        }
        rtn.Added.AddRange(newItems);

        return rtn;
    }

    public class CompareResult<TOldList, TNewList>
        where TOldList : notnull
        where TNewList : notnull
    {
        public Dictionary<TOldList, TNewList> Equal { get; init; } = new();

        public Dictionary<TOldList, TNewList> Different { get; init; } = new();
        public List<TOldList> Removed { get; init; } = new();
        public List<TNewList> Added { get; init; } = new();

        public bool IsSame => TotalDifferences == 0;

        public int TotalDifferences => Removed.Count + Added.Count + Different.Count;
    }
}