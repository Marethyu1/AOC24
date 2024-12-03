using Solutions.Day3;

namespace Solutions.UnitTests;

public class Day3Tests
{
    [Fact]
    public void DoesSubStringOverFlow()
    {
        var doesMatch = Matcher.DoesSubStringMatch("apple", 2, "applesauce");
        Assert.False(doesMatch);
    }

    [Fact]
    public void DoesSubstringOffBy1()
    {
        var doesMatch = Matcher.DoesSubStringMatch("apple", 2, "apples");
        Assert.False(doesMatch);
    }

    [Fact]
    public void CanActuallyMatchTho()
    {
        var doesMatch = Matcher.DoesSubStringMatch("mul(", 0, "mul(");
        Assert.True(doesMatch);
    }

    [Fact]
    public void CanCheckSequenceOfInts()
    {
        var isSequenceOfInts = Matcher.MatchSequenceOfInts("12345,", 0, ',');
        Assert.True(isSequenceOfInts.IsSequence);
        Assert.Equal(12345, isSequenceOfInts.AsInt);
    }

    [Fact]
    public void CanCheckNotSequenceOfInts()
    {
        var isSequenceOfInts = Matcher.MatchSequenceOfInts("12A,", 0, ',');
        Assert.False(isSequenceOfInts.IsSequence);
    }
    
    [Fact]
    public void CanCheckEmptySequenceOfInts()
    {
        var isSequenceOfInts = Matcher.MatchSequenceOfInts(" ,", 0, ',');
        Assert.False(isSequenceOfInts.IsSequence);
    }
}