using Assets.Resources.Scripts.Monitoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Assets.Resources.Scripts.Web
{
    // "Web Request" references:
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-request-data-using-the-webrequest-class
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-send-data-using-the-webrequest-class
    // https://forums.asp.net/t/2081327.aspx?Get+HttpClient+with+parameters
    // https://stackoverflow.com/a/10679340

    public sealed class GameWebRequest
    {
        private readonly HttpMethod method;
        private readonly Encoding encoding;
        private readonly GameWebMediaHeader header;

        private Dictionary<string, string> nameValueCollection;
        private string response;

        public readonly string request;

        public GameWebRequest(HttpMethod method, string request) : this(method, request, Encoding.UTF8, GameWebMediaHeader.ApplicationXWwwFormUrlEncoded)
        {
        }

        public GameWebRequest(HttpMethod method, string request, GameWebMediaHeader header) : this(method, request, Encoding.UTF8, header)
        {
        }

        public GameWebRequest(HttpMethod method, string request, Encoding encoding, GameWebMediaHeader header)
        {
            this.method = method;
            this.request = request;
            this.encoding = encoding;
            this.header = header;

            if (!WebUtils.Headers.ContainsKey(header)) throw new WebRequestException("GameWebMediaHeader not found in dictionary!");
            if (!method.SupportedHttpMethods()) throw new WebRequestException("HttpMethod not supported!");

            nameValueCollection = new Dictionary<string, string>();
            response = string.Empty;
        }

        public void AddQuery(string key, string value) => nameValueCollection.Add(key, value);

        private string UriString(bool hasPrefix = true)
            => $"{AppEngine.env.serverUrl()}/{(hasPrefix ? request.ValidateRequestPath() : string.Empty)}";

        [Obsolete]
        private void HandleGetRequest()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(WebUtils.Headers[header]));

                var builder = new UriBuilder(UriString())
                {
                    Query = nameValueCollection.Count != 0 ? nameValueCollection.ToQueryStringsBuilder() : string.Empty
                };
                var response = client.GetAsync(builder.Uri).Result;

                using (var stream = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                    this.response = stream.ReadToEnd();
            }
        }

        private async void HandleGetRequestAsync()
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
                response = await client.GetStringAsync(UriString() + (nameValueCollection.Count != 0 ? nameValueCollection.ToQueryStringsBuilder() : string.Empty));
        }

        [Obsolete]
        private void HandlePostRequest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UriString(false));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(WebUtils.Headers[header]));

                var request = new HttpRequestMessage(HttpMethod.Post, this.request.ValidateRequestPath())
                {
                    Content = new StringContent(nameValueCollection.Count != 0 ? nameValueCollection.ToQueryStringsBuilder() : string.Empty, encoding, WebUtils.Headers[header])
                };

                client.SendAsync(request).ContinueWith(response => this.response = response.Result.Content.ReadAsStringAsync().Result);
            }
        }

        private async void HandlePostRequestAsync()
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
                response = await client.PostAsync(UriString(), new FormUrlEncodedContent(nameValueCollection)).Result.Content.ReadAsStringAsync();
        }

        public void OnRequest()
        {
            Log.Write("Web Request to '{0}'...", UriString());

            //if (method == HttpMethod.Get) HandleGetRequest();
            //if (method == HttpMethod.Post) HandlePostRequest();
            if (method == HttpMethod.Get) HandleGetRequestAsync();
            if (method == HttpMethod.Post) HandlePostRequestAsync();
        }

        public string OnResponse()
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Error("Client received an empty response from request '{0}' to the AppEngine via {1} method and {2} protocol!",
                    request, method.ToString(), AppEngine.env.serverProtocol().ToString());
                return null;
            }

            return response;
        }
    }
}