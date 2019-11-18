using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Server
{
    public class CharDataModel : XmlElement
    {
        public int maxNumChars;
        public int nextCharid;

        public CharDataModel(XElement elem) : base(XmlType.Attribute, elem)
        {
            nextCharid = getInt("nextCharId", 0);
            maxNumChars = getInt("maxNumChars", 1);
        }
    }
}
