using Discord.Rest;
using Discord.WebSocket;

namespace Mueller.Bot.Logic;

public class Game
{
    public static List<Game> CurrentGames = [];

    public readonly RestInteractionMessage FirstInteraction;
    public readonly SocketGuildUser PlayerOne;
    public readonly SocketGuildUser PlayerTwo;
    public GamePhase GamePhase;

    public bool IsStillRunning() => (GamePhase != GamePhase.Pending ||
                                     FirstInteraction.Timestamp.CompareToNow() < TimeSpan.FromMinutes(5)) &&
                                    GamePhase != GamePhase.Ended;

    public Game(RestInteractionMessage firstInteraction, SocketGuildUser playerOne, SocketGuildUser playerTwo)
    {
        FirstInteraction = firstInteraction;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        GamePhase = GamePhase.Pending;
    }
}
