using Solutions.Day13;
using Xunit.Abstractions;

namespace Solutions.UnitTests;

public class Day13Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day13Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    [Fact]
    public void NewGridMaths()
    {
        var gridSize = new XyCoord(11, 7);
        var grid = new XyGrid(11, 7);
        var basicRobot = new XYRobot(new XyCoord(2, 4), new XyCoord(2, -3));
        grid[basicRobot.InitialPosition].Add(basicRobot);
        _testOutputHelper.WriteLine(grid.Display());
        
        var math = new XyRobotMath(gridSize);
        
        Assert.Equal(new XyCoord(4, 1), grid.Move(basicRobot, math));
        
        _testOutputHelper.WriteLine("");
        _testOutputHelper.WriteLine(grid.Display());
        
        Assert.Equal(new XyCoord(6, 5), grid.Move(basicRobot, math));
        _testOutputHelper.WriteLine("");
        _testOutputHelper.WriteLine(grid.Display());
        
        Assert.Equal(new XyCoord(8, 2), grid.Move(basicRobot, math));
        _testOutputHelper.WriteLine("");
        _testOutputHelper.WriteLine(grid.Display());
        
        Assert.Equal(new XyCoord(10, 6), grid.Move(basicRobot, math));
        _testOutputHelper.WriteLine("");
        _testOutputHelper.WriteLine(grid.Display());
        
        Assert.Equal(new XyCoord(1, 3), grid.Move(basicRobot, math));
        _testOutputHelper.WriteLine("");
        _testOutputHelper.WriteLine(grid.Display());
        
    }
}