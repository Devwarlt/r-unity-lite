using UnityEngine;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environment
{
    public static partial class GBE
    {
        public class ShotsRealm : IGBE
        {
            public string buildLabel() => $"R-Unity #{Application.version} (env: Shot's Realm)";

            public string copyrightLabel() => "© R-Unity";

            public string serverAddress() => "play.shotsrealm.com";

            public Protocol serverProtocol() => Protocol.HTTP;

            public string serverUrl() => $"{serverProtocol().ToString().ToLower()}://{serverAddress()}";

            public string supportLink() => "https://shotsrealm.com";
        }
    }
}
