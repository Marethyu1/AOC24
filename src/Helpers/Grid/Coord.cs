namespace Helpers.Grid;

public record Coord(int R, int C)
{
    public int R { get;} = R;
    public int C { get;} = C;

    // (0,0), (0,1), (0,2)
    // (1,0), (1,1), (1,2)
    // (2,0), (2,1), (2,2)
    
    public Coord Next(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Coord(R - 1, C),
            Direction.NorthEast => new Coord(R -1, C + 1),
            Direction.Right => new Coord(R, C + 1),
            Direction.SouthEast => new Coord(R + 1, C + 1),
            Direction.Down => new Coord(R + 1, C),
            Direction.SouthWest => new Coord(R + 1, C - 1),
            Direction.Left => new Coord(R, C - 1),
            Direction.NorthWest => new Coord(R - 1, C - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public IEnumerable<Coord> AdjacentCoords(Direction[] directions)
    {
        return directions.Select(Next);
    }
}