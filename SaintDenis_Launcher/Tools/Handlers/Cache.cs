using System.IO;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class Cache
    {
        public readonly static string[] ToRemove =
        [
            App.Local + @"/RedM/RedM.app/data/cache",
            App.Local + @"/RedM/RedM.app/data/server-cache",
            App.Local + @"/RedM/RedM.app/data/server-cache-priv",
        ];



        /// <summary>
        /// Move File Asynchronously
        /// </summary>
        /// <exception cref="IOException"></exception>
        public static async Task Clear()
        {
            List<Task> tasks = [];

            foreach (string path in ToRemove)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
