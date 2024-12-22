using System.Text;

namespace Helpers.XyGrid;

public class XyGrid<T>
{
    private readonly T[][] _grid;
    public int Width { get;  }
    public int Height { get;  }
    
    public IEnumerable<XyCoord> EnumerateCoords()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                yield return new XyCoord(j, i);
            }
        }
    }
    
    public T this[XyCoord coord]
    {
        get
        {
            try
            {
                return _grid[coord.TilesFromTopWall][coord.TilesFromLeftWall];
            }
            catch (IndexOutOfRangeException e)
            {
                throw new CoordinateOutOfXyGridException(coord);
            }
        }
        private set => _grid[coord.TilesFromTopWall][coord.TilesFromLeftWall] = value;
    }

    public string Display()
    {
        var s = new StringBuilder();
        foreach (var row in _grid)
        {
            foreach (var tile in row)
            {
                s.Append(tile); 
            }
            s.AppendLine();
        }

        return s.ToString();
    }

    public XyGrid(T[][] grid)
    {
        _grid = grid;
        Width = grid[0].Length;
        Height = grid.Length;
    }

    public void Swap(XyCoord from, XyCoord to)
    {
        (this[from], this[to]) = (this[to], this[from]);
    }
}

public class CoordinateOutOfXyGridException(XyCoord coord) : Exception($"({coord.TilesFromLeftWall},{coord.TilesFromTopWall}) is outside the grid coordinates")
{
    
}