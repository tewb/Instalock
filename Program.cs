using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using ValAPINet;
using Agent = Instalock.Agents.Agent;
using Console = Colorful.Console;

namespace Instalock
{
    public enum Mode
    {
        Normal = 1,
        Random = 2,
        MapSpecific = 3,
    }

    public class Program
    {
        private static Auth auth;
        private static Mode mode;
        private static IConfiguration config;
        private static List<string> matches = new();
        private static Agent agent;
        private static bool paused;

        private static void SetAgent(PregameGetMatch match = null)
        {
            var owned = Agents.GetOwnedAgents(auth);

            switch (mode)
            {
                case Mode.Random:
                    agent.UUID = owned[random.Next(0, owned.Count)];
                    agent.Name = Agents.GetNameFromUUID(agent.UUID).ToUpper();
                    break;

                case Mode.MapSpecific:
                    if (match == null) return;
                    var id = match.MapID.Split('/').Last();
                    var map = Maps.GetNameFromID(id);
                    agent.Name = config[$"maps:{map}"].ToUpper();
                    agent.UUID = Agents.GetUUIDFromName(agent.Name);
                    break;

                default:
                    //if (agent.UUID != null) return;
                    Logging.Input("Which agent do you want to instalock?");
                    agent.Name = Console.ReadLine().ToUpper();
                    agent.UUID = Agents.GetUUIDFromName(agent.Name);
                    break;
            }

            if (!owned.Contains(agent.UUID))
            {
                Logging.Log($"You do not have the agent {agent.Name} unlocked", "#f03a3a");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static Random random = new();

        private static void Main(string[] args)
        {
            Console.Title = "INSTALOCK v1.0 | github @tewb";
            Console.ForegroundColor = ColorTranslator.FromHtml("#fbff2b");
            Console.CursorVisible = false;
            Logging.PrintLogo();

            try
            {
                config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            }
            catch (Exception e)
            {
                Logging.Log("Error occurred while reading config", "#f03a3a");
                Logging.Log("Please check appSettings.json for any errors", "#f03a3a");
                Console.ReadKey();
                Environment.Exit(0);
            }

            try
            {
                if (bool.Parse(config["launch_valorant_if_not_open"]))
                {
                    Enum.TryParse(config["region"], out Region region);
                    auth = Websocket.StartAndGetAuthLocal(region);
                }
                else
                {
                    auth = Websocket.GetAuthLocal(true);
                }

                var username = Username.GetUsername(auth);
                Console.Title = $"INSTALOCK v1.0 | {username.GameName}#{username.TagLine} | github @tewb";
            }
            catch { }

            if (auth.subject == null)
            {
                Logging.Log("Error occurred while authenticating", "#f03a3a");
                Logging.Log("Please make sure you have VALORANT open", "#f03a3a");
                Console.ReadKey();
                Environment.Exit(0);
            }

            Logging.Log("Instalock mode");
            Logging.Log("[1] Normal");
            Logging.Log("[2] Random");
            Logging.Log("[3] Map specific");
            Logging.Input();

            var interval = int.Parse(config["check_interval"]);
            var delay = int.Parse(config["static_delay"]);
            mode = (Mode)int.Parse(Console.ReadLine());

            Console.Clear();
            Logging.PrintLogo();
            SetAgent();

            while (true)
            {
                if (paused) Console.ReadKey();
                Console.Clear();
                Logging.PrintLogo();
                Logging.Log($"Waiting for you to start a match... [{mode}]");

                PregameGetMatch match;
                PregameGetPlayer pregame;
                do
                {
                    pregame = PregameGetPlayer.GetPlayer(auth);
                    match = PregameGetMatch.GetMatch(auth, pregame.MatchID);
                    Thread.Sleep(interval);
                }
                while (pregame.Subject == null && match.ID == null);

                if (mode == Mode.MapSpecific) SetAgent(match);
                if (!string.IsNullOrEmpty(pregame.MatchID) && !matches.Contains(pregame.MatchID))
                {
                    var player = match.AllyTeam.Players.Find(x => x.Subject == auth.subject);
                    if (player.CharacterSelectionState == "locked")
                    {
                        var name = Agents.GetNameFromUUID(player.CharacterID);
                        Logging.Log($"You are already locked in as {name}");
                        Logging.Log("Press any key to resume waiting for matches...");
                        Console.ReadKey();
                        continue;
                    }

                    var sw = Stopwatch.StartNew();
                    SelectAgent.LockAgent(auth, pregame.MatchID, agent.UUID);
                    sw.Stop();

                    match = PregameGetMatch.GetMatch(auth, pregame.MatchID);
                    player = match.AllyTeam.Players.Find(x => x.Subject == auth.subject);

                    var lockTime = sw.ElapsedMilliseconds;
                    if (player.CharacterSelectionState == "locked")
                    {
                        matches.Add(pregame.MatchID);
                        Logging.Log($"Successfully instalocked {agent.Name} in {lockTime}ms");
                    }
                    else
                    {
                        Logging.Log($"Failed to instalock {agent.Name} in {lockTime}ms");
                    }

                    if (bool.Parse(config["pause_program_after_lock_in"]))
                    {
                        Logging.Log("Press any key to resume waiting for matches...");
                        paused = true;
                    }

                    Thread.Sleep(delay);
                }
                else
                {
                    Thread.Sleep(interval);
                }
            }
        }
    }
}