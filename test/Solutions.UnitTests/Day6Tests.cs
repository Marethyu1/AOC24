using Helpers.Grid;

namespace Solutions.UnitTests;

public class Day6Tests
{
    [Fact]
    public void ReferenceValidation()
    {
        var s = new Spot('A');
        var spots = new Spot[]
        {
            s
        };
        var grid = new Grid<Spot>(new Spot[][]
        {
            spots
        });
        var d = grid;
        
        Assert.Equal(grid, d);
    }

    [Fact]
    public void Whatever()
    {
        
    }
}