using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Screens.Main
{
    public class MainScreen : MonoBehaviour
    {
        [Header("Version Settings")]
        public string versionTag;

        public string versionKey;

        [Header("Buttons")]
        public Button serverButton;

        private TextMeshProUGUI version;

        private void GetGameObjects()
        {
            version = Utils.GetComponentByTag<TextMeshProUGUI>(versionTag);
        }

        private void InitializeGameObjects()
        {
            version.text = version.text.Replace(versionKey, Application.version);

            serverButton.interactable = true;
            serverButton.onClick.AddListener(() => Utils.ChangeSceneAsync(GameScene.Servers, LoadSceneMode.Additive));
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }
    }
}