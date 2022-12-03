namespace AdventOfCode;

public static class ResourceParser
{
    private static readonly string FilePath = $"{Directory.GetCurrentDirectory()}/Resources/";

    public static string[] SplitByLine(int day)
    {
        var file = $"{FilePath}day{day}.txt";
        return File.ReadAllText(file).Split(Environment.NewLine);
    }
    
    public static string[] SplitByEmptyLine(int day)
    {
        var file = $"{FilePath}day{day}.txt";
        return File.ReadAllText(file)
            .Split(new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
    }
}