namespace Mueller.Bot.Logic;

public static class GameClearer
{
    public static void RunClearer()
    {
        while (true)
        {
            Thread.Sleep(TimeSpan.FromMinutes(1));
            MinimalLogger.Log("Filter games...");
            Game.CurrentGames = Game.CurrentGames.FilterStillRunning();
        }
    }
}
