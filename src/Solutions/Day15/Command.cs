using Helpers.Grid;

namespace Solutions.Day15;

public static class Command
{
    public static Direction ToDirection(char c)
    {
        return c switch
        {
            '<' => Direction.Left,
            '>' => Direction.Right,
            'v' => Direction.Down,
            '^' => Direction.Up,
            _ => throw new InvalidOperationException()
        };
    }
}