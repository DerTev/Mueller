namespace Mueller.Bot;

public static class MinimalLogger
{
    public static bool Activated;

    public static void Log(string message) { if (Activated) Console.WriteLine(message); }
}
