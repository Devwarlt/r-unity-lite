using Assets.Core.Controller.Handlers.app;
using Assets.Core.Model.Account;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Core.Controller
{
    public static class AccountController
    {
        public static AccountModel account;

        public static CredentialsModel credentials;

        public static string file = "/account.data";

        public static AccountEventHandler onAccountChange;

        public static string path = Application.persistentDataPath;

        public delegate void AccountEventHandler();

        public static string location => path + file;

        public static void delete()
        {
            credentials = null;
            account = null;

            AccountModel.onCurrencyChange();
            File.Delete(location);

            onAccountChange();
        }

        public static void load()
        {
            if (File.Exists(location))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(location, FileMode.Open);

                credentials = formatter.Deserialize(stream) as CredentialsModel;

                stream.Close();
            }
            else
            {
                credentials = null;

                Debug.Log("Account file not found in " + location);
            }

            onAccountChange();
        }

        public static void save()
        {
            if (credentials == null) return;

            var formatter = new BinaryFormatter();
            var stream = new FileStream(location, FileMode.Create);

            formatter.Serialize(stream, credentials);

            stream.Close();
        }

        public static void set(string guid = null, string password = null)
        {
            credentials = new CredentialsModel(guid, password);
            save();

            onAccountChange();
        }

        public static bool verify()
        {
            if (credentials == null) return false;

            var verify = new VerifyHandler(credentials.guid, credentials.password);
            return (verify.SendRequest());
        }
    }
}
