// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day7;


var basicInput = FilePathHelper.GetFullFilePath("7_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("7_full.txt");


var basicSolution = Day7Solution.LoadSolution(basicInput);
var fullSolution = Day7Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day7Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day7Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day7Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day7Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());