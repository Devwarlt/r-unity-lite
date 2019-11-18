using System.Linq;
using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Server
{
    public class CharModel : XmlElement
    {
        public int attack;
        public int currentFame;
        public bool dead;
        public int defense;
        public int dexterity;
        public int[] equipment;
        public int exp;
        public bool hasBackpack;

        //public blah pcStats; /* don't know what this is, fame stats? */
        public int healthStackCount;

        public int hitPoints;
        public int id;
        public int level;
        public int magicPoints;
        public int magicStackCount;
        public int maxHitPoints;
        public int maxMagicPoints;
        public int objectType;
        public int speed;
        public int tex1;
        public int tex2;
        public int texture;
        public int vitality;
        public int wisdom;

        public CharModel(XElement elem) : base(XmlType.Node, elem)
        {
            id = int.Parse(elem.Attribute("id").Value);
            objectType = getInt("ObjectType", 0);
            level = getInt("Level", 1);
            exp = getInt("Exp", 0);
            currentFame = getInt("currentFame", 0);
            equipment = getArray("Equipment", new string[] { ", " })
                .Select(x => int.Parse(x)).ToArray();
            maxHitPoints = getInt("MaxHitPoints", 0);
            hitPoints = getInt("HitPoints", 0);
            maxMagicPoints = getInt("MaxMagicPoints", 0);
            magicPoints = getInt("MagicPoints", 0);
            attack = getInt("Attack", 0);
            defense = getInt("Defense", 0);
            speed = getInt("Speed", 0);
            dexterity = getInt("Dexterity", 0);
            vitality = getInt("HpRegen", 0);
            wisdom = getInt("MpRegen", 0);
            tex1 = getInt("Tex1", 0);
            tex2 = getInt("Tex2", 0);
            texture = getInt("Texture", 0);
            healthStackCount = getInt("HealthStackCount", 0);
            magicStackCount = getInt("MagicStackCount", 0);
            dead = getBool("Dead", false);
            hasBackpack = getBool("HasBackpack", false);
        }
    }
}
