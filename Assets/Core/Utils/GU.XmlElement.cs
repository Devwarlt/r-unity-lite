using System;
using System.Globalization;
using System.Xml.Linq;

namespace Assets.Core.Utils
{
    public static partial class GU
    {
        public abstract partial class XmlElement
        {
            public XElement Element;
            public XmlType ElemType;

            public XmlElement(XmlType type, XElement elem)
            {
                ElemType = type;
                Element = elem;
            }

            public bool exists(string name, XmlType? type = null)
            {
                switch (type ?? ElemType)
                {
                    case XmlType.Attribute: return Element.Attribute(name) != null;
                    case XmlType.Node: return Element.Element(name) != null;
                }

                return false;
            }

            public string[] getArray(string name, string[] split, XmlType? type = null) =>
                !exists(name, type) ? null : getString(name, type: type).Split(split, StringSplitOptions.None);

            public bool getBool(string name, bool def, XmlType? type = null)
            {
                if (!exists(name, type)) return def;

                var str = getString(name, type: type);

                return !(str == "0" || str == "false" || str == "f");
            }

            public double getDouble(string name, double def, XmlType? type = null) =>
                !exists(name, type) || !double.TryParse(getString(name, type: type), out double ret)
                ? def : ret;

            public int getInt(string name, int def, XmlType? type = null) =>
                !exists(name, type) || !int.TryParse(getString(name, type: type), out int ret)
                ? def : ret;

            public string getString(string name, string def = null, XmlType? type = null)
            {
                if (!exists(name)) return def;

                switch (type ?? ElemType)
                {
                    case XmlType.Attribute: return Element.Attribute(name).Value;
                    case XmlType.Node: return Element.Element(name).Value;
                    default: return def;
                }
            }

            public uint getUint(string name, uint def, XmlType? type = null) =>
                !exists(name, type) || !uint.TryParse(getString(name, type: type).Substring(2),
                    NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out uint ret) ? def : ret;
        }
    }
}
