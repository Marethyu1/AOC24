using Helpers.Solution;

namespace Solutions.Day13;

public class Day13Solution(XyGrid grid, List<XYRobot> robots, int rowSize, int columnSize) : ISolution
{
    public long SolvePart1()
    {
        var math = new XyRobotMath(new XyCoord(rowSize, columnSize));
        var ticks = 100;
        
        for (var i = 0; i < ticks; i++)
        {
            foreach (var robot in robots)
            {
                grid.Move(robot, math);
            }
        }

        var halfRow = rowSize / 2;
        var halfColumn = columnSize / 2;

        long topLeftSum = 0;
        for (var i = 0; i < halfColumn; i++)
        {
            for (var j = 0; j < halfRow; j++)
            {
                var coord = new XyCoord(j, i);
                topLeftSum += grid[coord].Count;
            }
        }
        
        long topRightSum = 0;
        for (var i = halfRow + 1; i < rowSize; i++)
        {
            for (var j = 0; j < halfColumn; j++)
            {
                var coord = new XyCoord(i, j);
                topRightSum += grid[coord].Count;
            }
        }
        
        long bottomLeftSum = 0;
        for (var i = 0; i < halfRow; i++)
        {
            for (var j = halfColumn + 1; j < columnSize; j++)
            {
                var coord = new XyCoord(i, j);
                bottomLeftSum += grid[coord].Count;
            }
        }
        
        long bottomRightSum = 0;
        for (var i = halfRow + 1; i < rowSize; i++)
        {
            for (var j = halfColumn + 1; j < columnSize; j++)
            {
                var coord = new XyCoord(i, j);
                bottomRightSum += grid[coord].Count;
            }
        }
        
        
        Console.WriteLine(grid.Display());
        
        return topLeftSum * topRightSum * bottomLeftSum * bottomRightSum;
    }

    public long SolvePart2()
    {

        var coords = new HashSet<XyCoord>();
        for (var i = 25; i < 76; i++)
        {
            for (var j = 25; j < 76; j++)
            {
                coords.Add(new XyCoord(i, j));
            }
        }
        var counter = 10000;
        var math = new XyRobotMath(new XyCoord(rowSize, columnSize));
        for (var i = 1; i < counter; i++)
        {
            
            foreach (var robot in robots)
            {
                grid.Move(robot, math);
            }

            var count = coords.Sum(c => grid[c].Count);
            // if (counter % 100000 == 0)
            // {
            //     Console.WriteLine(counter);
            //     Console.WriteLine(grid.Display());
            //     Console.WriteLine("");
            // }
            if (i is 7084 or 7085 or 7083 or 7082)
            {
                Console.WriteLine(i);
                Console.WriteLine(grid.Display());
            }
            if (count > 256)
            {
                Console.WriteLine(i);
                Console.WriteLine(grid.Display());
            }
        }
        
        return 1;
    }

    public static Day13Solution LoadSolution(string basicInput, int rowSize, int columnSize)
    {
        var robots = File.ReadAllLines(basicInput)
            .Select(line =>
            {
                var splitLine = line.Split(" ");
                var lhs = ToCoord(splitLine[0], "p=");
                var rhs = ToCoord(splitLine[1], "v=");
                return new XYRobot(lhs, rhs);
            }).ToList();
        
        
        var grid = new XyGrid(rowSize, columnSize);
        
        foreach (var robot in robots)
        {
            grid[robot.InitialPosition].Add(robot);
        }

        return new Day13Solution(grid, robots.ToList(), rowSize, columnSize);
    }

    private static XyCoord ToCoord(string input, string match)
    {
        var numbers = input.Replace(match, string.Empty).Split(",").Select(int.Parse).ToArray();
        return new XyCoord(numbers[0], numbers[1]);
    }
}
