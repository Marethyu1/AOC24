// See https://aka.ms/new-console-template for more information

using Helpers;
using Solutions.Day1;


var inputFile = FilePathHelper.GetFullFilePath("1_basic.txt");

var solution = Day1Solution.LoadSolution(inputFile);

solution.SolvePart1();