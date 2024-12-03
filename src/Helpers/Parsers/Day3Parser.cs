namespace Helpers.Parsers;

public class Day3Parser: IInputParser<string>
{
    public string ParseInput(string filePath)
    {
        var lines = File.ReadAllText(filePath);
        return lines;
    }
}