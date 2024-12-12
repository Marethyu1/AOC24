// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day11;

var basicInput = FilePathHelper.GetFullFilePath("11_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("11_full.txt");


var basicSolution = Day11Solution.LoadSolution(basicInput);
var fullSolution = Day11Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day11Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day11Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day11Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day11Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());