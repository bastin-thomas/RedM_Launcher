using SaintDenis_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class Rockstar
    {
        public readonly static String ProcessName = "Launcher";
        public readonly static String ExecName = "LauncherPatcher.exe";
        public readonly static String ProcessFolder = Settings.Default.RockstarFolder;

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
