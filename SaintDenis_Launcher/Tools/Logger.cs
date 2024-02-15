using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools
{
    public static class Logger
    {
        private static TextWriterTraceListener fileListener = new TextWriterTraceListener(App.workingDirectoryPath + "log.txt");
        public static void Setup()
        {
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
