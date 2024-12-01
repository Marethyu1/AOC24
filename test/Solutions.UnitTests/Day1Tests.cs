namespace Solutions.UnitTests;

public class Day1Tests
{
    [Fact]
    public void OnGroup()
    {
        var input = new List<int>(){ 1, 1, 2, 3, 4};
        var grouping = input.GroupBy(x => x);
        
        Assert.Equal(4, grouping.Count());
        
    }
}