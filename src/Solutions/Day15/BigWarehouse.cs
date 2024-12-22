using Helpers.Grid;
using Helpers.XyGrid;

namespace Solutions.Day15;

public class BigWarehouse
{
    private readonly XyGrid<Tile> _grid;
    private readonly Queue<Direction> _commands;
    private static TileValue _startValue = TileValue.Robot;
    public int MovesLeft => _commands.Count;

    private XyCoord _robotLocation;
    
    public XyCoord Start { get; set; }

    public BigWarehouse(XyGrid<Tile> grid, Queue<Direction> commands)
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

        if (IsMovingHorizontally(direction))
        {
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
                return _robotLocation;
            }
        }
        else // moving vertically
        {
            if (IsLookingAtBox(nextLocation))
            {
                var (doesSequenceResultInSpace, count) = DoesSequenceResultInSpaceWithCount(nextLocation, direction);
                if (!doesSequenceResultInSpace)
                {
                    return _robotLocation;
                }
                
                var parallelCord = GetParallelCord(nextLocation, direction);
                var (doesParallelSequenceResultInSpace, parallelCount) = DoesSequenceResultInSpaceWithCount(parallelCord, direction);
                if (!doesParallelSequenceResultInSpace)
                {
                    return _robotLocation;
                }

                if (count != parallelCount)
                {
                    return _robotLocation;
                }
                
                
                var swaps = GetSwaps(nextLocation, direction);
                while (swaps.Count > 0)
                {
                    var currentSwap = swaps.Pop();
                    Swap(currentSwap.Item1, currentSwap.Item2);
                }
                
                var parallelSwaps = GetSwaps(parallelCord, direction);
                while (parallelSwaps.Count > 0)
                {
                    var currentSwap = parallelSwaps.Pop();
                    Swap(currentSwap.Item1, currentSwap.Item2);
                }
                
                Swap(_robotLocation, nextLocation);
                _robotLocation = nextLocation;
            }
        }
        
        
        
        return _robotLocation;
    }

    private XyCoord GetParallelCord(XyCoord nextLocation, Direction direction)
    {
        var nextLocationTile = _grid[nextLocation];
        var tilesFromLeftWall = -1;
        var timesFromTopwall = -1;
        if (nextLocationTile.Value is TileValue.LhBox)
        {
            tilesFromLeftWall = nextLocation.TilesFromLeftWall + 1;
        } else if (nextLocationTile.Value == TileValue.RhBox)
        {
            tilesFromLeftWall = nextLocation.TilesFromLeftWall + 1;
        }
        else
        {
            throw new Exception($"Shouldnt have got here");
        }

        // if (direction == Direction.Up)
        // {
        //     timesFromTopwall = nextLocation.TilesFromLeftWall - 1;
        // } else if (direction == Direction.Down)
        // {
        //     timesFromTopwall = timesFromTopwall + 1;
        // }
        // else
        // {
        //     throw new Exception("Shouldnt have got here either");
        // }
        return nextLocation with { TilesFromLeftWall = tilesFromLeftWall };
    }

    private Stack<Tuple<XyCoord, XyCoord>> GetSwaps(XyCoord currentLocation, Direction direction)
    {
        var stack = new Stack<Tuple<XyCoord, XyCoord>>();
        var nextLocation = currentLocation.Next(direction);
        stack.Push(new Tuple<XyCoord, XyCoord>(currentLocation, nextLocation));
        while (IsLookingAtBox(nextLocation))
        {
            currentLocation = nextLocation;
            nextLocation = nextLocation.Next(direction);
            stack.Push(new Tuple<XyCoord, XyCoord>(currentLocation, nextLocation));
        }

        return stack;
    }
    
    private (bool, int) DoesSequenceResultInSpaceWithCount(XyCoord location, Direction direction)
    {
        var count = 0;
        while (IsLookingAtBox(location))
        {
            location = location.Next(direction);
            count++;
        }
        return (_grid[location].Value == TileValue.Space, count);
    }

    private bool DoesSequenceResultInSpace(XyCoord location, Direction direction)
    {
        while (IsLookingAtBox(location))
        {
            location = location.Next(direction);
        }
        return _grid[location].Value == TileValue.Space;
    }

    private bool IsMovingHorizontally(Direction direction)
    {
        return direction is Direction.Left or Direction.Right;
    }

    private bool IsLookingAtBox(XyCoord coord)
    {
        return _grid[coord].Value == TileValue.LhBox || _grid[coord].Value == TileValue.RhBox;
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