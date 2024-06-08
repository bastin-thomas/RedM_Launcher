using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedM_Launcher.Tools
{
    /// <summary>
    /// A Class to Handle Single instance of the program.
    /// </summary>
    internal class InstanceChecker
    {
        private static Mutex m_mutex = new(false, "Global\\Redm_Launcher_Instance_Checker");


        public static void MutexRealese()
        {
            m_mutex.ReleaseMutex();
        }


        public static bool IsAlreadyLaunched() 
        {
            try
            {
                return !m_mutex.WaitOne(1, true);
            }
            catch (AbandonedMutexException)
            {
                return false;
            }
        }
    }
}
