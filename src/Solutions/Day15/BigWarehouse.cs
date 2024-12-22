using Helpers.Grid;
using Helpers.XyGrid;
using XyCoord = Helpers.XyGrid.XyCoord;

namespace Solutions.Day15;

internal record BigBox(XyCoord Lhs, XyCoord Rhs)
{
    public List<XyCoord> Coords => [Lhs, Rhs];
    public List<XyCoord> CoordsInDirection(Direction direction) => [Lhs.Next(direction), Rhs.Next(direction)];
};

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
                var parallelCord = GetParallelCord(nextLocation);
                var box = GetBox(nextLocation, parallelCord);
                var stack = new Stack<HashSet<BigBox>>();
                stack.Push([box]);

                while (stack.Peek().Any(x => IsOccupied(x.Lhs) || IsOccupied(x.Rhs)))
                {
                    var bigBox = new HashSet<BigBox>();
                    foreach (var currentBox in stack.Peek())
                    {
                        foreach (var aboveCoord in currentBox.CoordsInDirection(direction))
                        {
                            if (IsLookingAtWall(aboveCoord))
                            {
                                return _robotLocation;
                            }

                            if (IsLookingAtBox(aboveCoord))
                            {
                                var besideCord = GetParallelCord(aboveCoord);
                                var aboveBox = GetBox(besideCord, aboveCoord);
                                bigBox.Add(aboveBox);
                            }
                        }
                    }
                    stack.Push(bigBox);
                }
                

                Console.WriteLine(stack);
                while (stack.Count > 0)
                {
                    var boxesToProcess = stack.Pop();
                    foreach (var boxToProcess in boxesToProcess)
                    {
                        Swap(boxToProcess.Lhs, boxToProcess.Lhs.Next(direction));
                        Swap(boxToProcess.Rhs, boxToProcess.Rhs.Next(direction));
                    }
                }
                
                Swap(_robotLocation, nextLocation);
                _robotLocation = nextLocation;
                return _robotLocation;
                // Look at the boxes at a given level 
                // for Each box, is it either above another box or above open space
                // if its above another box, add the box to the next level 


                // var (doesSequenceResultInSpace, count) = DoesSequenceResultInSpaceWithCount(nextLocation, direction);
                // if (!doesSequenceResultInSpace)
                // {
                //     return _robotLocation;
                // }
                //
                // var parallelCord = GetParallelCord(nextLocation, direction);
                // var (doesParallelSequenceResultInSpace, parallelCount) = DoesSequenceResultInSpaceWithCount(parallelCord, direction);
                // if (!doesParallelSequenceResultInSpace)
                // {
                //     return _robotLocation;
                // }
                //
                // if (count != parallelCount)
                // {
                //     return _robotLocation;
                // }
                //
                //
                // var swaps = GetSwaps(nextLocation, direction);
                // while (swaps.Count > 0)
                // {
                //     var currentSwap = swaps.Pop();
                //     Swap(currentSwap.Item1, currentSwap.Item2);
                // }
                //
                // var parallelSwaps = GetSwaps(parallelCord, direction);
                // while (parallelSwaps.Count > 0)
                // {
                //     var currentSwap = parallelSwaps.Pop();
                //     Swap(currentSwap.Item1, currentSwap.Item2);
                // }
                //
                // Swap(_robotLocation, nextLocation);
                // _robotLocation = nextLocation;
            }
        }
        
        
        
        return _robotLocation;
    }

    private BigBox GetBox(XyCoord coordA, XyCoord coordB)
    {
        if (coordA.TilesFromLeftWall < coordB.TilesFromLeftWall)
        {
            return new BigBox(coordA, coordB);
        }

        if (coordA.TilesFromLeftWall > coordB.TilesFromLeftWall)
        {
            return new BigBox(coordB, coordA);
        }

        throw new Exception("Shouldnt have got here");
    }

    private XyCoord GetParallelCord(XyCoord nextLocation)
    {
        var nextLocationTile = _grid[nextLocation];
        if (nextLocationTile.Value != TileValue.LhBox && nextLocationTile.Value != TileValue.RhBox)
        {
            throw new Exception("shouldnt have got here");
        }

        if (nextLocationTile.Value == TileValue.RhBox)
        {
            return nextLocation with
            {
                TilesFromLeftWall = nextLocation.TilesFromLeftWall - 1
            };
        }
        return nextLocation with
        {
            TilesFromLeftWall = nextLocation.TilesFromLeftWall + 1
        };
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

    private bool IsLookingAtWall(XyCoord coord)
    {
        return _grid[coord].Value == TileValue.Wall;
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