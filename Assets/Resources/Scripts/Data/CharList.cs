using Assets.Resources.Scripts.util.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public static class CharList
{
    public static CharListData data;
    public static Dictionary<int, CharData> chars;

    public static void load(XElement root)
    {
        data = new CharListData(root);
        chars = new Dictionary<int, CharData>();

        foreach (var elem in root.Elements("Char"))
        {
            var charData = new CharData(elem);
            chars.Add(charData.id, charData);
        }
    }
}

public class CharListData : XElem
{
    public int nextCharid;
    public int maxNumChars;

    public CharListData(XElement elem) : base(XElemType.attribute, elem)
    {
        nextCharid = getInt("nextCharId", 0);
        maxNumChars = getInt("maxNumChars", 1);
    }
}

public class CharData : XElem
{
    public int id;
    public int objectType;
    public int level;
    public int exp;
    public int currentFame;
    public int[] equipment;
    public int maxHitPoints;
    public int hitPoints;
    public int maxMagicPoints;
    public int magicPoints;
    public int attack;
    public int defense;
    public int speed;
    public int dexterity;
    public int vitality;
    public int wisdom;
    public int tex1;
    public int tex2;
    public int texture;
    //public blah pcStats; /* don't know what this is, fame stats? */
    public int healthStackCount;
    public int magicStackCount;
    public bool dead;
    public bool hasBackpack;

    public CharData(XElement elem) : base(XElemType.node, elem)
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
