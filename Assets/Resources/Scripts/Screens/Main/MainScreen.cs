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

        [Header("Bottom Buttons")]
        public Button serverButton;
        public Button playButton;
        public Button quitButton;

        [Header("Popups")]
        public GameObject loginPopup;
        public GameObject registerPopup;

        [Header("TopRight Groups")]
        public GameObject loggedInGroup;
        public GameObject loggedOutGroup;

        [Header("LoggedIn Buttons")]
        public Button loginButton;
        public Button registerButton;

        [Header("LoggedOut Buttons")]
        public Button logoutButton;
        public TextMeshProUGUI nameText;

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

            loadAccount();
            loginButton.onClick.AddListener(() => OpenPopup(loginPopup));
            registerButton.onClick.AddListener(() => OpenPopup(registerPopup));
            logoutButton.onClick.AddListener(() => logOut());
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }

        public void loadAccount()
        {
            var loggedIn = Account.load();
            loggedInGroup.SetActive(false);
            loggedOutGroup.SetActive(false);

            if (loggedIn)
            {
                loggedInGroup.SetActive(true);
                nameText.text = Account.data.guid;
            }
            else
            {
                loggedOutGroup.SetActive(true);
            }
        }

        public void logOut()
        {
            Account.delete();
            loadAccount();
        }

        private void OpenPopup(GameObject popup)
        {
            GameObject go = Instantiate(popup, this.transform);
            (go.GetComponent(typeof(GOStorage)) as GOStorage).set("screen", this);
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