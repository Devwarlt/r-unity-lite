using Assets.Core.Model.Account;
using Assets.Core.Model.Server;
using System.Net.Http;
using System.Xml.Linq;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class CharListHandler : RequestHandler
    {
        public CharListHandler(string guid, string password)
        {
            request = new Request(HttpMethod.Post, "char/list");
            request.AddQuery("guid", guid);
            request.AddQuery("password", password);
        }

        public void load()
        {
            var elem = XElement.Parse(request.OnResponse());

            AccountController.account = new AccountModel(elem.Element("Account"));
            CharListModel.load(elem);
            ServerController.load(elem.Element("Servers"));
        }
    }
}
