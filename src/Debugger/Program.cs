// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day6;


var basicInput = FilePathHelper.GetFullFilePath("6_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("6_full.txt");


var basicSolution = Day6Solution.LoadSolution(basicInput);
var fullSolution = Day6Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day6Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day6Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day6Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day6Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());