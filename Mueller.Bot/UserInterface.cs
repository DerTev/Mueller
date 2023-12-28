using Discord;
using Discord.WebSocket;

namespace Mueller.Bot;

public static class UserInterface
{
    private static Embed BuildSimpleEmbed(string title, string description, Color color)
    {
        var embedBuilder = new EmbedBuilder();

        embedBuilder.WithTitle(title);
        embedBuilder.WithDescription(description);
        embedBuilder.WithColor(color);

        return embedBuilder.Build();
    }

    public static Embed Error(string errorMessage) => BuildSimpleEmbed("Error", errorMessage, Color.Red);

    public static Embed GameRequest(SocketGuildUser playerOne, SocketGuildUser playerTwo)
        => BuildSimpleEmbed("Game Request",
            $"{playerOne.Mention} challenged {playerTwo.Mention} to a round of "
            + "[Mühle](https://de.wikipedia.org/wiki/M%C3%BChle_(Spiel)).\n\n"
            + $"{playerTwo.Mention} do you accept?",
            Color.Green);
}
