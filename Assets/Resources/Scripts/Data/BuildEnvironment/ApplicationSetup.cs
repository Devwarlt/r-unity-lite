using Assets.Resources.Scripts.Web;

public interface ApplicationSetup
{
    GameWebProtocol serverProtocol();
    string serverAddress();
    string serverUrl();
    string buildLabel();
    string supportLink();
    string copyrightLabel();
}