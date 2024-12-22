using Helpers.Grid;
using Helpers.XyGrid;

namespace Solutions.Day15;

public class WareHouse
{
    private readonly XyGrid<Tile> _grid;
    private readonly Queue<Direction> _commands;
    private static TileValue _startValue = TileValue.Robot;
    public int MovesLeft => _commands.Count;

    private XyCoord _robotLocation;
    
    public XyCoord Start { get; set; }

    public WareHouse(XyGrid<Tile> grid, Queue<Direction> commands)
    {
        _grid = grid;
        _commands = commands;
        Start = DetermineStartCoord();
        _robotLocation = Start;
    }

    private XyCoord DetermineStartCoord()
    {
        return _grid.EnumerateCoords().First(x => _grid[x].Value == _startValue);
    }

    public XyCoord MoveRobot()
    {
        var direction = _commands.Dequeue();
        
        var nextLocation = _robotLocation.Next(direction);
        if (!IsOccupied(nextLocation))
        {
            Swap(_robotLocation, nextLocation);
            _robotLocation = nextLocation;
        }

        if (IsLookingAtBox(nextLocation))
        {
            if (!DoesSequenceResultInSpace(nextLocation, direction))
            {
                return _robotLocation;
            }
            
            var swaps = GetSwaps(nextLocation, direction);
            while (swaps.Count > 0)
            {
                var currentSwap = swaps.Pop();
                Swap(currentSwap.Item1, currentSwap.Item2);
            }
            Swap(_robotLocation, nextLocation);
            _robotLocation = nextLocation;
        }
        
        return _robotLocation;
    }

    private Stack<Tuple<XyCoord, XyCoord>> GetSwaps(XyCoord currentLocation, Direction direction)
    {
        var stack = new Stack<Tuple<XyCoord, XyCoord>>();
        var nextLocation = currentLocation.Next(direction);
        stack.Push(new Tuple<XyCoord, XyCoord>(currentLocation, nextLocation));
        while (_grid[nextLocation].Value == TileValue.Box)
        {
            currentLocation = nextLocation;
            nextLocation = nextLocation.Next(direction);
            stack.Push(new Tuple<XyCoord, XyCoord>(currentLocation, nextLocation));
        }

        return stack;
    }

    private bool DoesSequenceResultInSpace(XyCoord location, Direction direction)
    {
        while (_grid[location].Value == TileValue.Box)
        {
            location = location.Next(direction);
        }
        return _grid[location].Value == TileValue.Space;
    }

    private bool IsLookingAtBox(XyCoord coord)
    {
        return _grid[coord].Value == TileValue.Box;
    }

    private bool IsOccupied(XyCoord coord)
    {
        return _grid[coord].Value != TileValue.Space;
    }

    private void Swap(XyCoord from, XyCoord to)
    {
        _grid.Swap(from, to);
    }
}