using SaintDenis_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class Teamspeak
    {
        public readonly static String ProcessName = "ts3client_win64";
        public readonly static String ExecName =    "ts3client_win64.exe";
        public readonly static String Arguments = $"ts3server://{Settings.Default.TSServerIP}?password={Settings.Default.TSPassword}";
        public readonly static String ProcessFolder = Settings.Default.TeamSpeakFolder;

        private static ProcessHandler _pr;

        /// <summary>
        /// All behaviors to Start Teamspeak Properly Synchronously
        /// </summary>
        public static void Start(bool asArguments)
        {
            IPAddress? addr;
            bool ValidateIP = IPAddress.TryParse(Settings.Default.TSServerIP, out addr);

            if (asArguments && ValidateIP)
            {
                (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder, Arguments)).Start();
            }
            else
            {
                (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder, "")).Start();
            }
        }

        /// <summary>
        /// All behaviors to Start Teamspeak Properly Asynchronously
        /// </summary>
        /// 
        public static void StartAsync(bool asArguments)
        {
            if (asArguments)
            {
                (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder, Arguments)).StartAsync();
            }
            else
            {
                (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder, "")).StartAsync();
            }
        }

        /// <summary>
        /// Wait that Teamspeak is Initialized
        /// </summary>
        public static void WaitTeamspeakInitialized() => _pr?.WaitProcessInitialized();
    }
}
