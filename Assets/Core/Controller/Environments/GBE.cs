using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environments
{
    public static partial class GBE
    {
        public static Environment localhost = new Environment("localhost", "Localhost", Protocol.HTTP, "https://github.com/Devwarlt", 8080);
        public static Environment utReborn = new Environment("54.39.227.169", "UT Reborn", Protocol.HTTP, "https://github.com/Devwarlt", 8080);

        public static Environment GetEnvironment() =>
            localhost;
    }
}
