using Helpers.Grid;
using Solutions.Day4;

namespace Solutions.UnitTests;

public class GridTests
{

    private static readonly Coord Center = new(1, 1);
    
    public GridTests()
    {
        // (0,0), (0,1), (0,2)
        // (1,0), (1,1), (1,2)
        // (2,0), (2,1), (2,2)
    }
    
    [Fact]
    public void CanGoNorth()
    {
        var nextCoord = Center.Next(Direction.Up);
        var expectedCord = new Coord(0, 1); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoNorthEast()
    {
        var nextCoord = Center.Next(Direction.NorthEast);
        var expectedCord = new Coord(0, 2); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoEast()
    {
        var nextCoord = Center.Next(Direction.Right);
        var expectedCord = new Coord(1, 2); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoSouthEast()
    {
        var nextCoord = Center.Next(Direction.SouthEast);
        var expectedCord = new Coord(2, 2); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoSouth()
    {
        var nextCoord = Center.Next(Direction.Down);
        var expectedCord = new Coord(2, 1); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoSouthWest()
    {
        var nextCoord = Center.Next(Direction.SouthWest);
        var expectedCord = new Coord(2, 0); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoWest()
    {
        var nextCoord = Center.Next(Direction.Left);
        var expectedCord = new Coord(1, 0); 
        Assert.Equal(nextCoord, expectedCord);
    }
    
    [Fact]
    public void CanGoNorthWest()
    {
        var nextCoord = Center.Next(Direction.NorthWest);
        var expectedCord = new Coord(0, 0); 
        Assert.Equal(nextCoord, expectedCord);
    }

    [Fact]
    public void Gets8Directions()
    {
        Assert.Equal(8, Enum.GetValuesAsUnderlyingType<Direction>().Length);
    }

    [Fact]
    public void IsBasicSequence()
    {
        char[] target = ['X', 'M', 'A'];
        var input = new[]
        {
            new[] { 'X', 'M', 'A', 'S', 'O'},
        };
        
        Assert.Equal(target[2], input[0][2]);
        var grid = new Grid<char>(input);
        var isValid = Day4Solution.IsValidSequence(grid, target, Direction.Right, new Coord(0, 0));
        Assert.True(isValid);
    }
    
    [Fact]
    public void HandlesOutOfBounds()
    {
        char[] target = ['S', 'O', 'Y'];
        var input = new[]
        {
            new[] { 'S', 'O'},
        };
        
        var grid = new Grid<char>(input);
        var isValid = Day4Solution.IsValidSequence(grid, target, Direction.Right, new Coord(0, 0));
        Assert.False(isValid);
    }

    [Fact]
    public void IsSequence()
    {
        char[] target = ['X', 'M', 'A', 'S'];
        var input = new[]
        {
            new char[] { 'X', 'M', 'A', 'S', 'O', 'X' },
        };
        var grid = new Grid<char>(input);
        var isValid = Day4Solution.IsValidSequence(grid, target, Direction.Right, new Coord(0, 0));
        
        Assert.True(isValid);
        var startCoord = new Coord(0, 0);
        Assert.True(grid[startCoord] == target[0]);
        startCoord = startCoord.Next(Direction.Right);
        Assert.True(grid[startCoord] == target[1]);
        
        startCoord = startCoord.Next(Direction.Right);
        Assert.True(grid[startCoord] == target[2]);
        
        startCoord = startCoord.Next(Direction.Right);
        Assert.True(grid[startCoord] == target[3]);
        
        startCoord = startCoord.Next(Direction.Right);
        Assert.True(grid[startCoord] == 'O');
    }
}