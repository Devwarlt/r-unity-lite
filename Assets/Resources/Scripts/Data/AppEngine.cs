using Assets.Resources.Scripts.Web;
using UnityEngine;

public static class AppEngine
{
    public static GameWebProtocol webServerProtocol => GameWebProtocol.HTTP;
    public static string webServerIPAddress => "54.39.227.169";
    public static int webServerPort => 8080;

    public static string getUrl() => $"{webServerProtocol.ToString().ToLower()}://{webServerIPAddress}:{webServerPort.ToString()}";

    public static string getBuild() => "Shot's Realm #{VERSION}".Replace("{VERSION}", Application.version);
}