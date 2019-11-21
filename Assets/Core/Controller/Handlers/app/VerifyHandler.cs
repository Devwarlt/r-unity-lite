using System.Net.Http;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class VerifyHandler : RequestHandler
    {
        public VerifyHandler(string guid, string password)
        {
            request = new Request(HttpMethod.Get, "account/verify", MediaHeader.TextPlain);
            request.AddQuery("guid", guid);
            request.AddQuery("password", password);
        }
    }
}
