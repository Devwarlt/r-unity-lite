using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyControl : MonoBehaviour
{
    public TextMeshProUGUI _fame;
    public TextMeshProUGUI _credits;

    bool set = false;

    void Update()
    {
        if (set)
            return;

        if (Account.account == null)
            return;

        _fame.text = Account.account.stats.fame.ToString();
        _credits.text = Account.account.credits.ToString();
        set = true;
    }
}
