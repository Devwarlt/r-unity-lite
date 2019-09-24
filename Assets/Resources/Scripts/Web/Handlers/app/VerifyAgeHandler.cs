using System.Net.Http;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
	public class VerifyAgeHandler : WebRequestHandler
	{
	    public VerifyAgeHandler(string guid, string password)
	    {
	    	request = new GameWebRequest(HttpMethod.Post, "account/verifyage");
	    	request.AddQuery("guid", guid);
	    	request.AddQuery("password", password);
	    	request.AddQuery("isAgeVerified", "1");
	    }
	}
}
