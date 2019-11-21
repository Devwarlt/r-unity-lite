using System.Net.Http;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class InitHandler : RequestHandler
    {
        public InitHandler() =>
            request = new Request(HttpMethod.Get, "app/init");
    }
}
