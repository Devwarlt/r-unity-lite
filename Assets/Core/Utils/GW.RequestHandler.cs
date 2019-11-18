using UnityEngine;

namespace Assets.Core.Utils
{
    public static partial class GW
    {
        public abstract class RequestHandler
        {
            protected Request request;
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
                return !response.Contains("Error");
            }
        }
    }
}
