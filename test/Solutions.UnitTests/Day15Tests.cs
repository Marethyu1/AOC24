using Helpers.Grid;
using Helpers.XyGrid;
using Solutions.Day15;
using Xunit.Abstractions;

namespace Solutions.UnitTests;

public class Day15Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day15Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CanFindStart()
    {
        var grid = new XyGrid<Tile>(new Tile[][]
        {
                "##########".Select(Tile.ToTile).ToArray(),
                "#..O..O.O#".Select(Tile.ToTile).ToArray(),
                "#......O.#".Select(Tile.ToTile).ToArray(),
                "#.OO..O.O#".Select(Tile.ToTile).ToArray(),
                "#..O@..O.#".Select(Tile.ToTile).ToArray(),
                "#.OO.O.OO#".Select(Tile.ToTile).ToArray(),
                "#....O...#".Select(Tile.ToTile).ToArray(),
                "##########".Select(Tile.ToTile).ToArray(),
        });
        var wareHouse = new WareHouse(grid, new Queue<Direction>());
        Assert.Equal(new XyCoord(4, 4), wareHouse.Start);
    }
    
    [Fact]
    public void CanMoveInEveryDirection()
    {
        var grid = new XyGrid<Tile>(new Tile[][]
        {
            "##########".Select(Tile.ToTile).ToArray(),
            "#..O..O.O#".Select(Tile.ToTile).ToArray(),
            "#......O.#".Select(Tile.ToTile).ToArray(),
            "#.OO..O.O#".Select(Tile.ToTile).ToArray(),
            "#..O@..O.#".Select(Tile.ToTile).ToArray(),
            "#.OO.O.OO#".Select(Tile.ToTile).ToArray(),
            "#....O...#".Select(Tile.ToTile).ToArray(),
            "##########".Select(Tile.ToTile).ToArray(),
        });
        var q = new Queue<Direction>();
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Down);
        q.Enqueue(Direction.Left);
        var wareHouse = new WareHouse(grid, q);
        Assert.Equal(new XyCoord(4, 4), wareHouse.Start);
        
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(4, 3), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(5, 3), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(5, 4), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(4, 4), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
    }
    
    [Fact]
    public void WontWalkWall()
    {
        var grid = new XyGrid<Tile>(new Tile[][]
        {
            "##########".Select(Tile.ToTile).ToArray(),
            "#..O..O.O#".Select(Tile.ToTile).ToArray(),
            "#...@..O.#".Select(Tile.ToTile).ToArray(),
            "#.OO..O.O#".Select(Tile.ToTile).ToArray(),
            "#..O...O.#".Select(Tile.ToTile).ToArray(),
            "#.OO.O.OO#".Select(Tile.ToTile).ToArray(),
            "#....O...#".Select(Tile.ToTile).ToArray(),
            "##########".Select(Tile.ToTile).ToArray(),
        });
        var q = new Queue<Direction>();
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Right);
        var wareHouse = new WareHouse(grid, q);
        Assert.Equal(new XyCoord(4, 2), wareHouse.Start);
        
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(4, 1), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(4, 1), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(4, 1), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(5, 1), wareHouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
    }

    [Fact]
    public void CanPushBox()
    {
        var grid = new XyGrid<Tile>([
            "########".Select(Tile.ToTile).ToArray(),
            "#..O.O.#".Select(Tile.ToTile).ToArray(),
            "##@.O..#".Select(Tile.ToTile).ToArray(),
            "#...O..#".Select(Tile.ToTile).ToArray(),
            "#.#.O..#".Select(Tile.ToTile).ToArray(),
            "#...O..#".Select(Tile.ToTile).ToArray(),
            "#......#".Select(Tile.ToTile).ToArray(),
            "########".Select(Tile.ToTile).ToArray()
        ]);
        
        var q = new Queue<Direction>();
        q.Enqueue(Direction.Left);
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Down);
        
        var wareHouse = new WareHouse(grid, q);
        Assert.Equal(new XyCoord(2, 2), wareHouse.Start);
        _testOutputHelper.WriteLine(grid.Display());
        
        Assert.Equal(new XyCoord(2, 2), wareHouse.MoveRobot()); //Left
        _testOutputHelper.WriteLine(grid.Display()); 
        Assert.Equal(new XyCoord(2, 1), wareHouse.MoveRobot()); //Up
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(2, 1), wareHouse.MoveRobot()); //Up
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(new XyCoord(3, 1), wareHouse.MoveRobot()); // Right
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.Box, grid[new XyCoord(4, 1)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(5, 1)].Value);
        
        
        
        Assert.Equal(new XyCoord(4, 1), wareHouse.MoveRobot()); // Right
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.Box, grid[new XyCoord(5, 1)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(6, 1)].Value);
        
        Assert.Equal(new XyCoord(4, 1), wareHouse.MoveRobot()); // Right
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.Box, grid[new XyCoord(5, 1)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(6, 1)].Value);
        
        Assert.Equal(new XyCoord(4, 2), wareHouse.MoveRobot()); // Down
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.Box, grid[new XyCoord(4, 3)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(4, 4)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(4, 5)].Value);
        Assert.Equal(TileValue.Box, grid[new XyCoord(4, 6)].Value);
    }

    [Fact]
    public void CanCreateBigGrid()
    {
        var tiles = new[]
        {
            "########".Select(Tile.ToTile).ToArray(),
            "#..O.O.#".Select(Tile.ToTile).ToArray(),
            "##@.O..#".Select(Tile.ToTile).ToArray(),
            "#...O..#".Select(Tile.ToTile).ToArray(),
            "#.#.O..#".Select(Tile.ToTile).ToArray(),
            "#...O..#".Select(Tile.ToTile).ToArray(),
            "#......#".Select(Tile.ToTile).ToArray(),
            "########".Select(Tile.ToTile).ToArray()
        };

        var wideTiles = tiles.Select(row => row.SelectMany(Tile.ToWideTile).ToArray()).ToArray();
        var grid = new XyGrid<Tile>(wideTiles);
        _testOutputHelper.WriteLine(grid.Display());
        var q = new Queue<Direction>();
        q.Enqueue(Direction.Up);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Right);
        q.Enqueue(Direction.Down);
        var warehouse = new BigWarehouse(grid, q);

        warehouse.MoveRobot();
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.LhBox, grid[new XyCoord(7, 1)].Value);
        Assert.Equal(TileValue.RhBox, grid[new XyCoord(8, 1)].Value);
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.LhBox, grid[new XyCoord(8, 1)].Value);
        Assert.Equal(TileValue.RhBox, grid[new XyCoord(9, 1)].Value);
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.LhBox, grid[new XyCoord(9, 1)].Value);
        Assert.Equal(TileValue.RhBox, grid[new XyCoord(10, 1)].Value);
        Assert.Equal(TileValue.LhBox, grid[new XyCoord(11, 1)].Value);
        Assert.Equal(TileValue.RhBox, grid[new XyCoord(12, 1)].Value);
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        Assert.Equal(TileValue.Space, grid[new XyCoord(9, 2)].Value);
        Assert.Equal(TileValue.LhBox, grid[new XyCoord(8, 3)].Value);
        Assert.Equal(TileValue.RhBox, grid[new XyCoord(9, 3)].Value);

        var list = grid.EnumerateCoords().Select(x => grid[x]).ToList();
    }

    [Fact]
    public void CanMoveBoxesUp()
    {
        var tiles = new[]
        {
            "#######".Select(Tile.ToTile).ToArray(),
            "#...#.#".Select(Tile.ToTile).ToArray(),
            "#.....#".Select(Tile.ToTile).ToArray(),
            "#..OO@#".Select(Tile.ToTile).ToArray(),
            "#..O..#".Select(Tile.ToTile).ToArray(),
            "#.....#".Select(Tile.ToTile).ToArray(),
            "#######".Select(Tile.ToTile).ToArray(),
        };
        var wideTiles = tiles.Select(row => row.SelectMany(Tile.ToWideTile).ToArray()).ToArray();
        var grid = new XyGrid<Tile>(wideTiles);
        
        _testOutputHelper.WriteLine(grid.Display());
        var q = new Queue<Direction>();
        q.Enqueue(Direction.Left);
        q.Enqueue(Direction.Down);
        q.Enqueue(Direction.Left);
        q.Enqueue(Direction.Down);
        q.Enqueue(Direction.Left);
        q.Enqueue(Direction.Up);
        var warehouse = new BigWarehouse(grid, q);
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        
        warehouse.MoveRobot();
        _testOutputHelper.WriteLine(grid.Display());
        
        
        Assert.Equal(new XyCoord(7, 4), warehouse.MoveRobot());
        _testOutputHelper.WriteLine(grid.Display());
         

        
    }
}