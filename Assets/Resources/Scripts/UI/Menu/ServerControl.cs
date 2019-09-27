using Assets.Resources.Scripts.Screens.Main;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerControl : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _usage;

    [Header("Objects")]
    public GameObject _background;
    public Button _button;

    [HideInInspector()]
    public ServerData data;

    public void init(ServerData data)
    {
        this.data = data;
        _button.onClick.AddListener(() => Servers.setSelectedServer(data.id));
        Servers.onServerChange += setup;
        setup();
    }

    public void setup()
    {
        _name.text = data.name;
        _usage.text = usage();
        byte color = data.id == Servers.selectedId ? (byte)255 : (byte)0;
        if (_background != null)
            _background.GetComponent<Image>().color = new Color32(color, color, color, 100);
    }

    private string usage()
    {
        if (data.usage >= 1)
            return "<color=red>Full";
        if (data.usage >= .66)
            return "<color=orange>Crowded";
        return "";
    }
}
