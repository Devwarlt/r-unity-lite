using Assets.Core.Model.Account;
using TMPro;
using UnityEngine;

namespace Assets.Core.Controller
{
    public class CurrencyController : MonoBehaviour
    {
        [Header("Fame")]
        public TextMeshProUGUI fameText;

        public GameObject fameGroup;

        [Header("Credits")]
        public TextMeshProUGUI creditsText;

        public GameObject creditsGroup;

        private bool setup;

        private void Awake()
        {
            setup = false;

            AccountModel.onCurrencyChange += load;
        }

        private void Update()
        {
            if (setup) return;

            load();

            if (AccountController.account != null) setup = true;
        }

        public void load()
        {
            if (AccountController.account == null)
            {
                setActive(false, false);
                return;
            }

            var fameAmt = AccountController.account.fame;
            var creditsAmt = AccountController.account.credits;

            setActive(true, creditsAmt > 0);

            fameText.text = fameAmt.ToString();
            creditsText.text = creditsAmt.ToString();
        }

        public void setActive(bool? fame = null, bool? credits = null)
        {
            if (fame != null) fameGroup.SetActive(fame.Value);
            if (credits != null) creditsGroup.SetActive(credits.Value);
        }
    }
}
