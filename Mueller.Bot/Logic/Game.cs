using Discord.Rest;
using Discord.WebSocket;

namespace Mueller.Bot.Logic;

public class Game
{
    public static List<Game> CurrentGames = [];

    public static Game? GetByFirstResponse(ulong firstResponseId) =>
        CurrentGames.Where(game => game.FirstResponse.Id == firstResponseId).FirstOrDefault(defaultValue: null);

    public readonly RestInteractionMessage FirstResponse;
    public readonly SocketGuildUser PlayerOne;
    public readonly SocketGuildUser PlayerTwo;
    public GamePhase GamePhase;

    public bool IsStillRunning() => (GamePhase != GamePhase.Pending ||
                                     FirstResponse.Timestamp.CompareToNow() < TimeSpan.FromMinutes(5)) &&
                                    GamePhase != GamePhase.Ended &&
                                    FirstResponse.Timestamp.CompareToNow() < TimeSpan.FromMinutes(30);

    public override string ToString() =>
        $"Game between \"{PlayerOne.DisplayName}\" and \"{PlayerTwo.DisplayName}\", currently in Phase {GamePhase}.";

    public Game(RestInteractionMessage firstResponse, SocketGuildUser playerOne, SocketGuildUser playerTwo)
    {
        FirstResponse = firstResponse;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        GamePhase = GamePhase.Pending;
    }
}
