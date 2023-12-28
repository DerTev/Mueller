namespace Mueller.Bot.Logic;

public static class Extensions
{
    public static TimeSpan CompareToNow(this DateTimeOffset time) => DateTimeOffset.Now - time;

    public static List<Game> FilterStillRunning(this List<Game> games) =>
        games.Where(game => game.IsStillRunning()).ToList();
}
