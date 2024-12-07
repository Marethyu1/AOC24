using Helpers.Grid;
using Helpers.Solution;

namespace Solutions.Day6;

public class Day6Solution(Grid<Spot> grid) : ISolution
{
    public long SolvePart1()
    {
        var currentCoord = GetStart();
        var uniqueLocations = new HashSet<Coord>()
        {
            currentCoord
        };

        var currentDirection = Direction.Up;
        var nextCoord = currentCoord.Next(currentDirection);
        
        while (grid.InBounds(nextCoord))
        {
            while (grid[nextCoord].IsOccupied)
            {
                currentDirection = NextDirection(currentDirection);
                nextCoord = currentCoord.Next(currentDirection);
            }
            currentCoord = nextCoord;
            nextCoord = currentCoord.Next(currentDirection);
            uniqueLocations.Add(currentCoord);
        }
        
        return uniqueLocations.Count;
    }

    public long SolvePart2()
    {
        long count = 0;
        var guardStartLocation = GetStart();

        // think about copies maybe
        var potentialLocations = grid.EnumerateCoords()
            .Where(x => !grid[x].IsOccupied && !grid[x].IsStart);

        foreach (var potentialLocation in potentialLocations)
        {
            grid[potentialLocation] = new Spot('#');

            var currentCoord = guardStartLocation;
            var currentDirection = Direction.Up;
            
            var uniqueLocations = new HashSet<(Coord, Direction)>()
            {
                new (currentCoord, currentDirection)
            };
            
            
            var nextCoord = currentCoord.Next(currentDirection);
            bool loopDetected = false;
        
            while (grid.InBounds(nextCoord) && !loopDetected)
            {
                while (grid[nextCoord].IsOccupied)
                {
                    currentDirection = NextDirection(currentDirection);
                    nextCoord = currentCoord.Next(currentDirection);
                }
                currentCoord = nextCoord;
                nextCoord = currentCoord.Next(currentDirection);
                (Coord, Direction) visit = new(currentCoord, currentDirection);
                if (uniqueLocations.Contains(visit))
                {
                    loopDetected = true;
                }
                uniqueLocations.Add(visit);
            }

            if (loopDetected)
            {
                count++;
            }
            
            grid[potentialLocation] = new Spot('.');
        }

        return count;

    }

    private Coord GetStart()
    {
        return grid.EnumerateCoords()
            .First(coord => grid[coord].IsStart);
    }

    private static Direction NextDirection(Direction currentDirection)
    {
        if (currentDirection == Direction.Up)
        {
            return Direction.Right;
        }
        if (currentDirection == Direction.Right)
        {
            return Direction.Down;
        }
        if (currentDirection == Direction.Down)
        {
            return Direction.Left;
        }
        if (currentDirection == Direction.Left)
        {
            return Direction.Up;
        }
        throw new Exception("this should never happen");
    }

    public static Day6Solution LoadSolution(string basicInput)
    {
        var spots = File.ReadAllLines(basicInput)
            .Select(x => x.Select(y => new Spot(y)).ToArray())
            .ToArray();
        var grid = new Grid<Spot>(spots);
        return new Day6Solution(grid);
    }
}