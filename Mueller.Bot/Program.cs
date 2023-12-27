using System.Collections;
using dotenv.net;
using Mueller.Bot;

DotEnv.Load();
var env = DotEnv.Read();
foreach (DictionaryEntry environmentVariable in (Hashtable)Environment.GetEnvironmentVariables())
    env.Add((string)environmentVariable.Key, (string)environmentVariable.Value!);

if (!env.ContainsKey("BOT_TOKEN"))
{
    Console.WriteLine("Please provide the Bot Token via Env-Variables!");
    return;
}

MinimalLogger.Activated = env.ContainsKey("DEBUG_MODE") && env["DEBUG_MODE"] == "TRUE";

if (!env.ContainsKey("TESTING_GUILD"))
{
    Console.WriteLine("Please confirm Global-Mode by pressing enter!");
    Console.ReadLine();
}

MinimalLogger.Log("Init Discord-Bot...");
var discordBot = new DiscordBot(env);

MinimalLogger.Log("Start Discord-Bot...");
discordBot.StartBot();
