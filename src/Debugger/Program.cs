// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day10;

var basicInput = FilePathHelper.GetFullFilePath("10_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("10_full.txt");


var basicSolution = Day10Solution.LoadSolution(basicInput);
var fullSolution = Day10Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day10Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day10Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day10Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day10Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());