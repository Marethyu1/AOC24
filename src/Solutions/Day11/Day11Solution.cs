using System.Numerics;
using Helpers.Solution;

namespace Solutions.Day11;

public class Day11Solution(IEnumerable<BigInteger> stones) : ISolution
{
    public long SolvePart1()
    {
        var currentStones = stones.ToArray();
        
        for (int i = 0; i < 25; i++)
        {
            currentStones = Blink(currentStones).ToArray();
        }

        return currentStones.Count();
    }

    private IEnumerable<BigInteger> Blink(IEnumerable<BigInteger> stones)
    {
        IEnumerable<BigInteger> newStones = new List<BigInteger>(); 
        foreach (var stone in stones)
        {
            newStones = newStones.Concat(Blink(stone));
        }

        return newStones;
    }

    private static BigInteger[] Blink(BigInteger stone)
    {
        if (stone == 0)
        {
            return [1];
        }
        if (EvenDigits(stone))
        {
            return Split(stone);
        }
        return [stone * 2024];
    }

    private static bool EvenDigits(BigInteger stone)
    {
        return $"{stone}".Length % 2 == 0;
    }

    private static BigInteger[] Split(BigInteger stone)
    {
        var stringStone = $"{stone}";
        var half = stringStone.Length / 2;
        var lhsSplit = string.Join("", stringStone.Take(half));
        var rhSplit = string.Join("", stringStone.Skip(half));
        return [BigInteger.Parse(lhsSplit), BigInteger.Parse(rhSplit)];
    }
    
    public long SolvePart2()
    {
        var map = new Dictionary<BigInteger, double>();
        foreach (var stone in stones)
        {
            if (!map.TryAdd(stone, 1))
            {
                map[stone] += 1;
            }
        }
        for (var i = 0; i < 75; i++)
        {
            var nextMap = new Dictionary<BigInteger, double>();
            foreach (var (stone, currentAmountOfStones) in map)
            {
                foreach (var nextStone in Blink(stone))
                {
                    if (!nextMap.TryAdd(nextStone, currentAmountOfStones))
                    {
                        nextMap[nextStone] += currentAmountOfStones;
                    }
                }
            }
            map = nextMap;
        }
        

        return (long) map.Sum(x => x.Value);
    }
    

    public static Day11Solution LoadSolution(string fileName)
    {
        var stones = File.ReadAllLines(fileName)
            .SelectMany(line => line.Split(" ")
                .Select(BigInteger.Parse));
        return new Day11Solution(stones);
    }
}