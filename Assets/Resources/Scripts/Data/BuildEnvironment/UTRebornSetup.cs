using Assets.Resources.Scripts.Web;
using UnityEngine;

public class UTRebornSetup : ApplicationSetup
{
    public const int serverPort = 8080;
    public GameWebProtocol serverProtocol() => GameWebProtocol.HTTP;
    public string serverAddress() => "54.39.227.169";
    public string serverUrl() => $"{serverProtocol().ToString().ToLower()}://{serverAddress()}:{serverPort}";
    public string buildLabel() => $"R-Unity #{Application.version}";
    public string supportLink() => "https://google.com";
    public string copyrightLabel() => "©R-Unity";
}