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

        public bool exists(string name)
        {
            switch (ElemType)
            {
                case XElemType.attribute:
                    return Element.Attribute(name) != null;
                case XElemType.node:
                    return Element.Element(name) != null;
            }
            return false;
        }
        public string getString(string name, string def = null)
        {
            if (!exists(name))
                return def;
            switch (ElemType)
            {
                case XElemType.attribute:
                    return Element.Attribute(name).Value;
                case XElemType.node:
                    return Element.Element(name).Value;
                default:
                    return def;
            }
        }
        public int getInt(string name, int def)
        {
            int ret;
            if (!exists(name) ||
                !int.TryParse(getString(name), out ret))
                return def;
            return ret;
        }
        public double getDouble(string name, double def)
        {
            double ret;
            if (!exists(name) ||
                !double.TryParse(getString(name), out ret))
                return def;
            return ret;
        }
        public bool getBool(string name, bool def)
        {
            if (!exists(name))
                return def;
            var str = getString(name);
            return !(str == "0" || str == "false" || str == "f");
        }
        public string[] getArray(string name, string[] split)
        {
            if (!exists(name))
                return null;
            return getString(name).Split(split, StringSplitOptions.None);
        }
        public uint getUint(string name, uint def)
        {
            uint ret;
            if (!exists(name) ||
                !uint.TryParse(getString(name).Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out ret))
                return def;
            return ret;
        }
    }
}
