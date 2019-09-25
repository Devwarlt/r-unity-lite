using System.Net.Http;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
    public class CharListHandler : WebRequestHandler
    {
        public CharListHandler(string guid, string password)
        {
            request = new GameWebRequest(HttpMethod.Post, "char/list");
            request.AddQuery("guid", guid);
            request.AddQuery("password", password);
        }

        public XElement getElem()
        {
            return XElement.Parse(request.OnResponse());
        }
    }
}