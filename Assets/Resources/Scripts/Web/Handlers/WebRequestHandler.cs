using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
	public abstract class WebRequestHandler
	{
	    protected GameWebRequest request;
	    public string response { get; private set; }

	    public virtual bool SendRequest()
        {
        	request.OnRequest();
        	return HandleRequest();
        }

        protected virtual bool HandleRequest()
        {
        	response = request.OnResponse();
        	Debug.LogWarning("Response from Web Request:\n" + response);
        	return !response.Contains("Error"); // not how this is supposed to be done
        	// todo: make this handle the acc info that comes from the login verification request
        }
	}
}
