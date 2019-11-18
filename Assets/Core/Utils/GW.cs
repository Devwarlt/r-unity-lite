using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Assets.Core.Utils
{
    public static partial class GW
    {
        public static readonly Dictionary<MediaHeader, string> Headers = new Dictionary<MediaHeader, string>
        {
            { MediaHeader.ApplicationJavascript, "application/javascript" },
            { MediaHeader.ApplicationJson, "application/json" },
            { MediaHeader.ApplicationXWwwFormUrlEncoded, "application/x-www-form-urlencoded" },
            { MediaHeader.ApplicationXml, "application/xml" },
            { MediaHeader.ApplicationZip, "application/zip" },
            { MediaHeader.ApplicationPdf, "application/pdf" },
            { MediaHeader.ApplicationSql, "application/sql" },
            { MediaHeader.ApplicationGraphql, "application/graphql" },
            { MediaHeader.ApplicationLdJson, "application/ld+json" },
            { MediaHeader.ApplicationDoc, "application/msword" },
            { MediaHeader.ApplicationDocx, "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { MediaHeader.ApplicationXls, "application/vnd.ms-excel" },
            { MediaHeader.ApplicationXlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { MediaHeader.ApplicationPpt, "application/vnd.ms-powerpoint" },
            { MediaHeader.ApplicationPptx, "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            { MediaHeader.ApplicationOdt, "application/vnd.oasis.opendocument.text" },
            { MediaHeader.ApplicationVndApiJson, "application/vnd.api+json" },
            { MediaHeader.AudioMpeg, "audio/mpeg" },
            { MediaHeader.AudioOgg, "audio/ogg" },
            { MediaHeader.MultipartFormData, "multipart/form-data" },
            { MediaHeader.TextCss, "text/css" },
            { MediaHeader.TextHtml, "text/html" },
            { MediaHeader.TextXml, "text/xml" },
            { MediaHeader.TextCsv, "text/csv" },
            { MediaHeader.TextPlain, "text/plain" },
            { MediaHeader.ImagePng, "image/png" },
            { MediaHeader.ImageJpeg, "image/jpeg" },
            { MediaHeader.ImageGif, "image/gif" }
        };

        public static bool SupportedHttpMethods(this HttpMethod method) =>
            method == HttpMethod.Get || method == HttpMethod.Post;

        public static string ToQueryStrings(this IDictionary<string, string> collection) =>
            "?" + string.Join("&", collection.Select(entry => $"{entry.Key}={entry.Value}").ToArray());

        public static string ToQueryStringsBuilder(this IDictionary<string, string> collection)
        {
            var sb = new StringBuilder("?");
            var i = 0;

            foreach (var kvp in collection)
                sb.Append($"{kvp.Key}={kvp.Value}{(++i != collection.Count ? "&" : "")}");

            return sb.ToString();
        }

        public static string ValidateRequestPath(this string requestPath) =>
            requestPath.StartsWith("/") || requestPath.StartsWith("\\") ? requestPath.Substring(1) : requestPath;
    }
}
