using Helpers.Grid;
using Helpers.XyGrid;

namespace Solutions.Day16;

internal record Location(XyCoord CurrentCoord, Direction Facing)
{
    public Location Next()
    {
        return this with { CurrentCoord = CurrentCoord.Next(Facing) };
    }

    public override string ToString()
    {
        return $"({CurrentCoord.TilesFromLeftWall}, {CurrentCoord.TilesFromTopWall}) {Facing}";
    }
};

public class MazeSolver
{
    private readonly XyGrid<MazeTile> _grid;
    private Action<string> _log;
    private readonly XyCoord _startPoint;
    private const Direction StartFacing = Direction.Right;
    private Location _startLocation;
    private XyCoord _endPoint;
    private const TileValue StartTileValue = TileValue.Start;

    private readonly Direction[] _directions =
    [
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    ];
    

    public MazeSolver(XyGrid<MazeTile> grid, Action<string> log)
    {
        _grid = grid;
        _log = log;
        _startPoint = FindStart();
        _startLocation = new Location(_startPoint, StartFacing);
        _endPoint = _grid.EnumerateCoords().First(c => _grid[c].Value == TileValue.End);
    }

    public XyCoord FindStart()
    {
        return _grid.EnumerateCoords().First(coord => _grid[coord].Value == StartTileValue);
    }


    public int Solve()
    {
        var queue = new PriorityQueue<Location, int>();
        var moves = new HashSet<Location>();
        var score = new Dictionary<Location, int>();

        var initialMove = new Location(_startPoint, StartFacing);
        score[initialMove] = 0;
        queue.Enqueue(initialMove, score[initialMove]);
        

        while (queue.Count > 0)
        {
            var currentLocation = queue.Dequeue();
            // _log($"Processing {currentLocation} [{score[currentLocation]}]");
            if (_grid[currentLocation.CurrentCoord].Value == TileValue.End)
            {
                // _log($"\tFound end of {currentLocation.CurrentCoord}");
                // _log($"\tScore: {score[currentLocation]}");
                return score[currentLocation];
            };
            
            if (moves.Add(currentLocation))
            {
                foreach (var location in GenerateMoves(currentLocation))
                {
                    // _log($"\tAdding {location}");
                    
                    if (location.Facing == currentLocation.Facing)
                    {
                        score[location] = score[currentLocation] + 1;
                    }
                    else
                    {
                        score[location] = score[currentLocation] + 1000;
                    }
                    queue.Enqueue(location, score[location]);
                }
            }
            else
            {
                // _log($"\tAlready Visited {currentLocation}");
            }
            
        }
        // _log("end");
        return -1;
    }

    public long SolvePart2()
    {
        
        var scoreToReach = Solve();
        _log($"Target score: {scoreToReach}");
        
        var queue = new PriorityQueue<Location, int>();
        var moves = new HashSet<Location>();
        var visits = new Dictionary<XyCoord, XyCoord>();
        var allVisits = new List<Location>();
        var score = new Dictionary<Location, int>();
        

        var initialMove = new Location(_startPoint, StartFacing);
        
        score[initialMove] = 0;
        queue.Enqueue(initialMove, score[initialMove]);
        
        var previousLocation = initialMove;
        while (queue.Count > 0)
        {
            var currentLocation = queue.Dequeue();
            
            _log($"Visiting {currentLocation} [{score[currentLocation]}]");
            if (_grid[currentLocation.CurrentCoord].Value == TileValue.End)
            {
                _log($"\tFound end of {currentLocation.CurrentCoord}");
                _log($"\tScore: {score[currentLocation]}");
            };

            var isNewLocation = moves.Add(currentLocation); 
            
            if (isNewLocation)
            {
                allVisits.Add(currentLocation);
                foreach (var location in GenerateMoves(currentLocation))
                {
                    _log($"\tAdding {location}");
                    
                    if (location.Facing == currentLocation.Facing)
                    {
                        score[location] = score[currentLocation] + 1;
                    }
                    else
                    {
                        score[location] = score[currentLocation] + 1000;
                    }
                    
                    queue.Enqueue(location, score[location]);
                           
                }
            }
            else
            {
                _log($"\tAlready Visited {currentLocation}");
            }
            
        }
        _log("end");
        foreach (var location in allVisits)
        {
            _log($"\tvisited {location}");
        }
        var alluniqueVisits = allVisits.
                Select(x => x.CurrentCoord)
                .Distinct()
                .Count();
        return alluniqueVisits;
    }

    private Location[] GenerateMoves(Location currentLocation)
    {
        var potentialLocations = new List<Location>();
        
        var straightAhead = currentLocation.Next();

        if (!IsWall(straightAhead))
        {
            potentialLocations.Add(straightAhead);
        }
        
        var clockWiseDirection = NextDirectionClockWise(currentLocation.Facing);
        var clockWiseLocation = currentLocation with { Facing = clockWiseDirection };
        potentialLocations.Add(clockWiseLocation);  
        
        var antiClockWiseDirection = NextDirectionAntiClockWise(currentLocation.Facing);
        var antiClockWiseLocation = currentLocation with { Facing = antiClockWiseDirection };
        potentialLocations.Add(antiClockWiseLocation);  
        
        return potentialLocations.ToArray();
    }

    private static Direction NextDirectionClockWise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    private static Direction NextDirectionAntiClockWise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Left,
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private bool IsLookingAtWall(Location currentLocation)
    {
        var nextSpot = currentLocation.Next();
        return IsWall(nextSpot);
    }

    private bool IsWall(Location location)
    {
        return IsWall(location.CurrentCoord);
    }

    private bool IsWall(XyCoord currentLocation)
    {
        return _grid[currentLocation].Value == TileValue.Wall;
    }

    public string Display()
    {
        return _grid.Display();
    }
}