// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day1;


var inputFile = FilePathHelper.GetFullFilePath("1_full.txt");

var solution = Day1Solution.LoadSolution(inputFile);

Console.WriteLine(solution.SolvePart1());
Console.WriteLine(solution.SolvePart2());