using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Environments
{
    public static partial class GBE
    {
        public static Environment localhost = new Environment("localhost", "Localhost", Protocol.HTTP, "https://github.com/Devwarlt/R-Unity", 8080);
        public static Environment rotmg = new Environment("realmofthemadgod.appspot.com", "RotMG", Protocol.HTTP, "http://realmofthemadgod.appspot.com/", 80);

        public static Environment GetEnvironment() =>
            rotmg;
    }
}
