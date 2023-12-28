namespace Mueller.Bot.Logic;

public static class GameClearer
{
    public static void RunClearer()
    {
        while (true)
        {
            Thread.Sleep(TimeSpan.FromMinutes(1));
            MinimalLogger.Log("Filter games...");
            MinimalLogger.Log("Games before clear: " + Game.CurrentGames.Count);
            Game.CurrentGames = Game.CurrentGames.FilterStillRunning();
            MinimalLogger.Log("Games after clear: " + Game.CurrentGames.Count);
        }
    }
}
