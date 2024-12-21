// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day13;

var basicInput = FilePathHelper.GetFullFilePath("13_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("13_full.txt");


var basicSolution = Day13Solution.LoadSolution(basicInput, 11, 7);
var fullSolution = Day13Solution.LoadSolution(fullInputFile, 101, 103);
// Console.WriteLine($"---{nameof(Day13Solution)}-basic-part-1---");
// Console.WriteLine(basicSolution.SolvePart1());
// Console.WriteLine($"---{nameof(Day13Solution)}-full-part-1---");
// Console.WriteLine(fullSolution.SolvePart1());
//
// Console.WriteLine("");
// Console.WriteLine($"---{nameof(Day12Solution)}-basic-part-2---");
// Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day13Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());