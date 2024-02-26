using System.Diagnostics;
using System.IO;

namespace SaintDenis_Launcher.Tools
{
    public static class Logger
    {
        private static TextWriterTraceListener fileListener;
        public static void Setup()
        {
            if (!Directory.Exists(App.workingDirectoryPath + $"/Logs/"))
            {
                Directory.CreateDirectory(App.workingDirectoryPath + $"/Logs/");
            }

            fileListener = new(App.workingDirectoryPath + $"/Logs/MainLog-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.txt");

            Trace.Listeners.Add(fileListener);
            Trace.WriteLine("\n");
        }

        public static void Information(string message)
        {
            Trace.TraceInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}");
            Trace.Flush();
        }

        public static void Warning(string message)
        {
            Trace.TraceWarning($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [WARN] {message}");
            Trace.Flush();
        }

        public static void Error(string? message)
        {
            if (message == null)
            {
                return;
            }

            Trace.TraceWarning($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}");
            Trace.Flush();
        }

        public static void LogError(Exception exception)
        {
            Trace.TraceError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {exception.Message}\n{exception}");
            Trace.Flush();
        }
    }
}
