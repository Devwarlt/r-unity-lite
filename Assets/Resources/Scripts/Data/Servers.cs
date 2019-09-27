using Assets.Resources.Scripts.util.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public static class Servers
{
    public delegate void ServersEventHandler();
    public static ServersEventHandler onServerChange;

    public static List<ServerData> servers;
    public static ServerData selected;
    public static int selectedId;

    public static void load(XElement root)
    {
        servers = new List<ServerData>();
        var serverId = 0;
        foreach (var elem in root.Elements("Server"))
        {
            servers.Add(new ServerData(elem, serverId));
            serverId++;
        }
        setSelectedServer(sendEvent: false);
    }

    public static void setSelectedServer(int server = 0, bool sendEvent = true)
    {
        if (server >= servers.Count())
            server = 0;
        selected = servers[server];
        selectedId = server;

        if (sendEvent)
            onServerChange();
    }
}

public class ServerData : XElem
{
    public string name;
    public string dns;
    public int port;
    public double _lat;
    public double _long;
    public double usage;
    public bool adminonly;
    public int id;

    public ServerData(XElement elem, int id) : base(XElemType.node, elem)
    {
        this.id = id;
        name = getString("Name");
        dns = getString("DNS");
        port = getInt("Port", 2050);
        _lat = getDouble("Lat", 0);
        _long = getDouble("Long", 0);
        usage = getDouble("Usage", 0);
        adminonly = getBool("AdminOnly", false);
    }

    /* THIS CONSTRUCTOR IS USED FOR TESTING */
    public ServerData(string name, double usage, int id) : base(XElemType.node, null)
    {
        this.id = id;
        this.name = name;
        this.usage = usage;
    }
}