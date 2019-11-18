using System.Collections.Generic;
using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Account
{
    public class StatsClassModel : XmlElement
    {
        public int bestFame;
        public int bestLevel;
        public uint objectType;

        public StatsClassModel(XElement elem) : base(XmlType.Node, elem)
        {
            objectType = getUint("objectType", 0, XmlType.Attribute);
            bestLevel = getInt("BestLevel", 1);
            bestFame = getInt("BestFame", 0);
        }

        public static Dictionary<uint, StatsClassModel> loadDict(XElement root)
        {
            Dictionary<uint, StatsClassModel> dict = new Dictionary<uint, StatsClassModel>();

            foreach (var elem in root.Elements("ClassStats"))
            {
                var stats = new StatsClassModel(elem);
                dict.Add(stats.objectType, stats);
            }

            return dict;
        }
    }
}
