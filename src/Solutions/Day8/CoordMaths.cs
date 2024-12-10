using Helpers.Grid;

namespace Solutions.Day8;

public static class CoordMaths
{
    public static IEnumerable<Coord> Delta(Coord a, Coord b)
    {
        var rows = new List<int>()
        {
            a.R, b.R
        };
        rows.Sort();
        var columns = new List<int>()
        {
            a.C, b.C
        };
        columns.Sort();
        
        var columnDelta = Math.Abs(a.C - b.C);
        var rowDelta = Math.Abs(a.R - b.R);
        if (IsOnSameRow(a, b))
        {
            yield return new Coord(rows[0], columns[0] - columnDelta);
            yield return new Coord(rows[0], columns[1] + columnDelta);
        }
        else if (IsOnSameColumn(a, b))
        {
            yield return new Coord(rows[0] - rowDelta, columns[0]);
            yield return new Coord(rows[1] + rowDelta, columns[0]);
        }
        else if (IsUpRightDiagonal(a, b))
        {
            yield return new Coord(rows[0] - rowDelta, columns[1] - columnDelta);
            yield return new Coord(rows[1] + rowDelta, columns[0] - columnDelta);
        }
        // yield return new Coord(0, 1);
    }

    private static bool IsUpRightDiagonal(Coord a, Coord b)
    {
        return true;
    }

    private static bool IsOnSameColumn(Coord a, Coord b)
    {
        return a.C == b.C;
    }

    private static bool IsOnSameRow(Coord a, Coord b)
    {
        return a.R == b.R;
    }
}