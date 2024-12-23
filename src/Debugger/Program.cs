// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day16;

var basicInput = FilePathHelper.GetFullFilePath("16_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("16_full.txt");


var basicSolution = Day16Solution.LoadSolution(basicInput);
var fullSolution = Day16Solution.LoadSolution(fullInputFile);
// Console.WriteLine($"---{nameof(Day16Solution)}-basic-part-1---");
// Console.WriteLine(basicSolution.SolvePart1());
// Console.WriteLine($"---{nameof(Day16Solution)}-full-part-1---");
// Console.WriteLine(fullSolution.SolvePart1());
//
// Console.WriteLine("");
// Console.WriteLine($"---{nameof(Day16Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
// Console.WriteLine($"---{nameof(Day16Solution)}-full-part-2---");
// Console.WriteLine(fullSolution.SolvePart2());