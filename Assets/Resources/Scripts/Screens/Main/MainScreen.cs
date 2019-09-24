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

        [Header("Popups")]
        public GameObject loginPopup;
        public GameObject registerPopup;

        private TextMeshProUGUI version;

        private void GetGameObjects()
        {
            version = Utils.GetComponentByTag<TextMeshProUGUI>(versionTag);
        }

        private void InitializeGameObjects()
        {
            version.text = version.text.Replace(versionKey, Application.version);

            //playButton.onClick.AddListener(() => Utils.ChangeSceneAsync(GameScene.CharacterSelect, LoadSceneMode.Additive));
            loginButton.onClick.AddListener(() => OpenPopup(loginPopup));
            registerButton.onClick.AddListener(() => OpenPopup(registerPopup));
            serverButton.onClick.AddListener(() => Utils.ChangeSceneAsync(GameScene.Servers, LoadSceneMode.Additive));
            quitButton.interactable = Application.platform.HasQuitSupport();
            quitButton.onClick.AddListener(() => Application.Quit());
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }

        private void OpenPopup(GameObject popup)
        {
            GameObject go = Instantiate(popup, this.transform);
            //go.onDisable.AddListener(() => EnableButtons());
            //DisableButtons();
        }

        private void DisableButtons()
        {
            playButton.interactable = false;
            serverButton.interactable = false;
            quitButton.interactable = false;
            loginButton.interactable = false;
            registerButton.interactable = false;
        }

        private void EnableButtons()
        {
            playButton.interactable = true;
            serverButton.interactable = true;
            quitButton.interactable = Application.platform.HasQuitSupport();
            loginButton.interactable = true;
            registerButton.interactable = true;
        }
    }
}