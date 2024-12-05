// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day4;


var basicInput = FilePathHelper.GetFullFilePath("4_basic.txt");
var fullInputFile = FilePathHelper.GetFullFilePath("4_full.txt");


var basicSolution = Day4Solution.LoadSolution(basicInput);
var fullSolution = Day4Solution.LoadSolution(fullInputFile);
Console.WriteLine($"---{nameof(Day4Solution)}-basic-part-1---");
Console.WriteLine(basicSolution.SolvePart1());
Console.WriteLine($"---{nameof(Day4Solution)}-full-part-1---");
Console.WriteLine(fullSolution.SolvePart1());

Console.WriteLine("");
Console.WriteLine($"---{nameof(Day4Solution)}-basic-part-2---");
Console.WriteLine(basicSolution.SolvePart2());
Console.WriteLine($"---{nameof(Day4Solution)}-full-part-2---");
Console.WriteLine(fullSolution.SolvePart2());