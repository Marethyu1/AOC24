using Solutions.Day5;

namespace Solutions.UnitTests;

public class Day5Tests
{
    [Fact]
    public void IsBefore()
    {
        var page = new PageOrdering(47, 53);
        var pageOrderings = new PageOrderings([page]);

        var shouldBeAfter = pageOrderings.IsAfter(page.Y, page.X);
        Assert.True(shouldBeAfter);
    }

    [Fact]
    public void IsBeforeMoreComplicated()
    {
        var o = new PageOrderings([
            new PageOrdering(75, 47),
            new PageOrdering(75, 61),
            new PageOrdering(75, 53),
            new PageOrdering(75, 29)
        ]);

        var items = new List<int>()
        {
            47, 61, 53, 29
        };
        var isFirst = o.IsFirst(75, items);
        Assert.True(isFirst);
    }

    [Fact]
    public void IsntAfter()
    {
        var o = new PageOrderings([
            new PageOrdering(97, 75),
        ]);

        var items = new List<int>()
        {
            97
        };
        var isFirst = o.IsFirst(75, items);
        Assert.False(isFirst);
    }
}