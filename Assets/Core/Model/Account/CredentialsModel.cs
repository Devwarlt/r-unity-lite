using System;

namespace Assets.Core.Model.Account
{
    [Serializable]
    public class CredentialsModel
    {
        public string guid;
        public string password;

        public CredentialsModel(string guid, string password)
        {
            this.guid = guid;
            this.password = password;
        }
    }
}
