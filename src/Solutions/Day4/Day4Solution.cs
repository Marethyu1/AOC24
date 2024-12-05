using Helpers.Grid;
using Helpers.Solution;

namespace Solutions.Day4;

public class Day4Solution : ISolution
{
    private readonly Grid<char> _grid;
    private static readonly char[] TargetSequence = ['X', 'M', 'A', 'S'];

    public static bool IsValidSequence(Grid<char> grid, char[] target, Direction direction, Coord coord)
    {
        var currentCord = coord;
        
        foreach (var t in target)
        {
            if (!grid.InBounds(currentCord))
            {
                return false;
            }
            if (grid[currentCord] != t)
            {
                return false;
            }
            currentCord = currentCord.Next(direction);
        }
        return true;
    }

    public Day4Solution(Grid<char> grid)
    {
        _grid = grid;
    }
    
    public long SolvePart1()
    {
        // (0,0), (0,1), (0,2)
        // (1,0), (1,1), (1,2)
        // (2,0), (2,1), (2,2)
        
        // (0,0), 'X'
        // 
        
        
        var matches = 0;
        foreach (var startCoord in _grid.EnumerateCoords())
        {
            var startChar = _grid[startCoord];
            if (startChar != TargetSequence[0])
            {
                continue;
            }
            
            foreach (Direction dir in Enum.GetValues<Direction>())
            {
                if (IsValidSequence(_grid, TargetSequence, dir, startCoord))
                {
                    matches++;
                }
                //
                // continue;
                // var currentCoord = startCoord;
                // for (var i = 1; i < TargetSequence.Length; i++)
                // {
                //     currentCoord = currentCoord.Next(dir);
                //     if (!_grid.InBounds(currentCoord))
                //     {
                //         break;
                //     }
                //     
                //     if (_grid[currentCoord] != TargetSequence[i])
                //     {
                //         break;
                //     }
                //     matches++;
                // }
            }
            
        }
        
        return matches;
    }

    public long SolvePart2()
    {
        var matches = 0;
        foreach (var startCoord in _grid.EnumerateCoords())
        {
            var startChar = _grid[startCoord];
            if (startChar != 'A')
            {
                continue;
            }
            
            
            bool isMas = true;
            foreach (var (directionA, directionB) in MasDirections)
            {
                if (!_grid.InBounds(startCoord.Next(directionA)) || !_grid.InBounds(startCoord.Next(directionB)))
                {
                    isMas = false;
                    continue;
                }
                var charA = _grid[startCoord.Next(directionA)];
                var charB = _grid[startCoord.Next(directionB)];
                if (!(charA == 'M' && charB == 'S' || charB == 'M' && charA == 'S'))
                {
                    isMas = false;
                }
            }
            if (isMas) matches++;
            
        }
        return matches;
    }
    

    private static Tuple<Direction, Direction>[] MasDirections = new Tuple<Direction, Direction>[]
    {
        new(Direction.NorthEast, Direction.SouthWest),
        new(Direction.NorthWest, Direction.SouthEast),
    };
    
    private static Tuple<char, Direction>[] MasLocationsV2 => new[]
    {
        new Tuple<char, Direction>('M', Direction.SouthEast),
        new Tuple<char, Direction>('M', Direction.SouthWest),
        new Tuple<char, Direction>('S', Direction.NorthWest),
        new Tuple<char, Direction>('S', Direction.NorthEast),
    };
    
    private static Tuple<char, Direction>[] MasLocationsV3 => new[]
    {
        new Tuple<char, Direction>('M', Direction.NorthWest),
        new Tuple<char, Direction>('M', Direction.NorthEast),
        new Tuple<char, Direction>('S', Direction.SouthEast),
        new Tuple<char, Direction>('S', Direction.SouthWest),
    };

    public static Day4Solution LoadSolution(string input)
    {
        var gridInput = File.ReadAllLines(input)
            .Select(line => line.Select(c => c).ToArray())
            .ToArray();
        
        var grid = new Grid<char>(gridInput);
        
        return new Day4Solution(grid);
    }
}