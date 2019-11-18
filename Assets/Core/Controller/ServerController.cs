using Assets.Core.Model.Server;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Assets.Core.Controller
{
    public static partial class ServerController
    {
        public static ServersEventHandler onServerChange;

        public static ServerModel selected;

        public static int selectedId;

        public static List<ServerModel> servers;

        public delegate void ServersEventHandler();

        public static void load(XElement root)
        {
            servers = new List<ServerModel>();

            var serverId = 0;

            foreach (var elem in root.Elements("Server"))
            {
                servers.Add(new ServerModel(elem, serverId));
                serverId++;
            }

            setSelectedServer(sendEvent: false);
        }

        public static void setSelectedServer(int server = 0, bool sendEvent = true)
        {
            if (server >= servers.Count()) server = 0;

            selected = servers[server];
            selectedId = server;

            if (sendEvent) onServerChange();
        }
    }
}
