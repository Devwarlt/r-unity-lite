using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Server
{
    public class ServerModel : XmlElement
    {
        public double _lat;
        public double _long;
        public bool adminonly;
        public string dns;
        public int id;
        public string name;
        public int port;
        public double usage;

        public ServerModel(XElement elem, int id) : base(XmlType.Node, elem)
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
    }
}
