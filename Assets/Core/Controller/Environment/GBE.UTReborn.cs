using UnityEngine;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environment
{
    public static partial class GBE
    {
        public class UTReborn : IGBE
        {
            public const int serverPort = 8080;

            public string buildLabel() => $"R-Unity #{Application.version} (env: UT Reborn)";

            public string copyrightLabel() => "© R-Unity";

            public string serverAddress() => "54.39.227.169";

            public Protocol serverProtocol() => Protocol.HTTP;

            public string serverUrl() => $"{serverProtocol().ToString().ToLower()}://{serverAddress()}:{serverPort}";

            public string supportLink() => "https://google.com";
        }
    }
}
