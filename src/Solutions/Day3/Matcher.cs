using OneOf;
using OneOf.Types;

namespace Solutions.Day3;

public class IsSequenceOrNotMatch : OneOfBase<string, False>
{
    private IsSequenceOrNotMatch(OneOf<string, False> input) : base(input)
    {
    }

    public bool IsSequence => IsT0;

    public string GetSequence => AsT0;
    
    public int AsInt => int.Parse(GetSequence);
    
    public static implicit operator IsSequenceOrNotMatch(string _) => new(_);
    public static implicit operator IsSequenceOrNotMatch(False _) => new(_);
    
}

public static class Matcher
{
    private static readonly HashSet<char> Ints =
    [
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    ];
    
    public static bool DoesSubStringMatch(string input, int startIndex, string substring)
    {
        if (startIndex + substring.Length > input.Length)
        {
            return false;
        }
        var inputSubstring = input.Substring(startIndex, substring.Length);
        return inputSubstring == substring;
    }

    public static IsSequenceOrNotMatch MatchSequenceOfInts(string input, int startIndex, char c)
    {
        if (startIndex > input.Length - 1) return new False();
        var currentIndex = startIndex;
        
        // we need at least one match
        if (!Ints.Contains(input[currentIndex])) return new False();

        var capturedNumbers = "";
        while (Ints.Contains(input[currentIndex]))
        {
            capturedNumbers += input[currentIndex];
            currentIndex++;
        }

        return input[currentIndex] == c ? capturedNumbers : new False();
    }
}