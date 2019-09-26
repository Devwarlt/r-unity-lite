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
    public int credits;
    public int fame; // FAME IS LOCATED UNDER STATS
    public int nextCharSlotPrice;

    public AccountData(XElement elem) : base(XElemType.node, elem)
    {
        accountID = getInt("AccountId", 0);
        username = getString("Name", "");
        admin = getBool("Admin", false);
        rank = getInt("Rank", 0);
        credits = getInt("Credits", 0);
        fame = getInt("Fame", 0);
        nextCharSlotPrice = getInt("NextCharSlotPrice", 1000);
    }
}
