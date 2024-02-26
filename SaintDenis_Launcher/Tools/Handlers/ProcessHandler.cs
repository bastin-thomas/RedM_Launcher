using System.Diagnostics;

namespace SaintDenis_Launcher.Tools.Handlers
{
    /// <summary>
    /// Handle a Process to Start It (Sync or Async)
    /// </summary>
    internal class ProcessHandler
    {
        private readonly String _processName;
        private readonly String _execName;
        private readonly String _folder;
        private readonly String _parameters;
        private readonly bool _asAdmin;
        private readonly ProcessWindowStyle _style;
        private Process _process = new();
        public bool HasBeenSkipped = false;

        public ProcessHandler(string processName, string execName, string folder, string parameters = "", bool asAdmin = false, ProcessWindowStyle style = ProcessWindowStyle.Normal)
        {
            _processName = processName;
            _execName = execName;
            _folder = folder;
            _parameters = parameters;
            _asAdmin = asAdmin;
            _style = style;
        }

        /// <summary>
        /// All behaviors to Start Process Properly But never wait Process Initialisation Finish
        /// </summary>
        public void StartAsync()
        {
            Logger.Information($"{_processName} Is Running: {IsProcessRunning}");
            HasBeenSkipped = false;

            // If Process Is Opened and Ready, skip
            if (IsProcessRunning)
            {
                Logger.Information($"{_processName} Already Launched Skip Phase");
                HasBeenSkipped = true;
            }
            // If Process Is Not Open, Launch it
            else
            {
                Logger.Information($"Launching {_processName}");
                ProcessStartInfo psi = new()
                {
                    FileName = _folder + @"\" + _execName,
                    Arguments = _parameters,
                    UseShellExecute = true,
                    WindowStyle = _style,
                };
                if (_asAdmin) psi.Verb = "runas";

                _process.StartInfo = psi;
                _process.Start();
                Logger.Information($"{_process.ProcessName} Process has been Launched");
            }
        }

        /// <summary>
        /// All behaviors to Start Process Properly
        /// </summary>
        public void Start()
        {
            StartAsync();
            if (!_asAdmin) { WaitProcessInitialized(); }
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

            if (_process != null)
            {
                _process.WaitForInputIdle();
                Logger.Information($"{_processName} Is Initialized");
                return;
            }
            Logger.Information($"{_processName} never been launched. Skipped");
        }

        /// <summary>
        /// Waiting Process Closure
        /// </summary>
        internal void WaitProcessClosed()
        {
            _process.WaitForExit();
        }

        /// <summary>
        /// Waiting Asynchronously Process Closure
        /// </summary>
        /// <param name="action"></param>
        internal async Task ProcessEndCallBack(Action action)
        {
            if (_process == null) { throw new Exception("Process_Null"); }
            if (_process.HasExited) { throw new Exception("Already_Finished"); }

            await _process.WaitForExitAsync();
            action.Invoke();
        }

        /// <summary>
        /// Check that Process is Already Running or Not
        /// </summary>
        public bool IsProcessRunning
        {
            get
            {
                Process[] list = Process.GetProcessesByName(_processName);
                return (list.Length > 0);
            }
        }
    }
}
