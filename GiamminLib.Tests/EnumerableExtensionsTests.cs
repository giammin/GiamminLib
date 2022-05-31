using System.Collections.Generic;
using NUnit.Framework;
using GiamminLib.ExtensionMethods;

namespace GiamminLib.Tests;

[TestFixture]
public class EnumerableExtensionsTests
{

    //liste value type
    //liste con classi uguali
    //liste con classi differenti

    //liste uguali
    //liste differenti
    //liste vuote
    [Test]
    [TestCaseSource(nameof(Create3))]
    public void CompareRefType_VariousLists(List<Dummy> oldList, List<Dummy> newList, bool expectedIsSame)
    {
        var result = oldList.Compare(newList, (x, y) => x == y, (x, y) => x.Id == y.Id);

        Assert.AreEqual(expectedIsSame, result.IsSame);
    }
    static IEnumerable<object[]> Create3() =>
        new List<object[]>
        {
            new object[] { new List<Dummy> { new(1,"pippo") }, new List<Dummy> { new(1, "pippo") }, true },
            new object[] { new List<Dummy> { new(1,"pippoa") }, new List<Dummy> { new(1, "pippo") }, false },
            new object[] { new List<Dummy> { new(1,"pippo") }, new List<Dummy> { new(2, "pippo") }, false },
        };

    public record Dummy(int Id, string Name);


    [Test]
    [TestCaseSource(nameof(Create2))]
    public void CompareValueType_EqualList_AreEqual(List<int> oldList, List<int> newList, bool expectedIsSame)
    {
        var result = oldList.Compare(newList, (x, y) => x == y, (x, y) => x == y);

        Assert.AreEqual(expectedIsSame, result.IsSame);
    }
    static IEnumerable<object[]> Create2() =>
        new List<object[]>
        {
            new object[] { new List<int> { 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5 }, true },
            new object[] { new List<int> { 1, 2, 3, 4, 5 }, new List<int> { 4, 5 }, false },
            new object[] { new List<int>( ), new List<int>( ), true },
            new object[] { new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3, 4, 5 }, false },
            new object[] { new List<int> { 1, 2, 3 }, new List<int> { 4, 5 }, false }
        };

    [Test]
    [TestCaseSource(nameof(Create1))]
    public void CompareResult_IsSameAndTotalDifference_Works(Dictionary<object, object> equals, Dictionary<object, object> different, List<object> added, List<object> removed, bool expectedIsSameResult)
    {
        var result = new EnumerableExtensions.CompareResult<object, object>
            { Equal = equals, Different = different, Added = added, Removed = removed };


        Assert.AreEqual(different.Count + added.Count + removed.Count, result.TotalDifferences);
        Assert.AreEqual(added.Count, result.Added.Count);
        Assert.AreEqual(removed.Count, result.Removed.Count);
        Assert.AreEqual(different.Count, result.Different.Count);
        Assert.AreEqual(equals.Count, result.Equal.Count);

        Assert.AreEqual(expectedIsSameResult, result.IsSame);
        Assert.AreEqual(result.TotalDifferences == 0, result.IsSame);
        Assert.AreEqual(result.TotalDifferences != 0, !result.IsSame);
    }

    static IEnumerable<object[]> Create1()
    {
        var rtn = new List<object[]>();
        var equalEmpty = new Dictionary<object, object>();
        var equal = new Dictionary<object, object> { { TestContext.CurrentContext.Random.GetString(), TestContext.CurrentContext.Random.GetString() } };
        var differentEmpty = new Dictionary<object, object>();
        var different = new Dictionary<object, object> { { TestContext.CurrentContext.Random.GetString(), TestContext.CurrentContext.Random.GetString() } };
        var addedEmpty = new List<object>();
        var added = new List<object> { TestContext.CurrentContext.Random.GetString() };
        var removedEmpty = new List<object>();
        var removed = new List<object> { TestContext.CurrentContext.Random.GetString() };

        rtn.Add(new object[] { equalEmpty, differentEmpty, addedEmpty, removedEmpty, true });
        rtn.Add(new object[] { equal, differentEmpty, addedEmpty, removedEmpty, true });
        rtn.Add(new object[] { equalEmpty, different, addedEmpty, removedEmpty, false });
        rtn.Add(new object[] { equalEmpty, differentEmpty, added, removedEmpty, false });
        rtn.Add(new object[] { equalEmpty, differentEmpty, addedEmpty, removed, false });
        rtn.Add(new object[] { equal, different, added, removed, false });

        return rtn;

    }
}