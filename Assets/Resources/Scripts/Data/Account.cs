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

public class AccountData
{
    public string username;
}