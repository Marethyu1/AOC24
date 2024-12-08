using Helpers.Solution;

namespace Solutions.Day7;

public enum Operation
{
    Add, 
    Multiply,
    Concat,
}

public class Day7Solution(IEnumerable<Equation> equations) : ISolution
{
    
    public long SolvePart1()
    {
        long sum = 0;
        var operations = new[]
        {
            Operation.Add,
            Operation.Multiply
        };
        foreach (var equation in equations)
        {
            if (CanSolveEquation(equation,operations))
            {
                sum += equation.Total;
            }
        }

        return sum;
    }

    public static bool CanSolveEquation(Equation equation, Operation[] operations)
    {
        var equationValues = equation.Values;
        
        foreach (var perm in PermutationGenerator<Operation>.
                     GetPermutationsV3(operations, equationValues.Length - 1))
        {
            var total = equationValues[0];
            for (var i = 1; i < equation.Values.Length; i++)
            {
                var operation = perm[i - 1];
                total = Apply(operation, total, equationValues[i]);
                if (total > equation.Total)
                {
                    break;
                }
            }

            if (total == equation.Total)
            {
                return true;
            }
        }
        return false;
    }

    private static long Apply(Operation operation, long lhs, long rhs)
    {
        return operation switch
        {
            Operation.Add => lhs + rhs,
            Operation.Multiply => lhs * rhs,
            Operation.Concat => long.Parse($"{lhs}{rhs}"),
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };
    }

    public long SolvePart2()
    {
        long sum = 0;
        var operations = new[]
        {
            Operation.Add,
            Operation.Multiply,
            Operation.Concat
        };
        foreach (var equation in equations)
        {
            if (CanSolveEquation(equation,operations))
            {
                sum += equation.Total;
            }
        }

        return sum;
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