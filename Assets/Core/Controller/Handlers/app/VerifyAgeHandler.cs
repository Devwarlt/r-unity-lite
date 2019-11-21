using System.Net.Http;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class VerifyAgeHandler : RequestHandler
    {
        public VerifyAgeHandler(string guid, string password)
        {
            request = new Request(HttpMethod.Get, "account/verifyage");
            request.AddQuery("guid", guid);
            request.AddQuery("password", password);
            request.AddQuery("isAgeVerified", "1");
        }
    }
}
