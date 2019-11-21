using Assets.Core.Controller.Environments;
using Assets.Core.Utils.Monitoring;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

                if (!Headers.ContainsKey(header)) throw new RequestException("MediaHeader not found in dictionary!");
                if (!method.SupportedHttpMethods()) throw new RequestException("HttpMethod not supported!");

                nameValueCollection = new Dictionary<string, string>();
                response = string.Empty;
            }

            public void AddQuery(string key, string value) =>
                nameValueCollection.Add(key, value);

            public async Task<string> OnRequest()
            {
                Log.Write("Web Request to '{0}'...", UriString());

                if (method == HttpMethod.Get) return await HandleGetRequestAsync();
                if (method == HttpMethod.Post) return await HandlePostRequestAsync();

                return OnResponse();
            }

            private string OnResponse()
            {
                if (string.IsNullOrEmpty(response))
                {
                    Log.Error("Client received an empty response from request '{0}' to the AppEngine via {1} method and {2} protocol!",
                        request, method.ToString(), GBE.GetEnvironment().GetProtocol().ToString());
                    return null;
                }

                return response;
            }

            private async Task<string> HandleGetRequestAsync()
            {
                using (new TimedProfiler(string.Format("[protocol: {0}, method: {1}] request: {2}", GBE.GetEnvironment().GetProtocol().ToString(), method.ToString(), request)))
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.None }))
                    response = await client.GetStringAsync(UriString() + (nameValueCollection.Count != 0 ? nameValueCollection.ToQueryStringsBuilder() : string.Empty));

                OnResponse();

                return response;
            }

            private async Task<string> HandlePostRequestAsync()
            {
                using (new TimedProfiler(string.Format("[protocol: {0}, method: {1}] request: {2}", GBE.GetEnvironment().GetProtocol().ToString(), method.ToString(), request)))
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.None }))
                    response = await client.PostAsync(UriString(), new FormUrlEncodedContent(nameValueCollection)).Result.Content.ReadAsStringAsync();

                OnResponse();

                return response;
            }

            private string UriString(bool hasPrefix = true) =>
                string.Format("{0}/{1}",
                    GBE.GetEnvironment().GetUriString(),
                    hasPrefix ? request.ValidateRequestPath() : string.Empty);
        }
    }
}
