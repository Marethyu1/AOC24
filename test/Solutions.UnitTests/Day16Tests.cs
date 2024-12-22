using Helpers.XyGrid;
using Solutions.Day16;
using Xunit.Abstractions;

namespace Solutions.UnitTests;

public class Day16Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day16Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CanWalkInStraightLine()
    {
        var rawInput = new List<string>
        {
            "#######",
            "#S...E#",
            "#######",
        };
        var solver = ToMazeSolver(rawInput);
        _testOutputHelper.WriteLine(solver.Display());
        
        Assert.Equal(4, solver.Solve());
    }
    
    [Fact]
    public void CanWalkInStraightLineForPart2()
    {
        var rawInput = new List<string>
        {
            "#######",
            "#S...E#",
            "#######",
        };
        var solver = ToMazeSolver(rawInput);
        _testOutputHelper.WriteLine(solver.Display());
        
        Assert.Equal(5, solver.SolvePart2());
    }

    private MazeSolver ToMazeSolver(List<string> lines)
    {
        return new MazeSolver(ToXyGrid(lines), _testOutputHelper.WriteLine);
    }

    private static XyGrid<MazeTile> ToXyGrid(List<string> lines)
    {
        var tiles = lines.Select(l => l.Select(MazeTile.ToTile).ToArray()).ToArray();
        return new XyGrid<MazeTile>(tiles);
    }
}