using SaintDenis_Launcher.Properties;


namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class EpicGame
    {
        public readonly static string ProcessName = "EpicGamesLauncher";
        public readonly static string ExecName = "EpicGamesLauncher.exe";
        public static string ProcessFolder { get { return Settings.Default.EpicFolder + @"\Launcher\Portal\Binaries\Win64"; } }

        public static bool IsLaunched
        {
            get
            {
                return _pr is not null ? _pr.IsProcessRunning : false;
            }
        }

        public static bool HasBeenSkipped
        {
            get
            {
                return _pr is not null ? _pr.HasBeenSkipped : false;
            }
        }

        private static ProcessHandler _pr;

        /// <summary>
        /// All behaviors to Start EpicGame Properly Synchronously
        /// </summary>
        public static void Start() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder)).Start();

        /// <summary>
        /// All behaviors to Start EpicGame Properly Asynchronously
        /// </summary>
        public static void StartAsync() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder)).StartAsync();

        /// <summary>
        /// Wait that EpicGame is Initialized
        /// </summary>
        public static void WaitEpicInitialized() => _pr?.WaitProcessInitialized();
    }
}
