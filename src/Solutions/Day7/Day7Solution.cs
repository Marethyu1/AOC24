using Helpers.Solution;

namespace Solutions.Day7;

public enum Operation
{
    Add, 
    Multiply,
}

public class Day7Solution(IEnumerable<Equation> equations) : ISolution
{
    
    public long SolvePart1()
    {
        long sum = 0;
        foreach (var equation in equations)
        {
            if (CanSolveEquation(equation))
            {
                sum += equation.Total;
            }
        }

        return sum;
    }

    private bool CanSolveEquation(Equation equation)
    {
        var equationValues = equation.Values;
        var perms = PermutationGenerator<Operation>.
            GetPermutationsV2(Operation.Add, Operation.Multiply, equationValues.Length - 1)
            .ToList();
        
        foreach (var perm in perms)
        {
            var total = equationValues[0];
            for (var i = 1; i < equation.Values.Length; i++)
            {
                var operation = perm[i - 1];
                if (operation == Operation.Add)
                {
                    total += equationValues[i];
                }
                if (operation == Operation.Multiply)
                {
                    total *= equationValues[i];
                }
            }

            if (total == equation.Total)
            {
                return true;
            }
        }
        return false;
    }

    public long SolvePart2()
    {
        throw new NotImplementedException();
    }

    public static Day7Solution LoadSolution(string basicInput)
    {
        var equations = File.ReadAllLines(basicInput)
            .Select(line =>
            {
                var firstLine = line.Split(":");
                var testValue = long.Parse(firstLine[0]);
                var numbers = firstLine[1]
                    .Trim()
                    .Split(" ")
                    .Select(long.Parse)
                    .ToArray();
                return new Equation(testValue, numbers);
            });
        return new Day7Solution(equations);
    }
}