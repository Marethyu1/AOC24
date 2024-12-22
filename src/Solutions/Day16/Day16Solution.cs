using Helpers.Solution;
using Helpers.XyGrid;

namespace Solutions.Day16;

public class Day16Solution(XyGrid<MazeTile> xyGrid) : ISolution
{
    public long SolvePart1()
    {
        var solver = new MazeSolver(xyGrid, (string x) => { });
        
        return solver.Solve();
    }

    public long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static Day16Solution LoadSolution(string filePath)
    {
        var gridInput = File.ReadAllLines(filePath)
            .Select(line => line.Select(MazeTile.ToTile).ToArray())
            .ToArray();
        
        var grid = new XyGrid<MazeTile>(gridInput);
        return new Day16Solution(grid);
    }
}