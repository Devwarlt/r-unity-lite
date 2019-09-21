using TMPro;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class MainScreen : MonoBehaviour
    {
        [Header("Version Settings")]
        public string versionTag;

        public string versionKey;

        private TextMeshProUGUI version;

        private void GetGameObjects()
        {
            version = Utils.GetComponentByTag<TextMeshProUGUI>(versionTag);
        }

        private void InitializeGameObjects()
        {
            version.text = version.text.Replace(versionKey, Application.version);
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }
    }
}