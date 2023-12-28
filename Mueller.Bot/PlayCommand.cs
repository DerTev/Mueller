using Discord;
using Discord.WebSocket;
using Mueller.Bot.Logic;
using Game = Mueller.Bot.Logic.Game;

namespace Mueller.Bot;

public static class PlayCommand
{
    public static DiscordSocketClient? SocketClient;

    public static async Task RunCommand(SocketSlashCommand command)
    {
        if (command.Data.Name != "play") return;
        MinimalLogger.Log("Run play-Command...");

        var opponent = (SocketGuildUser)command.Data.Options.First().Value;

        if (opponent.IsBot || opponent.Id == command.User.Id)
        {
            await command.RespondAsync(embeds: [UserInterface.Error("Invalid opponent!")], ephemeral: true);
            return;
        }

        var guildUser = SocketClient!.GetGuild(command.GuildId!.Value).GetUser(command.User.Id);
        await command.RespondAsync(opponent.Mention, embeds: [UserInterface.GameRequest(guildUser, opponent)]);
        var thisGame = new Game(await command.GetOriginalResponseAsync(), guildUser, opponent);
        Game.CurrentGames.Add(thisGame);

        var originalResponse = await command.GetOriginalResponseAsync();
        await originalResponse.ModifyAsync(properties => properties.Components = new ComponentBuilder()
            .WithButton("Accept", "accept-game", ButtonStyle.Success)
            .Build());
    }

    public static async Task HandleComponent(SocketMessageComponent component)
    {
        if (component.Data.CustomId != "accept-game") return;
        MinimalLogger.Log("Handle component...");

        var game = Game.GetByFirstResponse(component.Message.Id);
        if (game == null || !game.IsStillRunning())
        {
            await component.RespondAsync(embeds: [UserInterface.Error("The game is already expired!")],
                ephemeral: true);
            return;
        }

        if (game.GamePhase != GamePhase.Pending)
        {
            await component.RespondAsync(embeds: [UserInterface.Error("The game is already accepted!")],
                ephemeral: true);
            return;
        }

        if (game.PlayerTwo.Id != component.User.Id)
        {
            await component.RespondAsync(
                embeds: [UserInterface.Error("Only the requested user is able to accept the game!")], ephemeral: true);
            return;
        }

        game.GamePhase = GamePhase.Placing;
        await component.Message.ModifyAsync(properties =>
        {
            properties.Content = null;
            properties.Embed = UserInterface.Error("Not implemented yet!\nState of game: " + game);
        });
        await component.RespondAsync();
    }

    public static SlashCommandProperties BuildCommand()
    {
        var slashCommand = new SlashCommandBuilder();

        slashCommand.WithName("play");
        slashCommand.WithDescription("Play Mühle with your friends!");
        slashCommand.AddOption("opponent", ApplicationCommandOptionType.User,
            "Your opponent", isRequired: true);

        return slashCommand.Build();
    }
}
