// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day2;


var basicInput = FilePathHelper.GetFullFilePath("2_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("2_full.txt");


var basicSolution = Day2Solution.LoadSolution(basicInput);
var fullSolution = Day2Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day2Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day2Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day2Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day2Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());