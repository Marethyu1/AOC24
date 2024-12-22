using Helpers.Grid;

namespace Helpers.XyGrid;

public record XyCoord(int TilesFromLeftWall, int TilesFromTopWall)
{
    public XyCoord Next(Direction direction)
    {
        return direction switch
        {
            Direction.Up => new XyCoord(TilesFromLeftWall, TilesFromTopWall - 1),
            Direction.NorthEast => new XyCoord(TilesFromLeftWall + 1, TilesFromTopWall - 1),
            Direction.Right => new XyCoord(TilesFromLeftWall + 1, TilesFromTopWall),
            Direction.SouthEast => new XyCoord(TilesFromLeftWall + 1, TilesFromTopWall + 1),
            Direction.Down => new XyCoord(TilesFromLeftWall, TilesFromTopWall + 1),
            Direction.SouthWest => new XyCoord(TilesFromLeftWall - 1, TilesFromTopWall + 1),
            Direction.Left => new XyCoord(TilesFromLeftWall - 1, TilesFromTopWall),
            Direction.NorthWest => new XyCoord(TilesFromLeftWall - 1, TilesFromTopWall - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public IEnumerable<XyCoord> AdjacentCoords(Direction[] directions)
    {
        return directions.Select(Next);
    }
};