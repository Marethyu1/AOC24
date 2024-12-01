namespace Helpers.Parsers;

public class ListStringParser: IInputParser<List<string>>
{
    public List<string> ParseInput(string filePath)
    {
        return File
            .ReadAllLines(filePath)
            .ToList();
    }
}