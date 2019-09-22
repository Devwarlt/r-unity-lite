using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
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

        private void HandleRequest(HttpWebResponse wr)
        {
            if (wr.StatusCode != HttpStatusCode.OK)
                Debug.LogWarning($"StatusCode is not HttpStatusCode.OK: {wr.StatusCode}");
            
            using (StreamReader reader = new StreamReader(wr.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }
        }

        private void HandleGetRequest()
        {
            var wr = WebRequest.Create(FormatRequest());
            wr.Credentials = CredentialCache.DefaultCredentials;
            wr.Method = "GET";
            HttpWebResponse wresponse = (HttpWebResponse)wr.GetResponse();

            HandleRequest(wresponse);
        }

        private void HandlePostRequest()
        {
            var wr = (HttpWebRequest)WebRequest.Create(FormatRequest());
            wr.Credentials = CredentialCache.DefaultCredentials;
            wr.Method = "POST";

            wr.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            
            var form = new WWWForm();

            if (fields != null && fields.Count != 0)
                foreach (var (key, value) in fields)
                    form.AddField(key, value);

            wr.ContentLength = form.data.Length;
            wr.ContentType = "application/x-www-form-urlencoded";

            Stream st = wr.GetRequestStream();
            st.Write(form.data, 0, form.data.Length);
            st.Close();

            HttpWebResponse wresponse = (HttpWebResponse)wr.GetResponse();

            HandleRequest(wresponse);
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
            if (string.IsNullOrEmpty(response))
            {
                Debug.LogErrorFormat("Client received an empty response from request '{0}' to the AppEngine via {1} method and {2} protocol!",
                    request, method.ToString(), webServerProtocol.ToString());
                return null;
            }

            return response;
        }
    }
}