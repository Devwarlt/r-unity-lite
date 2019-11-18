using System;

namespace Assets.Core.Utils
{
    public static partial class GW
    {
        public class RequestException : Exception
        {
            public RequestException(string message) : base(message)
            {
            }
        }
    }
}
