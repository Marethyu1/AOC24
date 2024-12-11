using Helpers.Grid;
using Helpers.Solution;

namespace Solutions.Day10;

public class Day10Solution(Grid<int> grid) : ISolution
{
    private const int StartValue = 0;
    private const int Peak = 9;

    private static readonly Direction[] AdjacentDirections =
    [
        Direction.Up, Direction.Down, Direction.Left, Direction.Right
    ];

    public long SolvePart1()
    {
        long count = 0;
        foreach (var coord in grid.EnumerateCoords()
                     .Where(c => grid[c] == StartValue))
        {
            var res = Search(coord);
            count += res;
        }

        return count;
    }

    private long Search(Coord coord)
    {
        var start = grid[coord];
        if (start != StartValue) return 0;

        var peaks = new HashSet<Coord>() {};
        var visited = new HashSet<Coord> {  };
        var stack = new Stack<Coord>();
        stack.Push(coord);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (grid[current] == Peak)
            {
                peaks.Add(current);
            }

            if (!visited.Add(current))
            {
                continue;
            }


            foreach (var coordToVisit in current.AdjacentCoords(AdjacentDirections))
            {
                if (!grid.InBounds(coordToVisit))
                {
                    continue;
                }

                if (grid[coordToVisit] != grid[current] + 1)
                {
                    continue;
                }
                stack.Push(coordToVisit);
            }
        }



        return peaks.Count;
    }

    public long SolvePart2()
    {
        long count = 0;
        foreach (var coord in grid.EnumerateCoords()
                     .Where(c => grid[c] == StartValue))
        {
            var res = SearchV2(coord);
            count += res;
        }

        return count;
    }
    
    private long SearchV2(Coord coord)
    {
        var start = grid[coord];
        if (start != StartValue) return 0;

        var peaks = 0;
        var stack = new Stack<Coord>();
        stack.Push(coord);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (grid[current] == Peak)
            {
                peaks++;
            }

            foreach (var coordToVisit in current.AdjacentCoords(AdjacentDirections))
            {
                if (!grid.InBounds(coordToVisit))
                {
                    continue;
                }

                if (grid[coordToVisit] != grid[current] + 1)
                {
                    continue;
                }
                stack.Push(coordToVisit);
            }
        }
        return peaks;
    }
    
    


    public static Day10Solution LoadSolution(string basicInput)
    {
        var input = File.ReadLines(basicInput)
            .Select(x => x.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
        var grid = new Grid<int>(input);
        return new Day10Solution(grid);
    }
}