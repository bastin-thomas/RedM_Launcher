using System.Diagnostics;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class ClearCache
    {
        public readonly static String ProcessName = "ClearCache.bat";
        public readonly static String ExecName = "ClearCache.bat";
        public readonly static String ProcessFolder = App.workingDirectoryPath + @"Resources";

        private static ProcessHandler _pr;

        /// <summary>
        /// All behaviors to Start EpicGame Properly Synchronously
        /// </summary>
        public static void Start() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder,"", false, ProcessWindowStyle.Hidden)).Start();

        /// <summary>
        /// All behaviors to Start EpicGame Properly Asynchronously
        /// </summary>
        public static void StartAsync() => (_pr = new ProcessHandler(ProcessName, ExecName, ProcessFolder, "", false, ProcessWindowStyle.Hidden)).StartAsync();

        public static void Wait() 
        {
            _pr.WaitProcessClosed();
        }
    }
}
