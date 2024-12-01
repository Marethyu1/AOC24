namespace Helpers.Parsers;

public interface IInputParser<out T>
{
    T ParseInput(string filePath);
}