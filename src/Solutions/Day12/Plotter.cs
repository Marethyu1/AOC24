using Helpers.Grid;

namespace Solutions.Day12;

public static class Plotter
{
    public static HashSet<Coord> FindNeighbours(Coord startCoord, Grid<char> grid, Direction[] directions)
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
            foreach (var coordToVisit in currentCoord.AdjacentCoords(directions))
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
    
    public static int DeterminePerimeter(HashSet<Coord> plot, Grid<char> grid,  char searchTerm, Direction[] directions)
    {
        var perimeter = 0;
        foreach (var currentPlotCoord in plot)
        {
            foreach (var adjacentCoord in currentPlotCoord.AdjacentCoords(directions))
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

        return perimeter;
    }

    public static Coord FindNorthWest(HashSet<Coord> plot)
    {
        var plots = plot.ToArray();
        var minRow = plots.Min(coord => coord.R);
        var minColumn = plots
            .Where(coord => coord.R == minRow)
            .Min(coord => coord.C);
        return new Coord(minRow, minColumn);
    }
}