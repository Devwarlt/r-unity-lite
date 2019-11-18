using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environment
{
    public interface IGBE
    {
        string buildLabel();

        string copyrightLabel();

        string serverAddress();

        Protocol serverProtocol();

        string serverUrl();

        string supportLink();
    }
}
