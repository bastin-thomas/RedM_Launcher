using SaintDenis_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools.Handlers
{
    /// <summary>
    /// Handle a Process to Start It (Sync or Async)
    /// </summary>
    internal class ProcessHandler
    {
        private String _processName;
        private String _execName;
        private String _folder;
        private String _parameters;
        private Process? _process;

        public ProcessHandler(string processName, string execName, string folder, string parameters = "")
        {
            _processName = processName;
            _execName = execName;
            _folder = folder;
            _parameters = parameters;
        }

        /// <summary>
        /// All behaviors to Start Process Properly
        /// </summary>
        public void Start()
        {
            Logger.Information($"{_processName} Is Running: {IsRedMRunning}");

            // If Process Is Opened and Ready, skip
            if (IsRedMRunning)
            {
                Logger.Information($"{_processName} Already Launched Skip Phase");
            }
            // If Process Is Not Open, Launch it
            else
            {
                Logger.Information($"Launching {_processName}");
                _process = Process.Start(_folder + @"\" + _execName, _parameters);
                WaitProcessInitialized();
            }
        }

        /// <summary>
        /// All behaviors to Start Process Properly But never wait Process Initialisation Finish
        /// </summary>
        public void StartAsync()
        {
            Logger.Information($"{_processName} Is Running: {IsRedMRunning}");

            // If Process Is Opened and Ready, skip
            if (IsRedMRunning)
            {
                Logger.Information($"{_processName} Already Launched Skip Phase");
            }
            // If Process Is Not Open, Launch it
            else
            {
                Logger.Information($"Launching {_processName}");
                _process = Process.Start(_folder + @"\" + _execName, _parameters);
            }
        }

        /// <summary>
        /// Wait that Process is Initialized
        /// </summary>
        public void WaitProcessInitialized()
        {
            Logger.Information($"Waiting {_processName} Initialisation");

            Process[] list = Process.GetProcessesByName(_processName);
            if ((list.Length > 0))
            {
                _process = list[0];
            }

            if ( _process != null ) 
            {
                _process.WaitForInputIdle();
                Logger.Information($"{_processName} Is Initialized");
                return;
            }
            Logger.Information($"{_processName} never been launched. Skipped");
        }


        /// <summary>
        /// Check that Process is Already Running or Not
        /// </summary>
        private bool IsRedMRunning
        {
            get
            {
                Process[] list = Process.GetProcessesByName(_processName);
                return (list.Length > 0);
            }
        }
    }
}
