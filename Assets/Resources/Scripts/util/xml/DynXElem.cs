using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assets.Resources.Scripts.util.xml
{
    public enum XElemType
    {
        attribute,
        node
    }
    public abstract class XElem
    {
        public XElemType ElemType;
        public XElement Element;

        public XElem(XElemType type, XElement elem)
        {
            ElemType = type;
            Element = elem;
        }

        public bool exists(string name, XElemType? type = null)
        {
            switch (type ?? ElemType)
            {
                case XElemType.attribute:
                    return Element.Attribute(name) != null;
                case XElemType.node:
                    return Element.Element(name) != null;
            }
            return false;
        }
        public string getString(string name, string def = null, XElemType? type = null)
        {
            if (!exists(name))
                return def;
            switch (type ?? ElemType)
            {
                case XElemType.attribute:
                    return Element.Attribute(name).Value;
                case XElemType.node:
                    return Element.Element(name).Value;
                default:
                    return def;
            }
        }
        public int getInt(string name, int def, XElemType? type = null)
        {
            int ret;
            if (!exists(name, type) ||
                !int.TryParse(getString(name, type: type), out ret))
                return def;
            return ret;
        }
        public double getDouble(string name, double def, XElemType? type = null)
        {
            double ret;
            if (!exists(name, type) ||
                !double.TryParse(getString(name, type: type), out ret))
                return def;
            return ret;
        }
        public bool getBool(string name, bool def, XElemType? type = null)
        {
            if (!exists(name, type))
                return def;
            var str = getString(name, type: type);
            return !(str == "0" || str == "false" || str == "f");
        }
        public string[] getArray(string name, string[] split, XElemType? type = null)
        {
            if (!exists(name, type))
                return null;
            return getString(name, type: type).Split(split, StringSplitOptions.None);
        }
        public uint getUint(string name, uint def, XElemType? type = null)
        {
            uint ret;
            if (!exists(name, type) ||
                !uint.TryParse(getString(name, type: type).Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out ret))
                return def;
            return ret;
        }
    }
}
