using SaintDenis_Launcher.Properties;
using Steamworks;
using System.Diagnostics;

namespace SaintDenis_Launcher.Tools.Handlers
{
    public static class Steam
    {
        public static String ProcessName = "Steam";
        public static String ExecName = "steam.exe";

        /// <summary>
        /// All behaviors to Start Steam Properly
        /// </summary>
        public static void Start()
        {
            Logger.Information("Steam Is Running: " + IsRunning);
            Logger.Information("Steam Is Initialized: " + IsInitialized);

            // If Steam Is Opened and Ready, skip
            if (IsRunning && IsInitialized)
            {
                Logger.Information("Steam Already Launched Skip Phase");
            }
            // If Steam Is Opened and not Ready, Wait it is ready
            else if (IsRunning && !IsInitialized)
            {
                WaitSteamInitialized();
            }
            // If Steam Is Not Open, Launch it
            else
            {
                Logger.Information("Launching Steam");
                Process Steam = Process.Start(Settings.Default.SteamFolder + @"\" + ExecName, "-silent");
                WaitSteamInitialized();
            }

            ShutDownAPI();
        }

        /// <summary>
        /// Wait that Steam is Initialized
        /// </summary>
        private static void WaitSteamInitialized()
        {
            Logger.Information("Waiting Initialisation");
            while (!IsInitialized)
            {
                Thread.Sleep(500);
            }
            Logger.Information("Steam Is Initialized");
        }

        /// <summary>
        /// Shutdown SteamWorksAPI
        /// </summary>
        private static void ShutDownAPI()
        {
            SteamAPI.Shutdown();
            Logger.Information("SteamAPI ShutingDown");
        }

        /// <summary>
        /// Check if Steam Process Running
        /// </summary>
        public static bool IsRunning
        {
            get
            {
                return SteamAPI.IsSteamRunning();
            }
        }

        /// <summary>
        /// Check if Steam is Initialized (ready to Open RedM)
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return SteamAPI.Init(); ;
            }
        }
    }
}
