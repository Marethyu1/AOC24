using Helpers.Solution;

namespace Solutions.Day2;

public class Day2Solution: ISolution
{
    private readonly List<List<int>> _rows;

    public Day2Solution(List<List<int>> rows)
    {
        _rows = rows;
    }

    public long SolvePart1()
    {
        long count = 0;
        foreach (var row in _rows)
        {
            if (IsSafe(row))
            {
                count++;
            }
            
        }

        return count;
    }

    private static bool IsSafe(List<int> row)
    {
        var isRowSafe = true;
            
        var firstNumber = row[0];
        var secondNumber = row[1];
        if (firstNumber == secondNumber)
        {
            return false;
        }

        bool sortUpwards = !(firstNumber > secondNumber);
        for (var i = 0; i < row.Count -1; i++)
        {
            var current = row[i];
            var next = row[i + 1];
            var difference = Math.Abs(current - next);
            var isValid = difference is >= 1 and <= 3;
            if (!isValid)
            {
                isRowSafe = false;
            }

            if (sortUpwards)
            {
                if (current >= next)
                {
                    isRowSafe = false;
                }
            }
            else //sort downwards
            {
                if (current <= next)
                {
                    isRowSafe = false;
                }
            }
        }
        return isRowSafe;
    }

    public long SolvePart2()
    {
        long count = 0;
        foreach (var row in _rows)
        {
            var isSafe = false;
            for (var i = 0; i < row.Count; i++)
            {
                var subset = new List<int>();
                for (var j = 0; j < row.Count; j++)
                {
                    if (i != j)
                    {
                        subset.Add(row[j]);
                    }
                }

                if (IsSafe(subset))
                {
                    isSafe = true;
                }
                
            }

            if (isSafe)
            {
                count++;
            }
        }

        return count;
    }

    public static Day2Solution LoadSolution(string input)
    {
        var rows = new List<List<int>>();
        
        foreach (var line in File.ReadAllLines(input))
        {
            var splitLine = line.Split(" ")
                .Select(int.Parse);
            rows.Add(splitLine.ToList());
        }
        return new Day2Solution(rows);
    }

}