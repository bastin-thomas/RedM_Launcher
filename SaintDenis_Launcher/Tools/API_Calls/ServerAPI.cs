using SaintDenis_Launcher.Properties;
using System.Net;
using System.Text.Json.Nodes;

namespace SaintDenis_Launcher.Tools.API_Calls
{
    internal class ServerAPI
    {
        public static string Ip { get { return Settings.Default.RedmServerIP; } }

        public static bool IsOnline
        {
            get
            {
                try
                {
                    return GetStatusFromWeb($@"http://{Ip}:30120/info.json");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
            }
        }

        public static int ConnectedPlayers
        {
            get
            {
                try
                {
                    return CountConnectedPlayers($@"http://{Ip}:30120/players.json");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    return 0;
                }
            }
        }

        public static int MaxPlayers
        {
            get
            {
                try
                {
                    return GetMaxPlayers($@"http://{Ip}:30120/info.json");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Try Get a File to Test if Server is Online
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool GetStatusFromWeb(string url = @"http://127.0.0.1:30120/RootNode.json")
        {
            WebClient client = new();
            _ = client.DownloadString(url);
            return true;
        }


        /// <summary>
        /// Get the current number of connected RootNode on the server using RootNode.json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static int CountConnectedPlayers(string url = @"http://127.0.0.1:30120/RootNode.json")
        {
            WebClient client = new();
            string data = client.DownloadString(url);
            JsonArray players = JsonNode.Parse(data)!.AsArray();
            return players!.Count;
        }



        private static int GetMaxPlayers(string url = @"http://127.0.0.1:30120/RootNode.json")
        {
            WebClient client = new();
            string data = client.DownloadString(url);
            JsonNode RootNode = JsonNode.Parse(data)!;

            return int.Parse(RootNode["vars"]!["sv_maxClients"]!.GetValue<string>());
        }
    }
}
