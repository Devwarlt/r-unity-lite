using Assets.Resources.Scripts.Screens.Main;
using Assets.Resources.Scripts.util.xml;
using Assets.Resources.Scripts.Web.Handlers.app;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using UnityEngine;

public static class Account
{
    public delegate void AccountEventHandler();
    public static AccountEventHandler onAccountChange;

    public static string path = Application.persistentDataPath;
    public static string file = "/account.data";
    public static string location => path + file;

    public static CredentialsData credentials;
    public static AccountData account;

    public static void set(string guid = null, string password = null)
    {
        credentials = new CredentialsData(guid, password);
        save();

        onAccountChange();
    }

    public static void load()
    {
        if (File.Exists(location))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(location, FileMode.Open);
            credentials = formatter.Deserialize(stream) as CredentialsData;
            stream.Close();
        }
        else
        {
            credentials = null;
            Debug.Log("Account file not found in " + location);
        }

        onAccountChange();
    }

    public static void delete()
    {
        credentials = null;
        File.Delete(location);

        onAccountChange();
    }

    public static void save()
    {
        if (credentials == null)
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(location, FileMode.Create);
        formatter.Serialize(stream, credentials);
        stream.Close();
    }

    public static bool verify()
    {
        if (credentials == null)
            return false;

        var verify = new VerifyHandler(credentials.guid, credentials.password);
        return (verify.SendRequest());
    }
}

[System.Serializable]
public class CredentialsData
{
    public string guid;
    public string password;

    public CredentialsData(string guid, string password)
    {
        this.guid = guid;
        this.password = password;
    }
}

public class AccountData : XElem
{
    public int accountID;
    public string username;
    public bool admin;
    public int rank;
    public int lastSeen; //possibly long?
    public bool isAgeVerified;
    public bool isFirstDeath;
    public int petYardType;
    public int credits;
    public int nextCharSlotPrice;
    public int charSlotCurrency;
    public string menuMusic;
    public string deadMusic;
    public int mapMinRank;
    public int spriteMinRank;
    public int beginnerPackageTimeLeft;
    public AccountStatsData stats;

    public AccountData(XElement elem) : base(XElemType.node, elem)
    {
        accountID = getInt("AccountId", 0);
        username = getString("Name", "");
        admin = getBool("Admin", false);
        rank = getInt("Rank", 0);
        lastSeen = getInt("LastSeen", 0);
        isAgeVerified = getBool("isAgeVerified", true);
        isFirstDeath = getBool("isFirstDeath", false);
        petYardType = getInt("PetYardType", 1);
        credits = getInt("Credits", 0);
        nextCharSlotPrice = getInt("NextCharSlotPrice", 1000);
        charSlotCurrency = getInt("CharSlotCurrency", 1);
        menuMusic = getString("MenuMusic");
        deadMusic = getString("DeadMusic");
        mapMinRank = getInt("MapMinRank", 0);
        spriteMinRank = getInt("SpriteMinRank", 0);
        beginnerPackageTimeLeft = getInt("BeginnerPackageTimeLeft", 0);
        stats = new AccountStatsData(elem.Element("Stats"));
    }
}

public class AccountStatsData : XElem
{
    public Dictionary<uint, ClassStatsData> classStats;
    public int bestCharFame;
    public int totalFame;
    public int fame;

    public AccountStatsData(XElement elem) : base(XElemType.node, elem)
    {
        //classStats = ClassStatsData.loadDict(elem); /* GIVES AN ERROR TRYING TO GET OBJECTTYPE */
        bestCharFame = getInt("BestCharFame", 0);
        totalFame = getInt("TotalFame", 0);
        fame = getInt("Fame", 0);
    }
}

public class ClassStatsData : XElem
{
    public uint objectType;
    public int bestLevel;
    public int bestFame;

    public ClassStatsData(XElement elem) : base(XElemType.node,  elem)
    {
        objectType = getUint("objectType", 0, XElemType.attribute);
        bestLevel = getInt("BestLevel", 1);
        bestFame = getInt("BestFame", 0);
    }

    public static Dictionary<uint, ClassStatsData> loadDict(XElement root)
    {
        Dictionary<uint, ClassStatsData> dict = new Dictionary<uint, ClassStatsData>();

        foreach (var elem in root.Elements("ClassStats"))
        {
            var stats = new ClassStatsData(elem);
            dict.Add(stats.objectType, stats);
        }

        return dict;
    }
}
