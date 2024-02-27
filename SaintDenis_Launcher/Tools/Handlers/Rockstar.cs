using SaintDenis_Launcher.Properties;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class Rockstar
    {
        public readonly static string ProcessName = "Launcher";
        public readonly static string ExecName = "LauncherPatcher.exe";
        public  static string ProcessFolder { get { return Settings.Default.RockstarFolder; } }

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
        /// All behaviors to Start Rockstar Properly Synchronously
        /// </summary>
        public static void Start() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder)).Start();

        /// <summary>
        /// All behaviors to Start Rockstar Properly Asynchronously
        /// </summary>
        public static void StartAsync() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder)).StartAsync();

        /// <summary>
        /// Wait that Rockstar is Initialized
        /// </summary>
        public static void WaitRockstarInitialized() => _pr?.WaitProcessInitialized();
    }
}
