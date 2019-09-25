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
    public static string path = Application.persistentDataPath;
    public static string file = "/account.bin";
    public static string location => path + file;

    public static AccountData data;

    public static void set(string guid = null, string password = null)
    {
        data.set(guid, password);
        save();
    }

    public static bool load()
    {
        if (File.Exists(location))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(location, FileMode.Open);

            data = formatter.Deserialize(stream) as AccountData;
            return true;
        }
        else
        {
            Debug.Log("Account file not found in " + location);
            return false;
        }
    }

    public static void save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }
}

[System.Serializable]
public class AccountData
{
    public string guid;
    public string password;

    public AccountData(string guid, string password)
    {
        this.guid = guid;
        this.password = password;
    }

    public void set(string guid, string password)
    {
        if (guid != null)
            this.guid = guid;
        if (password != null)
            this.password = password;
    }
}