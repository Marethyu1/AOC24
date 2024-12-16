// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day12;

var basicInput = FilePathHelper.GetFullFilePath("12_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("12_full.txt");


var basicSolution = Day12Solution.LoadSolution(basicInput);
var fullSolution = Day12Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day12Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day12Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());
//
// Console.WriteLine("");
// Console.WriteLine($"---{nameof(Day12Solution)}-basic-part-2---");
// Console.WriteLine(basicSolution.SolvePart2());
// Console.WriteLine($"---{nameof(Day12Solution)}-full-part-2---");
// Console.WriteLine(fullSolution.SolvePart2());