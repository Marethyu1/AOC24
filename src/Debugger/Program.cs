// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day9;

var basicInput = FilePathHelper.GetFullFilePath("9_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("9_full.txt");


var basicSolution = Day9Solution.LoadSolution(basicInput);
var fullSolution = Day9Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day9Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
// Console.WriteLine($"---{nameof(Day9Solution)}-full-part-1---");
// Console.WriteLine(fullSolution.SolvePart1());
//
// Console.WriteLine("");
// Console.WriteLine($"---{nameof(Day12Solution)}-basic-part-2---");
// Console.WriteLine(basicSolution.SolvePart2());
// Console.WriteLine($"---{nameof(Day12Solution)}-full-part-2---");
// Console.WriteLine(fullSolution.SolvePart2());