using Helpers.Parsers;
using Helpers.Solution;

namespace Solutions.Day3;

public class Day3Solution(string input) : ISolution
{
    private const string StartSubstring = "mul(";
    private const string DoSubstring = "do()";
    private const string DontSubString = "don't()";
    public long SolvePart1()
    {
        long sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (!Matcher.DoesSubStringMatch(input, i, StartSubstring))
            {
                continue;
            }
            else
            {
                // mul(12345,12345)
                // 0123456789 0 + 4
               var firstMultipleIndex = i + StartSubstring.Length;
               var firstMultipleSequence =  Matcher.MatchSequenceOfInts(input, firstMultipleIndex, ',');

               if (!firstMultipleSequence.IsSequence) continue;
                   
               var secondMultipleIndex = firstMultipleIndex + firstMultipleSequence.GetSequence.Length + 1;
               var secondMultipleSequence =  Matcher.MatchSequenceOfInts(input, secondMultipleIndex, ')');
               if (!secondMultipleSequence.IsSequence) continue;
               
               sum += firstMultipleSequence.AsInt * secondMultipleSequence.AsInt;
               
            }
        }
        return sum;
    }

    

    public long SolvePart2()
    {
        long sum = 0;
        bool shouldMultiply = true;
        for (int i = 0; i < input.Length; i++)
        {
            
            if (Matcher.DoesSubStringMatch(input, i, DoSubstring))
            {
                shouldMultiply = true;
                continue;
            }
            else if (Matcher.DoesSubStringMatch(input, i, DontSubString))
            {
                shouldMultiply = false;
                continue;
            }
            if (!Matcher.DoesSubStringMatch(input, i, StartSubstring))
            {
                continue;
            }
            else
            {
                // mul(12345,12345)
                // 0123456789 0 + 4
                var firstMultipleIndex = i + StartSubstring.Length;
                var firstMultipleSequence =  Matcher.MatchSequenceOfInts(input, firstMultipleIndex, ',');

                if (!firstMultipleSequence.IsSequence) continue;
                   
                var secondMultipleIndex = firstMultipleIndex + firstMultipleSequence.GetSequence.Length + 1;
                var secondMultipleSequence =  Matcher.MatchSequenceOfInts(input, secondMultipleIndex, ')');
                if (!secondMultipleSequence.IsSequence) continue;

                if (shouldMultiply)
                {
                    sum += firstMultipleSequence.AsInt * secondMultipleSequence.AsInt;
                }
                
               
            }
        }
        return sum;
    }

    public static Day3Solution LoadSolution(string filePath)
    {
        var input = new Day3Parser()
            .ParseInput(filePath);
        return new Day3Solution(input);
    }
}