using Assets.Resources.Scripts.Web;
using UnityEngine;

public class ShotsRealmSetup : ApplicationSetup
{
    public GameWebProtocol serverProtocol() => GameWebProtocol.HTTP;
    public string serverAddress() => "play.shotsrealm.com";
    public string serverUrl() => $"{serverProtocol().ToString().ToLower()}://{serverAddress()}";
    public string buildLabel() => $"Shot's Realm #{Application.version}";
    public string supportLink() => "https://shotsrealm.com";
    public string copyrightLabel() => "©Shot's Realm";
}