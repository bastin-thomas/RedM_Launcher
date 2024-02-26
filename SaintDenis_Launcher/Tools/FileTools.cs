using Microsoft.Win32;
using System.IO;

namespace SaintDenis_Launcher.Tools
{
    class FileTools
    {
        private readonly struct Steam
        {
            public const String DefaultPath = @"C:\Program Files (x86)\Steam\";
            public const String KeyPath = @"Software\Valve\Steam";
            public const String KeyValue = @"SteamPath";
        }

        private readonly struct Rockstar
        {
            public const String DefaultPath = @"C:\Program Files\Rockstar Games\Launcher";
            public const String KeyPath = @"SOFTWARE\WOW6432Node\Rockstar Games\Launcher";
            public const String KeyValue = @"InstallFolder";
        }

        private readonly struct Epic
        {
            public const String DefaultPath = @"C:\Program Files\EpicGame Games";
            public const String KeyPath = @"SOFTWARE\WOW6432Node\EpicGames\Unreal Engine";
            public const String KeyValue = @"INSTALLDIR";
        }

        private readonly struct TeamSpeak
        {
            public const String DefaultPath = @"C:\Program Files\TeamSpeak";
            public const String KeyPath = @"SOFTWARE\TeamSpeak 3 Client";
            public const String KeyValue = @"";
        }


        /// <summary>
        /// Get The Default Folder, based on RegKey (if available).
        /// </summary>
        public static String DefaultRedMFolder
        {
            get
            {
                return App.Local + @"\RedM\";
            }
        }

        /// <summary>
        /// Get The Default Folder, based on RegKey (if available).
        /// </summary>
        public static String DefaultSteamFolder
        {
            get
            {
                try
                {
                    RegistryKey? SteamKey = Registry.CurrentUser.OpenSubKey(Steam.KeyPath);
                    if (SteamKey is not null)
                    {
                        object? value = SteamKey.GetValue(Steam.KeyValue);
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return Steam.DefaultPath;
            }
        }

        /// <summary>
        /// Get The Default Folder, based on RegKey (if available).
        /// </summary>
        public static String DefaultRockstarFolder
        {
            get
            {
                try
                {
                    RegistryKey? RockstarKey = Registry.LocalMachine.OpenSubKey(Rockstar.KeyPath);

                    if (RockstarKey is not null)
                    {
                        object? value = RockstarKey.GetValue(Rockstar.KeyValue);

                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }

                return Rockstar.DefaultPath;
            }
        }

        /// <summary>
        /// Get The Default Folder, based on RegKey (if available).
        /// </summary>
        public static String DefaultEpicFolder
        {
            get
            {
                try
                {
                    RegistryKey? EpicKey = Registry.LocalMachine.OpenSubKey(Epic.KeyPath);
                    if (EpicKey is not null)
                    {
                        object? value = EpicKey.GetValue(Epic.KeyValue);
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return Epic.DefaultPath;
            }
        }

        /// <summary>
        /// Get The Default Folder, based on RegKey (if available).
        /// </summary>
        public static String DefaultTSFolder
        {
            get
            {
                try
                {
                    RegistryKey? SteamKey = Registry.LocalMachine.OpenSubKey(TeamSpeak.KeyPath);
                    if (SteamKey is not null)
                    {
                        object? value = SteamKey.GetValue(TeamSpeak.KeyValue);
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return TeamSpeak.DefaultPath;
            }
        }





        /// <summary>
        /// Open a popup to get a Folder
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="currentFolder"></param>
        /// <returns></returns>
        public static String? GetFolder(String Name, String currentFolder)
        {
            Logger.Information("OldPath: " + currentFolder);

            OpenFolderDialog? dialog;
            dialog = new()
            {
                InitialDirectory = currentFolder,
                Title = Name
            };

            if (dialog is not null && dialog.ShowDialog() == true)
            {
                Logger.Information("NewPath: " + dialog);
                return dialog.FolderName;
            }
            else
            {
                return currentFolder;
            }
        }

        /// <summary>
        /// Open a popup to select a file that will return it's folder location
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="currentFile"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static String GetFolderFromFile(String Name, String currentFile, String filter)
        {
            Logger.Information("OldPath:" + currentFile);

            OpenFileDialog? dialog;
            dialog = new()
            {
                InitialDirectory = currentFile,
                Title = Name,
                Filter = filter + "|Exec|*.exe",
                FilterIndex = 0,
                DereferenceLinks = true
            };

            if (dialog is not null && dialog.ShowDialog() == true)
            {
                Logger.Information("NewPath:" + dialog);
                string? path = Path.GetDirectoryName(dialog.FileName);
                return path is not null ? path : "C:/";
            }
            else
            {
                return currentFile;
            }
        }
    }
}