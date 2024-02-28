using System.Windows;
using System.Windows.Threading;

namespace RedM_Launcher.Model.Utils
{
    internal class CallbackTimer
    {
        public static void RunAfter(double seconds, Action action)
        {
            if (Application.Current.Dispatcher == null)
                return;
            var myTimer = new DispatcherTimer(
                DispatcherPriority.Normal,
                Application.Current.Dispatcher)
            {
                Interval = TimeSpan.FromSeconds(seconds)
            };
            myTimer.Tick += (_, _) =>
            {
                action?.Invoke();
                myTimer.Stop();
            };
            myTimer.Start();
        }
    }
}
