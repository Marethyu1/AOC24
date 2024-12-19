using System.Text;

namespace Solutions.Day13;

public class XyGrid
{
    public int Width { get;  }
    public int Height { get;  }
    private readonly HashSet<XYRobot>[][] _grid;
    
    public XyGrid(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = Populate();
    }
    
    public HashSet<XYRobot> this[XyCoord coord]
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
        set => _grid[coord.TilesFromTopWall][coord.TilesFromLeftWall] = value;
    }

    private HashSet<XYRobot>[][] Populate()
    {
        var grid = new List<HashSet<XYRobot>[]>();
        for (var i = 0; i < Height; i++)
        {
            var row = new List<HashSet<XYRobot>>();
            for (var j = 0; j < Width; j++)
            {
                row.Add([]);
            }
            grid.Add(row.ToArray());
        }
        return grid.ToArray();
    }

    public string Display()
    {
        var s = new StringBuilder();
        foreach (var row in _grid)
        {
            foreach (var xyCoord in row)
            {
                var str = xyCoord.Count > 0 ? xyCoord.Count.ToString() : ".";
               s.Append(str); 
            }
            s.AppendLine();
        }

        return s.ToString();
    }

    public XyCoord Move(XYRobot robot, XyRobotMath xyRobotMath)
    {
        this[robot.CurrentPosition].Remove(robot);
        robot.NextPosition(xyRobotMath);
        this[robot.CurrentPosition].Add(robot);
        return robot.CurrentPosition;
    }
}

public class CoordinateOutOfXyGridException : Exception
{
    public CoordinateOutOfXyGridException(XyCoord coord) : base(coord.ToString())
    {

    }
}