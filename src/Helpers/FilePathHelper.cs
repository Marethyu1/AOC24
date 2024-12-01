namespace Helpers;

public static class FilePathHelper
{
    public static string GetFullFilePath(string fileName)
    {
        return $"../../../../../src/inputs/{fileName}";
    }
}