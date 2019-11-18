using System.Collections.Generic;
using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Account
{
    public class StatsAccountModel : XmlElement
    {
        public int bestCharFame;
        public Dictionary<uint, StatsClassModel> classStats;
        public int fame;
        public int totalFame;

        public StatsAccountModel(XElement elem) : base(XmlType.Node, elem)
        {
            //classStats = ClassStatsData.loadDict(elem); /* GIVES AN ERROR TRYING TO GET OBJECTTYPE */
            bestCharFame = getInt("BestCharFame", 0);
            totalFame = getInt("TotalFame", 0);
            fame = getInt("Fame", 0);
        }
    }
}
