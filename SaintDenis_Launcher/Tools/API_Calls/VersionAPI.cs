using System.Net;
using System.Reflection;
using System.Xml;

namespace SaintDenis_Launcher.Tools.API_Calls
{
    internal class VersionAPI
    {
        public static bool NeedUpdate
        {
            get
            {
                try
                {
                    if (LastRealesedVersion.Equals("unknown") || CurrentRealesedVersion.Equals(LastRealesedVersion)) return false;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    return false;
                }

                return true;
            }
        }

        public static string CurrentRealesedVersion = "unknown";

        public static string LastRealesedVersion = "unkown";


        private static void InitAPI()
        {
            try
            {
                LastRealesedVersion = GetVersionFromGitHub();

                Version version = Assembly.GetExecutingAssembly().GetName().Version!;
                CurrentRealesedVersion = version.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }


        /// <summary>
        /// Get From Web the current Version On Github
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetVersionFromGitHub(string url = @"https://raw.githubusercontent.com/bastin-thomas/SaintDenis_Launcher/realese/SaintDenis_Launcher/SaintDenis_Launcher.csproj")
        {
            WebClient client = new();
            string data = client.DownloadString(url);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            XmlElement RootNode = doc.DocumentElement!;
            XmlNodeList nodes = RootNode.SelectNodes("//Project/PropertyGroup/AssemblyVersion")!;

            foreach (XmlNode node in nodes)
            {
                if (node.Name == "AssemblyVersion")
                {
                    return node.InnerXml!;
                }
            }

            return "unkown";
        }

        internal static void Setup()
        {
            InitAPI();
        }
    }
}
