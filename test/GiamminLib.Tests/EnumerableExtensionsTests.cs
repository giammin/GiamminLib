using System.Collections.Generic;
using GiamminLib.ExtensionMethods;
using Xunit;

namespace GiamminLib.Tests;

public class EnumerableExtensionsTests
{
    [Theory]
    [MemberData(nameof(Create3))]
    public void CompareRefType_VariousLists(List<Dummy> oldList, List<Dummy> newList, bool expectedIsSame)
    {
        var result = oldList.GetDifferences(newList, (x, y) => x == y, (x, y) => x.Id == y.Id);

        Assert.Equal(expectedIsSame, result.AreEqual);
    }
    public static IEnumerable<object[]> Create3() =>
        new List<object[]>
        {
            new object[] { new List<Dummy> { new(1,"pippo") }, new List<Dummy> { new(1, "pippo") }, true },
            new object[] { new List<Dummy> { new(1,"pippoa") }, new List<Dummy> { new(1, "pippo") }, false },
            new object[] { new List<Dummy> { new(1,"pippo") }, new List<Dummy> { new(2, "pippo") }, false },
        };

    public record Dummy(int Id, string Name);


    [Theory]
    [MemberData(nameof(Create2))]
    public void CompareValueType_EqualList_AreEqual(List<int> oldList, List<int> newList, bool expectedIsSame)
    {
        var result = oldList.GetDifferences(newList, (x, y) => x == y, (x, y) => x == y);

        Assert.Equal(expectedIsSame, result.AreEqual);
    }
    public static IEnumerable<object[]> Create2() =>
        new List<object[]>
        {
            new object[] { new List<int> { 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5 }, true },
            new object[] { new List<int> { 1, 2, 3, 4, 5 }, new List<int> { 4, 5 }, false },
            new object[] { new List<int>( ), new List<int>( ), true },
            new object[] { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3, 4, 5 }, false },
            new object[] { new List<int> { 1, 2, 3 }, new List<int> { 4, 5 }, false }
        };
}