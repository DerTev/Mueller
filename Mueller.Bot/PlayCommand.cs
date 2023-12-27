using Discord;
using Discord.WebSocket;

namespace Mueller.Bot;

public static class PlayCommand
{
    public static async Task RunCommand(SocketSlashCommand command)
    {
        if (command.Data.Name != "play") return;
        MinimalLogger.Log("Run play-Command...");

        await command.RespondAsync("Hello, world!", ephemeral: true);
    }

    public static SlashCommandProperties BuildCommand()
    {
        var slashCommand = new SlashCommandBuilder();
        
        slashCommand.WithName("play");
        slashCommand.WithDescription("Play Mühle with your friends!");
        
        return slashCommand.Build();
    }
}
