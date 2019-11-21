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
                response = request.OnRequest().Result;

                return !response.Contains("Error");
            }
        }
    }
}
