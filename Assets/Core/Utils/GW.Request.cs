using Assets.Core.Controller.Environments;
using Assets.Core.Utils.Monitoring;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Assets.Core.Utils
{
    public static partial class GW
    {
        public sealed class Request
        {
            public readonly string request;
            private readonly Encoding encoding;
            private readonly MediaHeader header;
            private readonly HttpMethod method;
            private Dictionary<string, string> nameValueCollection;
            private string response;

            public Request(HttpMethod method, string request) : this(method, request, Encoding.UTF8, MediaHeader.ApplicationXWwwFormUrlEncoded)
            {
            }

            public Request(HttpMethod method, string request, MediaHeader header) : this(method, request, Encoding.UTF8, header)
            {
            }

            public Request(HttpMethod method, string request, Encoding encoding, MediaHeader header)
            {
                this.method = method;
                this.request = request;
                this.encoding = encoding;
                this.header = header;

                if (!Headers.ContainsKey(header)) throw new RequestException("GameWebMediaHeader not found in dictionary!");
                if (!method.SupportedHttpMethods()) throw new RequestException("HttpMethod not supported!");

                nameValueCollection = new Dictionary<string, string>();
                response = string.Empty;
            }

            public void AddQuery(string key, string value) =>
                nameValueCollection.Add(key, value);

            public void OnRequest()
            {
                Log.Write("Web Request to '{0}'...", UriString());

                if (method == HttpMethod.Get) HandleGetRequestAsync();
                if (method == HttpMethod.Post) HandlePostRequestAsync();
            }

            public string OnResponse()
            {
                if (string.IsNullOrEmpty(response))
                {
                    Log.Error("Client received an empty response from request '{0}' to the AppEngine via {1} method and {2} protocol!",
                        request, method.ToString(), GBE.GetEnvironment().GetProtocol().ToString());
                    return null;
                }

                return response;
            }

            private async void HandleGetRequestAsync()
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                    response = await client.GetStringAsync(UriString() + (nameValueCollection.Count != 0 ? nameValueCollection.ToQueryStringsBuilder() : string.Empty));
            }

            private async void HandlePostRequestAsync()
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                    response = await client.PostAsync(UriString(), new FormUrlEncodedContent(nameValueCollection)).Result.Content.ReadAsStringAsync();
            }

            private string UriString(bool hasPrefix = true) =>
                string.Format("{0}/{1}",
                    GBE.GetEnvironment().GetUriString(),
                    hasPrefix ? request.ValidateRequestPath() : string.Empty);
        }
    }
}
