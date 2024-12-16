using Helpers.Grid;
using Solutions.Day12;

namespace Solutions.UnitTests;

public class Day12Tests
{
    
    [Fact]
    public void CanPlot()
    {
        var input = new[]
        {
            "AAABBB",
            "AAABBB"
        };

        var grid = new Grid<char>(ToArray(input));

        var plot = Plotter.FindNeighbours(new Coord(0, 0), grid, Day12Solution.PlotDirections);
        Assert.Contains(new Coord(0, 0), plot);
        Assert.Contains(new Coord(0, 1), plot);
        Assert.Contains(new Coord(0, 2), plot);
        Assert.Contains(new Coord(1, 0), plot);
        Assert.Contains(new Coord(1, 1), plot);
        Assert.Contains(new Coord(1, 2), plot);
        var perimeter = Plotter.DeterminePerimeter(plot, grid, grid[plot.First()], Day12Solution.PlotDirections);
        Assert.Equal(10, perimeter);
    }
    
    [Fact]
    public void CanPlot2()
    {
        var input = new[]
        {
            "AAAAAA",
            "A----A",
            "AAAAAA",
        };// 123456

        var grid = new Grid<char>(ToArray(input));

        var plot = Plotter.FindNeighbours(new Coord(0, 0), grid, Day12Solution.PlotDirections);
        var perimeter = Plotter.DeterminePerimeter(plot, grid, grid[plot.First()], Day12Solution.PlotDirections);
        Assert.Equal(28, perimeter);
    }
    
    [Fact]
    public void CanPlot5()
    {
        var input = new[]
        {
            "OOOOO",
            "OXOXO",
            "OOOOO",
            "OXOXO",
            "OOOOO",
        };// 123456

        var grid = new Grid<char>(ToArray(input));

        var plot = Plotter.FindNeighbours(new Coord(0, 0), grid, Day12Solution.PlotDirections);
        var perimeter = Plotter.DeterminePerimeter(plot, grid, grid[plot.First()], Day12Solution.PlotDirections);
        Assert.Equal(36, perimeter);
        var plot2 = Plotter.FindNeighbours(new Coord(1, 1), grid, Day12Solution.PlotDirections);
        var perimeter2 = Plotter.DeterminePerimeter(plot2, grid, grid[new Coord(1, 1)], Day12Solution.PlotDirections);
        Assert.Equal(4, perimeter2);
    }

    [Fact]
    public void DoPart5()
    {
        var input = new[]
        {
            "OOOOO",
            "O0OXO",
            "OOOOO",
            "OXOXO",
            "OOOOO",
        };
        
        var grid = new Grid<char>(ToArray(input));
        var plot = Plotter.FindNeighbours(new Coord(0, 0), grid, Day12Solution.PlotDirections);
        var nW = Plotter.FindNorthWest(plot);
        Assert.Equal(new Coord(0, 0), nW);
    }

    private static char[][] ToArray(string[] s)
    {
        return s.Select(c => c.ToCharArray()).ToArray();
    }
}