using Helpers.Grid;
using Helpers.Solution;

namespace Solutions.Day12;

public class Day12Solution(Grid<char> grid) : ISolution
{
    private static readonly Direction[] AdjacentDirections =
    [
        Direction.Up,
        Direction.NorthEast,
        Direction.Right,
        Direction.SouthEast,
        Direction.Down,
        Direction.SouthWest,
        Direction.Left,
        Direction.NorthWest,
    ];
    
    public static readonly Direction[] PlotDirections =
    [
        Direction.Up,
        Direction.Right,
        Direction.Down,
        Direction.Left,
    ];
    
    public long SolvePart1()
    {
        var superSet = new HashSet<Coord>();
        long fullTotal = 0;
        foreach (var coord in grid.EnumerateCoords())
        {
            if (superSet.Contains(coord))
            {
                continue;
            }
            
            var plot = Plotter.FindNeighbours(coord, grid, PlotDirections);
            var searchTerm = grid[coord];
            var perimeter = Plotter.DeterminePerimeter(plot, grid, searchTerm, PlotDirections);
            
            Console.WriteLine($"Visiting {grid[coord]}-{plot.Count}  {perimeter}");
            fullTotal += plot.Count * perimeter;
            
            superSet.UnionWith(plot);
        }
        return fullTotal;
    }

    public long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static Day12Solution LoadSolution(string filePath)
    {
        var lines = File.ReadAllLines(filePath)
            .Select(line => line.ToCharArray())
            .ToArray();
        var grid = new Grid<char>(lines);
        return new Day12Solution(grid);
    }
}