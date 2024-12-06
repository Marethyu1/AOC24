namespace Solutions.Day5;

public record PageOrdering(int X, int Y);

public class PageOrderings
{
    private readonly Dictionary<int, List<int>> _orderings = new();
    
    public PageOrderings(PageOrdering[] orderings)
    {
        foreach (var ordering in orderings)
        {
            if (_orderings.TryGetValue(ordering.X, out var existingOrdering))
            {
                _orderings[ordering.X] = existingOrdering.Append(ordering.Y).ToList();
            }
            else
            {
                _orderings[ordering.X] = [ordering.Y];
            }
        }
    }

    public bool IsAfter(int pageY, int pageX)
    {
        return _orderings[pageX].Contains(pageY);
    }

    public bool IsFirst(int i, List<int> items)
    {
        return items.All(item => IsAfter(item, i));
    }
}