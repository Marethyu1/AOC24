using Helpers.Grid;
using Helpers.Solution;
using Solutions.Day7;

namespace Solutions.Day8;

public class Day8Solution(Grid<char> grid) : ISolution
{
    public long SolvePart1()
    {
        var frequencyLocations = GetFrequencyLocations();
        var antennaLocations = new HashSet<Coord>();
        foreach (var (key, location) in frequencyLocations)
        {
            var locationPairings = PermutationGenerator<Coord>.GetPairings(location.ToArray());
            foreach (var (coord1, coord2) in locationPairings)
            {
                var columnDelta = Math.Abs(coord1.C - coord2.C);
                var rowDelta = Math.Abs(coord1.R - coord2.R);
                
            }
        }
        
        var max = frequencyLocations.Values.Max(v => v.Count);
        return max;
        return frequencyLocations['A'].Count;
    }

    private Dictionary<char, HashSet<Coord>> GetFrequencyLocations()
    {
        var frequencyLocations = new Dictionary<char, HashSet<Coord>>();

        foreach (var coord in grid.EnumerateCoords())
        {
            var currentFrequency = grid[coord];
            if (currentFrequency == '.') continue;
            if (frequencyLocations.ContainsKey(currentFrequency))
            {
                frequencyLocations[currentFrequency].Add(coord);
            }
            else
            {
                frequencyLocations.Add(currentFrequency, [coord]);
            }
        }
        return frequencyLocations;
    }

    public long SolvePart2()
    {
        return 1;
    }

    public static Day8Solution LoadSolution(string basicInput)
    {
        var gridInput = File.ReadAllLines(basicInput)
            .Select(x => x.ToCharArray())
            .ToArray();
        return new Day8Solution(new Grid<char>(gridInput));
    }
}