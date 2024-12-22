// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day13;
using Solutions.Day15;

var basicInput = FilePathHelper.GetFullFilePath("15_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("15_full.txt");


var basicSolution = Day15Solution.LoadSolution(basicInput);
var fullSolution = Day15Solution.LoadSolution(fullInputFile);
// Console.WriteLine($"---{nameof(Day13Solution)}-basic-part-1---");
// Console.WriteLine(basicSolution.SolvePart1());
// Console.WriteLine($"---{nameof(Day13Solution)}-full-part-1---");
// Console.WriteLine(fullSolution.SolvePart1());
//
Console.WriteLine("");
Console.WriteLine($"---{nameof(Day13Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
// Console.WriteLine($"---{nameof(Day13Solution)}-full-part-2---");
// Console.WriteLine(fullSolution.SolvePart2());