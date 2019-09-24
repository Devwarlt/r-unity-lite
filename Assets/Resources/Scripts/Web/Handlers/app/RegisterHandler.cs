using System.Net.Http;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
	public class RegisterHandler : WebRequestHandler
	{
	    public RegisterHandler(string newGUID, string newPassword)
	    {
	    	request = new GameWebRequest(HttpMethod.Post, "account/register");
	    	request.AddQuery("guid", newGUID);
	    	request.AddQuery("newGUID", newGUID);
	    	request.AddQuery("newPassword", newPassword);
	    }
	}
}
