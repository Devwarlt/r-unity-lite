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

        [Header("Top Buttons")]
        public Button loginButton;

        public Button registerButton;

        [Header("Bottom Buttons")]
        public Button serverButton;

        public Button playButton;
        public Button quitButton;

        private TextMeshProUGUI version;

        private void GetGameObjects()
        {
            version = Utils.GetComponentByTag<TextMeshProUGUI>(versionTag);
        }

        private void InitializeGameObjects()
        {
            version.text = version.text.Replace(versionKey, Application.version);

            //playButton.onClick.AddListener(() => Utils.ChangeSceneAsync(GameScene.CharacterSelect, LoadSceneMode.Additive));
            serverButton.onClick.AddListener(() => Utils.ChangeSceneAsync(GameScene.Servers, LoadSceneMode.Additive));
            quitButton.interactable = Application.platform.HasQuitSupport();
            quitButton.onClick.AddListener(() => Application.Quit());
            //registerButton.onClick.AddListener(() => open register gui);
            //loginButton.onClick.AddListener(() => open login gui);
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }
    }
}