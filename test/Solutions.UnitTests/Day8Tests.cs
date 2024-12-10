using Helpers.Grid;
using Solutions.Day7;
using Solutions.Day8;

namespace Solutions.UnitTests;

public class Day8Tests
{
    [Fact]
    public void CanGeneratePairings()
    {
        int[] input = [1, 2, 3, 4];
        var pairings = PermutationGenerator<int>.GetPairings(input).ToList();
        Assert.Equal(6, pairings.Count);
        Assert.Contains(new Tuple<int, int>(1, 2), pairings);
        Assert.Contains(new Tuple<int, int>(1, 3), pairings);
        Assert.Contains(new Tuple<int, int>(1, 4), pairings);
        Assert.Contains(new Tuple<int, int>(2, 3), pairings);
        Assert.Contains(new Tuple<int, int>(2, 4), pairings);
        Assert.Contains(new Tuple<int, int>(3, 4), pairings);
    }

    [Fact]
    public void CanDoMaths()
    {
        var a = new Coord(2, 2);
        var b = new Coord(4, 4);
        var results= CoordMaths.Delta(a, b).ToArray();
        Assert.Contains(new Coord(0, 0), results);
        Assert.Contains(new Coord(6, 6), results);
    }
    
    [Fact]
    public void CanDoHorizontalMaths()
    {
        var a = new Coord(5, 2);
        var b = new Coord(5, 4);
        var results= CoordMaths.Delta(a, b).ToArray();
        Assert.Contains(new Coord(5, 0), results);
        Assert.Contains(new Coord(5, 6), results);
    }
    
    [Fact]
    public void CanDoVerticalMaths()
    {
        var a = new Coord(3, 1);
        var b = new Coord(5, 1);
        var results= CoordMaths.Delta(a, b).ToArray();
        Assert.Contains(new Coord(1, 1), results);
        Assert.Contains(new Coord(7, 1), results);
    }

    [Fact]
    public void CanDoDiagonalMaths()
    {
        var a = new Coord(4, 3);
        var b = new Coord(2, 5);
        var results= CoordMaths.Delta(a, b).ToArray();
        Assert.Contains(new Coord(0, 7), results);
        Assert.Contains(new Coord(6, 1), results);
    }
}