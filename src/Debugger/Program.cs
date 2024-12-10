// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day8;

var basicInput = FilePathHelper.GetFullFilePath("8_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("8_full.txt");


var basicSolution = Day8Solution.LoadSolution(basicInput);
var fullSolution = Day8Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day8Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day8Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day8Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day8Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());