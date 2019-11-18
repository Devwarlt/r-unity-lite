using System;

namespace Assets.Core.Utils
{
    public static partial class GW
    {
        [Flags]
        public enum Protocol
        {
            HTTP,
            HTTPS
        }
    }
}
