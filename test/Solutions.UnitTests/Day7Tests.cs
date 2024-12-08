using Solutions.Day7;

namespace Solutions.UnitTests;

public class Day7Tests
{
    
    [Fact]
    public void CanGenerate3PermutationsV2()
    {
        var str = PermutationGenerator<char>.BinaryRepresentation(0, 8);
        Assert.Equal("00000000", str);
        
        var perms = PermutationGenerator<int>.GetPermutationsV2(1, 2, 2)
            .ToList();
        Assert.Contains([1, 1], perms);
        Assert.Contains([1, 2], perms);
        Assert.Contains([2, 2], perms);
        Assert.Contains([2, 1], perms);
        Assert.Equal(4, perms.Count);
    }
    
    [Fact]
    public void CanGenerate2PermutationsV2()
    {
        var perms = PermutationGenerator<int>.GetPermutationsV2(1, 2, 1)
            .ToList();
        Assert.Contains([1], perms);
        Assert.Contains([2], perms);
        Assert.Equal(2, perms.Count);
    }

    [Fact]
    public void Trim()
    {
        Assert.Equal("10 19", " 10 19".Trim());
    }

    [Fact]
    public void Hmm()
    {
        var equationValues = new List<long>() { 10, 19 };
        var perms = PermutationGenerator<Operation>.GetPermutationsV2(Operation.Add, Operation.Multiply, equationValues.Count - 1)
            .ToList();
        Assert.Contains([Operation.Add], perms);
        Assert.Contains([Operation.Multiply], perms);
    }
    

    [Fact]
    public void TernaryCounter()
    {
        var number = new BaseNumber(3);
        Assert.Equal(0, number.Value);
        number.Increment();
        Assert.Equal(1, number.Value);
        number.Increment();
        Assert.Equal(2, number.Value);
        number.Increment();
        Assert.Equal(0, number.Value);
    }

    [Fact]
    public void ConCat()
    {
        var lhs = 48;
        var rhs = 6;
        var res = long.Parse(string.Join("", lhs.ToString().Concat(rhs.ToString())));
        Assert.Equal(486, res);
    }

    [Fact]
    public void BasNCounterTests()
    {
        var baseNCounter = new BaseNCounter(3, 3);
        Assert.Equal("000", baseNCounter.Value);      //0
        Assert.Equal("001", baseNCounter.NextValue());//1
        Assert.Equal("002", baseNCounter.NextValue());//2
        
        Assert.Equal("010", baseNCounter.NextValue());//3
        
        Assert.Equal("011", baseNCounter.NextValue());//4
        
        Assert.Equal("012", baseNCounter.NextValue());//5
        Assert.Equal("020", baseNCounter.NextValue());//6
        Assert.Equal("021", baseNCounter.NextValue());//7
        Assert.Equal("022", baseNCounter.NextValue());//8
        Assert.Equal("100", baseNCounter.NextValue());//0
        
        var quickMath = new BaseNCounter(3, 4);
        for (int i = 0; i < 54; i++)
        {
            quickMath.NextValue();
        }
        Assert.Equal("2000", quickMath.Value);
    }

    [Fact]
    public void CanSolveThis()
    {
        var eq = new Equation(156, [15, 6]);
        var operations = new[]
        {
            Operation.Add, Operation.Multiply, Operation.Concat  
        };
        var canSolve = Day7Solution.CanSolveEquation(eq, operations);
        
        var perms = PermutationGenerator<Operation>.
            GetPermutationsV3(operations, eq.Values.Length - 1)
            .ToList();
        
        Assert.Contains([Operation.Add], perms);
        
        Assert.True(canSolve);
    }

    [Fact]
    public void FastCounter()
    {
        var counter = new BaseNCounterFast(3, 4);
        Assert.Equal([0, 0, 0, 0, 0], counter.Values);
        Assert.Equal([0, 0, 0, 0, 1], counter.Next());
        Assert.Equal([0, 0, 0, 0, 2], counter.Next());
        Assert.Equal([0, 0, 0, 1, 0], counter.Next());
        Assert.Equal([0, 0, 0, 1, 1], counter.Next());
    }
}