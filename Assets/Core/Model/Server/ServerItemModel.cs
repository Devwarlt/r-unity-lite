using Assets.Core.Controller;
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
        public Text _name;

        public Text _usage;

        [HideInInspector]
        public ServerModel data;

        private bool isInitialized = false;

        public void init(ServerModel data)
        {
            this.data = data;

            _button.onClick.AddListener(() =>
            {
                ServerController.setSelectedServer(data.id);
                _background.SetActive(true);
            });

            ServerController.onServerChange += setup;

            setup();
        }

        public void setup()
        {
            _name.text = data.name;
            _usage.text = usage();

            isInitialized = true;
        }

        private string usage()
        {
            if (data.usage >= 1) return "<color=red>Full</color>";
            else if (data.usage >= .66) return "<color=orange>Crowded</color>";
            else return "<color=lime>Normal</color>";
        }

        private void LateUpdate()
        {
            if (isInitialized) _background.SetActive(data.id == ServerController.selectedId);
        }
    }
}
