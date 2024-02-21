using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools.API_Calls
{
    internal class ServerAPI
    {
        public static bool IsOnline
        {
            get
            {
                try
                {
                    return AnalyseJson(GetDataFromWeb());
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// Get The Data From WebPage
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetDataFromWeb(string url = @"https://status.cfx.re/api/v2/status.json")
        {
            WebClient client = new();
            return client.DownloadString(url);
        }
        private static bool AnalyseJson(string json)
        {
            JsonNode rootNode = JsonNode.Parse(json)!;
            JsonNode status = rootNode["status"]!;

            if (status!["indicator"]!.Equals("none"))
            {
                return true;
            }

            return false;
        }
    }
}
