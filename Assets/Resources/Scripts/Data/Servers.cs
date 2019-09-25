using Assets.Resources.Scripts.util.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public static class Servers
{
    public static List<ServerData> servers;

    public static void load(XElement root)
    {
        servers = new List<ServerData>();
        foreach (var elem in root.Elements("Server"))
            servers.Add(new ServerData(elem));
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

    public ServerData(XElement elem) : base(XElemType.node, elem)
    {
        name = getString("Name");
        dns = getString("DNS");
        port = getInt("Port", 2050);
        _lat = getDouble("Lat", 0);
        _long = getDouble("Long", 0);
        usage = getDouble("Usage", 0);
        adminonly = getBool("AdminOnly", false);
    }
}