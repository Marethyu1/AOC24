using Helpers.Solution;

namespace Solutions.Day1;

public class Day1Solution: ISolution
{
    private readonly List<string> _input;

    public Day1Solution(List<string> input)
    {
        _input = input;
    }
    
    public long SolvePart1()
    {
        throw new NotImplementedException();
    }

    public long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static Day1Solution LoadSolution(string input)
    {
        var lines = File.ReadAllLines(input).ToList();
        return new Day1Solution(lines);
    }
}