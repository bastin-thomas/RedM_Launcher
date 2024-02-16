using SaintDenis_Launcher.Properties;


namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class EpicGame
    {
        public readonly static String ProcessName = "EpicGamesLauncher";
        public readonly static String ExecName = "EpicGamesLauncher.exe";
        public readonly static String ProcessFolder = Settings.Default.EpicFolder + @"\Launcher\Portal\Binaries\Win64";

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
