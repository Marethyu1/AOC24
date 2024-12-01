using Helpers.Solution;

namespace Solutions.Day1;

public class Day1Solution: ISolution
{
    private readonly List<int> _leftList;
    private readonly List<int> _rightList;


    private Day1Solution(List<int> leftList, List<int> rightList)
    {
        _leftList = leftList;
        _rightList = rightList;
    }

    public long SolvePart1()
    {
        _leftList.Sort();
        _rightList.Sort();
        long count = 0;
        for (var i = 0; i < _leftList.Count; i++)
        {
            var remainder = Math.Abs(_leftList[i] - _rightList[i]);
            count += remainder;
        }

        return count;
    }

    public long SolvePart2()
    {
        long count = 0;
        var counter = new Dictionary<int, int>();
        foreach (var number in _rightList)
        {
            if (counter.TryGetValue(number, out _))
            {
                counter[number] += 1;
            }
            else
            {
                counter[number] = 1;
            }
        }

        foreach (var number in _leftList)
        {
            if (counter.TryGetValue(number, out int value))
            {
                count += number * value;
            }
        }

        return count;
    }

    public static Day1Solution LoadSolution(string input)
    {
        
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in File.ReadAllLines(input))
        {
            var splitLIne = line.Split("   ")
                .Select(x => x.Trim())
                .Select(int.Parse).ToList();
            var lhs = splitLIne[0];
            var rhs = splitLIne[1];
            leftList.Add(lhs);
            rightList.Add(rhs);
        }
        
        return new Day1Solution(leftList, rightList);
    }
}