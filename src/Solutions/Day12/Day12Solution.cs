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
    
    private static readonly Direction[] PlotDirections =
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
            
            var plot = FindNeighbours(coord);
            var perimeter = 0;
            var searchTerm = grid[coord];
            foreach (var currentPlotCoord in plot)
            {
                foreach (var adjacentCoord in currentPlotCoord.AdjacentCoords(PlotDirections))
                {
                    if (!grid.InBounds(adjacentCoord))
                    {
                        perimeter++;
                    } else if (grid[adjacentCoord] != searchTerm)
                    {
                        perimeter++;
                    }
                }
            }
            Console.WriteLine($"Visiting {grid[coord]}-{plot.Count}  {perimeter}");
            fullTotal += plot.Count * perimeter;
            
            superSet.UnionWith(plot);
        }
        return fullTotal;
    }

    private HashSet<Coord> FindNeighbours(Coord startCoord)
    {
        var visited = new HashSet<Coord>();
        var startValue = grid[startCoord];
        var stack = new Stack<Coord>();
        stack.Push(startCoord);

        while (stack.Count > 0)
        {
            var currentCoord = stack.Pop();
            if (!visited.Add(currentCoord))
            {
                continue;
            }
            foreach (var coordToVisit in currentCoord.AdjacentCoords(AdjacentDirections))
            {
                if (!grid.InBounds(coordToVisit))
                {
                    continue;
                }

                if (grid[coordToVisit] != startValue)
                {
                    continue;
                }
                stack.Push(coordToVisit);
            }
        }

        return visited;

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