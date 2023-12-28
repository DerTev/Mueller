using Discord;
using Discord.WebSocket;
using Mueller.Bot.Logic;

namespace Mueller.Bot;

public class DiscordBot
{
    private readonly DiscordSocketClient _client = new();
    private readonly string _token;
    private readonly IDictionary<string, string> _env;

    private async Task OnReady()
    {
        MinimalLogger.Log("Bot is ready.");
        
        MinimalLogger.Log("Run Clearer...");
        new Task(GameClearer.RunClearer).Start();
        
        //TODO run the CreateGlobalApplicationCommandAsync() once for each command.
        //TODO maybe just write the latest used version to file and only register und update
        MinimalLogger.Log("Register Slash-Commands...");
        try
        {
            var playCommand = PlayCommand.BuildCommand();
            
            MinimalLogger.Log("Guild-Only-Mode activated: " + _env.ContainsKey("TESTING_GUILD"));
            if (_env.ContainsKey("TESTING_GUILD"))
            {
                var guild = _client.GetGuild(ulong.Parse(_env["TESTING_GUILD"]));
                await guild.CreateApplicationCommandAsync(playCommand);
            }
            else await _client.CreateGlobalApplicationCommandAsync(playCommand);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error while creating slash command: " + exception);
        }

        _client.SlashCommandExecuted += PlayCommand.RunCommand;
    }
    
    public async Task StartBotAsync()
    {
        MinimalLogger.Log("Running the start-function now...");
        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
        
        MinimalLogger.Log("Run infinitely delay...");
        await Task.Delay(-1);
    }

    public void StartBot() => StartBotAsync().GetAwaiter().GetResult();

    public DiscordBot(IDictionary<string, string> env)
    {
        _env = env;
        _token = _env["BOT_TOKEN"];
        _client.Ready += OnReady;
    }
}
