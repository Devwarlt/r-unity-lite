using System.Xml.Linq;
using static Assets.Core.Utils.GU;

namespace Assets.Core.Model.Account
{
    public class AccountModel : XmlElement
    {
        public static CurrencyEventHandler onCurrencyChange;

        public int _credits;
        public int accountID;
        public bool admin;
        public int beginnerPackageTimeLeft;
        public int charSlotCurrency;
        public string deadMusic;
        public bool isAgeVerified;
        public bool isFirstDeath;
        public int lastSeen;
        public int mapMinRank;
        public string menuMusic;
        public int nextCharSlotPrice;

        //possibly long?
        public int petYardType;

        public int rank;
        public int spriteMinRank;
        public StatsAccountModel stats;
        public string username;

        public AccountModel(XElement elem) : base(XmlType.Node, elem)
        {
            accountID = getInt("AccountId", 0);
            username = getString("Name", "");
            admin = getBool("Admin", false);
            rank = getInt("Rank", 0);
            lastSeen = getInt("LastSeen", 0);
            isAgeVerified = getBool("isAgeVerified", true);
            isFirstDeath = getBool("isFirstDeath", false);
            petYardType = getInt("PetYardType", 1);
            credits = getInt("Credits", 0);
            nextCharSlotPrice = getInt("NextCharSlotPrice", 1000);
            charSlotCurrency = getInt("CharSlotCurrency", 1);
            menuMusic = getString("MenuMusic");
            deadMusic = getString("DeadMusic");
            mapMinRank = getInt("MapMinRank", 0);
            spriteMinRank = getInt("SpriteMinRank", 0);
            beginnerPackageTimeLeft = getInt("BeginnerPackageTimeLeft", 0);
            stats = new StatsAccountModel(elem.Element("Stats"));
        }

        public delegate void CurrencyEventHandler();

        public int credits
        {
            get { return _credits; }
            set { _credits = value; onCurrencyChange(); }
        }

        public int fame
        {
            get { return stats.fame; }
            set { stats.fame = value; onCurrencyChange(); }
        }
    }
}
