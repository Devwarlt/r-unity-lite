using Assets.Resources.Scripts.Screens.Main;
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

    public static AccountData data;

    public static void set(string guid = null, string password = null)
    {
        data = new AccountData(guid, password);
        save();

        onAccountChange();
    }

    public static void load()
    {
        if (File.Exists(location))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(location, FileMode.Open);

            data = formatter.Deserialize(stream) as AccountData;
            Debug.Log(data.Fame);
        }
        else
        {
            data = null;
            Debug.Log("Account file not found in " + location);
        }

        onAccountChange();
    }

    public static void delete()
    {
        data = null;
        File.Delete(location);

        onAccountChange();
    }

    public static void save()
    {
        if (data == null)
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(location, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
}

[System.Serializable]
public class AccountData
{
    public string guid;
    public string password;

    public string Name;
    public int AccountID;
    public int NextCharSlotPrice;
    public int Fame;
    public int Credits;

    public AccountData(string guid, string password)
    {
        this.guid = guid;
        this.password = password;
    }

    public void Setup(XElement xml) {
        var acc = xml.Element("Account");

        Name = acc.Element("Name").Value;
        AccountID = int.Parse(acc.Element("AccountId").Value);
        NextCharSlotPrice = int.Parse(acc.Element("NextCharSlotPrice").Value);
        Fame = int.Parse(acc.Element("Fame").Value);
        Credits = int.Parse(acc.Element("Credits").Value);
    }
}