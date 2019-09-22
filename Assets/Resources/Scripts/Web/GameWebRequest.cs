using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Resources.Scripts.Web
{
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-send-data-using-the-webrequest-class
    // https://stackoverflow.com/questions/46003824/sending-http-requests-in-c-sharp-with-unity

    public abstract class GameWebRequest
    {
        protected static GameWebProtocol webServerProtocol => GameWebProtocol.http;
        protected static string webServerIPAddress => "54.39.227.169";
        protected static int webServerPort => 8080;

        protected GameWebMethod method { get; set; }
        protected string request { get; set; }
        protected List<(string key, string value)> fields { get; set; }
        protected string response { get; set; }

        public string GetRequest => request;

        private void HandleRequest(UnityWebRequest uwr)
        {
            if (uwr.isHttpError) Debug.LogErrorFormat("HTTP Error occurred along request procedure '{0}' to the AppEngine!\n Error: {1}", request, uwr.error);
            if (uwr.isNetworkError) Debug.LogErrorFormat("Network Error occurred along request procedure '{0}' to the AppEngine!\n Error: {1}", request, uwr.error);

            response = uwr.downloadHandler.text;

            Debug.LogWarning(response);
        }

        private IEnumerator HandleGetRequest()
        {
            var wr = WebRequest.Create(FormatRequest());
            wr.Credentials = CredentialCache.DefaultCredentials;
            wr.Method = "GET";

            var uwr = UnityWebRequest.Get(FormatRequest());

            yield return uwr.SendWebRequest();

            HandleRequest(uwr);
        }

        private IEnumerator HandlePostRequest()
        {
            var form = new WWWForm();

            if (fields != null && fields.Count != 0)
                foreach (var (key, value) in fields)
                    form.AddField(key, value);

            var uwr = UnityWebRequest.Post(FormatRequest(), form);

            yield return uwr.SendWebRequest();

            HandleRequest(uwr);
        }

        private string FormatRequest()
            => $"{webServerProtocol.ToString()}://{webServerIPAddress}:{webServerPort.ToString()}/{request.ValidateRequestPath()}";

        public abstract void Configure();

        public virtual void OnRequest()
        {
            switch (method)
            {
                case GameWebMethod.get: HandleGetRequest(); break;
                case GameWebMethod.post: HandlePostRequest(); break;
            }
        }

        public virtual string OnResponse()
        {
            //if (string.IsNullOrEmpty(response))
            //{
            //    Debug.LogErrorFormat("Client received an empty response from request '{0}' to the AppEngine via {1} method and {2} protocol!",
            //        request, method.ToString(), webServerProtocol.ToString());
            //    return null;
            //}

            return response;
        }
    }
}