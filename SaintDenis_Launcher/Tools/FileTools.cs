using Microsoft.Win32;
using System.IO;

#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
namespace SaintDenis_Launcher.Tools
{
    class FileTools
    {

        public static String DefaultRedMFolder
        {
            get {
                return App.Local + @"\RedM\";
            }
        }

        public static String DefaultSteamFolder
        {
            get
            {     
                try 
                {
                    RegistryKey SteamKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                    if (SteamKey is not null)
                    {
                        object value = SteamKey.GetValue("SteamPath");
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                        else
                        { return @"C:\Program Files (x86)\Steam\"; }
                    }
                    else 
                    { return @"C:\Program Files (x86)\Steam\"; }
                }
                catch(Exception ex) 
                {
                    Logger.LogError(ex);
                }
                return @"";
            }
        }

        public static String DefaultRockstarFolder
        {
            get
            {
                try
                {

                    RegistryKey SteamKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\Launcher");

                    if (SteamKey is not null)
                    {
                        object value = SteamKey.GetValue("InstallFolder");
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                        else
                        { return @"C:\Program Files\Rockstar Games\Launcher"; }
                    }
                    else 
                    { return @"C:\Program Files\Rockstar Games\Launcher"; }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return @"";
            }
        }

        public static String DefaultEpicFolder
        {
            get
            {
                 
                try
                {
                    RegistryKey SteamKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EpicGames\Unreal Engine");
                    if (SteamKey is not null)
                    {
                        object value = SteamKey.GetValue("INSTALLDIR");
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                        else { return @"C:\Program Files\EpicGame Games"; }
                    }
                    else { return @"C:\Program Files\EpicGame Games"; }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return @"";
            }
        }

        public static String DefaultTSFolder
        {
            get
            {
                try
                {
                    RegistryKey SteamKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\TeamSpeak 3 Client");
                    if (SteamKey is not null)
                    {
                        object value = SteamKey.GetValue("");
                        if (value is not null && value is string StringValue)
                        {
                            return StringValue.ToUpper().Replace("/", @"\");
                        }
                        else { return @"C:\Program Files\TeamSpeak"; }
                    }
                    else { return @"C:\Program Files\TeamSpeak"; }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
                return @"";
            }
        }






        public static String GetFolder(String Name, String currentFolder)
        {
            Logger.Information("OldPath: " + currentFolder);

            OpenFolderDialog folderDialog;
            folderDialog = new OpenFolderDialog();
            folderDialog.InitialDirectory = currentFolder;
            folderDialog.Title = Name;

            if (folderDialog.ShowDialog() == true)
            {
                Logger.Information("NewPath: " + folderDialog);
                return folderDialog.FolderName;
            }
            else {
                return currentFolder;
            }
        }

        public static String GetFolderFromFile(String Name, String currentFile, String filter)
        {
            Logger.Information("OldPath:" + currentFile);

            OpenFileDialog folderDialog;
            folderDialog = new OpenFileDialog();
            folderDialog.InitialDirectory = currentFile;
            folderDialog.Title = Name;
            folderDialog.Filter = filter + "|Exec|*.exe";
            folderDialog.FilterIndex = 0;
            folderDialog.DereferenceLinks = true;

            if (folderDialog.ShowDialog() == true)
            {
                Logger.Information("NewPath:" + folderDialog);

                return Path.GetDirectoryName(folderDialog.FileName);

            }
            else
            {
                return currentFile;
            }
        }
    }
}
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.