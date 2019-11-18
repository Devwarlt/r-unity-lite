using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Core.Model.Server
{
    public class CharListModel
    {
        public static Dictionary<int, CharModel> chars;
        public static CharDataModel data;

        public static void load(XElement root)
        {
            data = new CharDataModel(root);
            chars = new Dictionary<int, CharModel>();

            foreach (var elem in root.Elements("Char"))
            {
                var charData = new CharModel(elem);
                chars.Add(charData.id, charData);
            }
        }
    }
}
