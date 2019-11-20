using Assets.Core.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Model.Server
{
    public class ServerItemModel : MonoBehaviour
    {
        [Header("Objects")]
        public GameObject _background;

        public Button _button;

        [Header("Text")]
        public TextMeshProUGUI _name;

        public TextMeshProUGUI _usage;

        [HideInInspector]
        public ServerModel data;

        public void init(ServerModel data)
        {
            this.data = data;

            _button.onClick.AddListener(() => ServerController.setSelectedServer(data.id));

            ServerController.onServerChange += setup;

            setup();
        }

        public void setup()
        {
            _name.text = data.name;
            _usage.text = usage();

            var color = data.id == ServerController.selectedId ? (byte)255 : (byte)0;

            if (_background != null) _background.GetComponent<Image>().color = new Color32(color, color, color, 100);
        }

        private string usage()
        {
            if (data.usage >= 1) return "<color=red>Full";
            if (data.usage >= .66) return "<color=orange>Crowded";

            return "";
        }
    }
}
