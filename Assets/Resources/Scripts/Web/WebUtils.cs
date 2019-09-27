using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Assets.Resources.Scripts.Web
{
    public static class WebUtils
    {
        public static readonly Dictionary<GameWebMediaHeader, string> Headers = new Dictionary<GameWebMediaHeader, string>
        {
            { GameWebMediaHeader.ApplicationJavascript, "application/javascript" },
            { GameWebMediaHeader.ApplicationJson, "application/json" },
            { GameWebMediaHeader.ApplicationXWwwFormUrlEncoded, "application/x-www-form-urlencoded" },
            { GameWebMediaHeader.ApplicationXml, "application/xml" },
            { GameWebMediaHeader.ApplicationZip, "application/zip" },
            { GameWebMediaHeader.ApplicationPdf, "application/pdf" },
            { GameWebMediaHeader.ApplicationSql, "application/sql" },
            { GameWebMediaHeader.ApplicationGraphql, "application/graphql" },
            { GameWebMediaHeader.ApplicationLdJson, "application/ld+json" },
            { GameWebMediaHeader.ApplicationDoc, "application/msword" },
            { GameWebMediaHeader.ApplicationDocx, "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { GameWebMediaHeader.ApplicationXls, "application/vnd.ms-excel" },
            { GameWebMediaHeader.ApplicationXlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { GameWebMediaHeader.ApplicationPpt, "application/vnd.ms-powerpoint" },
            { GameWebMediaHeader.ApplicationPptx, "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            { GameWebMediaHeader.ApplicationOdt, "application/vnd.oasis.opendocument.text" },
            { GameWebMediaHeader.ApplicationVndApiJson, "application/vnd.api+json" },
            { GameWebMediaHeader.AudioMpeg, "audio/mpeg" },
            { GameWebMediaHeader.AudioOgg, "audio/ogg" },
            { GameWebMediaHeader.MultipartFormData, "multipart/form-data" },
            { GameWebMediaHeader.TextCss, "text/css" },
            { GameWebMediaHeader.TextHtml, "text/html" },
            { GameWebMediaHeader.TextXml, "text/xml" },
            { GameWebMediaHeader.TextCsv, "text/csv" },
            { GameWebMediaHeader.TextPlain, "text/plain" },
            { GameWebMediaHeader.ImagePng, "image/png" },
            { GameWebMediaHeader.ImageJpeg, "image/jpeg" },
            { GameWebMediaHeader.ImageGif, "image/gif" }
        };

        public static bool SupportedHttpMethods(this HttpMethod method) => method == HttpMethod.Get || method == HttpMethod.Post;

        public static string ToQueryStringsBuilder(this IDictionary<string, string> collection)
        {
            var sb = new StringBuilder("?");
            var i = 0;

            foreach (var kvp in collection)
                sb.Append($"{kvp.Key}={kvp.Value}{(++i != collection.Count ? "&" : "")}");

            return sb.ToString();
        }

        public static string ToQueryStrings(this IDictionary<string, string> collection)
            => "?" + string.Join("&", collection.Select(entry => $"{entry.Key}={entry.Value}").ToArray());

        public static string ValidateRequestPath(this string requestPath)
            => requestPath.StartsWith("/") || requestPath.StartsWith("\\") ? requestPath.Substring(1) : requestPath;
    }
}