using System.Net.Http;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class RegisterHandler : RequestHandler
    {
        public RegisterHandler(string newGUID, string newPassword)
        {
            request = new Request(HttpMethod.Post, "account/register");
            request.AddQuery("guid", newGUID);
            request.AddQuery("newGUID", newGUID);
            request.AddQuery("newPassword", newPassword);
        }
    }
}
