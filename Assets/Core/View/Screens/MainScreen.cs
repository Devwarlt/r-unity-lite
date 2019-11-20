using Assets.Core.Controller;
using Assets.Core.Controller.Environments;
using Assets.Core.Controller.Handlers.app;
using Assets.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.Core.Utils.GU;

namespace Assets.Core.View.Screens
{
    public class MainScreen : MonoBehaviour
    {
        [Header("Version Settings")]
        public string versionTag;

        public string environmentKey;
        public string applicationKey;
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

        [Header("Lite Tag")]
        public GameObject litePrefab;

        private TextMeshProUGUI version;

        private void GetGameObjects() =>
            version = GetComponentByTag<TextMeshProUGUI>(versionTag);

        private void InitializeGameObjects()
        {
            version.text = "[ <u>{ENVIRONMENT}</u> ] {APPLICATION} - version: <color=yellow><b>{VERSION}</b></color>";
            version.text = version.text
                .Replace(environmentKey, GBE.GetEnvironment().GetName())
                .Replace(applicationKey, Application.productName)
                .Replace(versionKey, Application.version);

            playButton.onClick.AddListener(() => ChangeSceneAsync(GameScene.Play, LoadSceneMode.Additive));
            serverButton.onClick.AddListener(() => ChangeSceneAsync(GameScene.Servers, LoadSceneMode.Additive));
            quitButton.interactable = Application.platform.HasQuitSupport();
            quitButton.onClick.AddListener(() => Application.Quit());
            loginButton.onClick.AddListener(() => OpenPopup(loginPopup));
            registerButton.onClick.AddListener(() => OpenPopup(registerPopup));
            logoutButton.onClick.AddListener(() => AccountController.delete());

            if (Application.companyName.ToLower().Contains("lite")) Instantiate(litePrefab, transform);

            AccountController.onAccountChange += loadAccount;
            AccountController.load();
        }

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
        }

        public void loadAccount()
        {
            loggedInGroup.SetActive(false);
            loggedOutGroup.SetActive(false);

            if (AccountController.verify())
            {
                loggedInGroup.SetActive(true);

                var charList = new CharListHandler(AccountController.credentials.guid, AccountController.credentials.password);
                charList.SendRequest();
                charList.load();

                nameText.text = AccountController.account.username;
            }
            else
            {
                if (AccountController.credentials != null) AccountController.delete();

                loggedOutGroup.SetActive(true);
            }
        }

        private void OpenPopup(GameObject popup) =>
            Instantiate(popup, transform);

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
