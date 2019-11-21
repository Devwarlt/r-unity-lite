using UnityEngine;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environments
{
    public static partial class GBE
    {
        public struct Environment
        {
            private const string copyright = "©";

            private readonly string ipAddress;
            private readonly string name;
            private readonly Protocol protocol;
            private readonly string supportLink;
            private readonly int webServerPort;

            public Environment(string ipAddress, string name, Protocol protocol,
                string supportLink, int webServerPort)
            {
                this.ipAddress = ipAddress;
                this.name = name;
                this.protocol = protocol;
                this.supportLink = supportLink;
                this.webServerPort = webServerPort;
            }

            public string GetCopyright() =>
                string.Format("{0} {1}",
                    copyright, Application.productName);

            public string GetName() =>
                name;

            public string GetIpAddress() =>
                ipAddress;

            public Protocol GetProtocol() =>
                protocol;

            public string GetSupportLink() =>
                supportLink;

            public string GetUriString() =>
                string.Format("{0}://{1}{2}",
                    protocol.ToString().ToLower(), ipAddress, webServerPort == 80 ? "" : $":{webServerPort}");
        }
    }
}
