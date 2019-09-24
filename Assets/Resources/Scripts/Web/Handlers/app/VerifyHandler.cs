using System.Net.Http;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
	public class VerifyHandler : WebRequestHandler
	{
        public VerifyHandler(string guid, string password)
        {
            request = new GameWebRequest(HttpMethod.Post, "account/verify");
            request.AddQuery("guid", guid);
            request.AddQuery("password", password);
        }
    }
}
