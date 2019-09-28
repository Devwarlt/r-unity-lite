using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyControl : MonoBehaviour
{
    [Header("Fame")]
    public TextMeshProUGUI fameText;
    public GameObject fameGroup;

    [Header("Credits")]
    public TextMeshProUGUI creditsText;
    public GameObject creditsGroup;

    bool setup;
    private void Awake()
    {
        setup = false;
        AccountData.onCurrencyChange += load;
    }

    private void Update()
    {
        if (setup)
            return;

        load();
        //just checks for first time account is set to true
        //kinda messy code :/ should redo but works for now
        if (Account.account != null)
            setup = true;
    }

    public void load()
    {
        if (Account.account == null)
        {
            setActive(false, false);
            return;
        }

        var fameAmt = Account.account.fame;
        var creditsAmt = Account.account.credits;

        setActive(true, creditsAmt > 0);

        fameText.text = fameAmt.ToString();
        creditsText.text = creditsAmt.ToString();
    }

    public void setActive(bool? fame = null, bool? credits = null)
    {
        if (fame != null)
            fameGroup.SetActive(fame.Value);
        if (credits != null)
            creditsGroup.SetActive(credits.Value);
    }
}
