using System;

namespace Assets.Core.Utils
{
    public static partial class GU
    {
        [Flags]
        public enum GameScene
        {
            Main,
            Servers,
            Play
        }
    }
}
