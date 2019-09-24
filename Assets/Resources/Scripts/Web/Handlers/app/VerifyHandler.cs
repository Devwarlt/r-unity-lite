using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
	public class VerifyHandler
	{
	    private GameWebRequest verify;

        public VerifyHandler(string guid, string password)
        {
            verify = new GameWebRequest(HttpMethod.Post, "account/verify");
            verify.AddQuery("guid", guid);
            verify.AddQuery("password", password);
        }

        public bool SendRequest()
        {
        	verify.OnRequest();
        	return HandleRequest();
        }

        private bool HandleRequest()
        {
        	string response = verify.OnResponse();
        	Debug.LogWarning("Response from Login Verification:\n" + response);
        	return !response.Contains("Error"); // not how this is supposed to be done
        	// todo: make this handle the acc info that comes from the login verification request
        }
    }
}
