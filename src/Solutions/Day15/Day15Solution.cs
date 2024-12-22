using Helpers.Grid;
using Helpers.Solution;
using Helpers.XyGrid;

namespace Solutions.Day15;

public class Day15Solution(XyGrid<Tile> grid, Queue<Direction> commands, XyGrid<Tile> xyGrid) : ISolution
{
    public long SolvePart1()
    {
        var wareHouse = new WareHouse(grid, commands);
        Console.WriteLine(grid.Display());
        
        var n = commands.Count;
        for (var i = 0; i < n; i++)
        {
            wareHouse.MoveRobot();
        }
        
        Console.WriteLine(grid.Display());

        
        
        var sumo  = grid.EnumerateCoords()
            .Where(coord => grid[coord].Value == TileValue.Box)
            .Select(coord => 100 * coord.TilesFromTopWall + coord.TilesFromLeftWall)
            .Sum();
        return sumo;
    }

    public long SolvePart2()
    {
        Console.WriteLine(xyGrid.Display());
        
        var wareHouse = new BigWarehouse(xyGrid, commands);
        var n = commands.Count;
        for (var i = 0; i < n; i++)
        {
            wareHouse.MoveRobot();
        }
        xyGrid.Display();
        
        
        var sumo  = xyGrid.EnumerateCoords()
            .Where(coord => xyGrid[coord].Value == TileValue.LhBox)
            .Select(coord => 100 * coord.TilesFromTopWall + coord.TilesFromLeftWall)
            .Sum();
        return sumo;
    }

    public static Day15Solution LoadSolution(string filePath)
    {
        var commands = new HashSet<char>() { '>', '^', '<', 'v' };

        var gridInput = new List<Tile[]>();
        var directions = new Queue<Direction>();
        foreach (var line in File.ReadAllLines(filePath))
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.StartsWith('#'))
            {
                var row = trimmedLine.Select(c => new Tile((TileValue) c)).ToArray();
                gridInput.Add(row);
            }

            if (trimmedLine.Length > 0 && commands.Contains(trimmedLine[0]))
            {
                var inputDirections = trimmedLine.Select(Command.ToDirection).ToArray();
                foreach (var d in inputDirections)
                {
                    directions.Enqueue(d);
                }
            }
        }
        
        var v2GridInput = gridInput.Select(row => row.SelectMany(Tile.ToWideTile).ToArray()).ToArray();
        var v2Grid = new XyGrid<Tile>(v2GridInput);
        var grid = new XyGrid<Tile>(gridInput.ToArray());
        return new Day15Solution(grid, directions, v2Grid);
    }
    
    
}