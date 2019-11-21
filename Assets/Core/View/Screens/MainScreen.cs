using Assets.Core.Controller;
using Assets.Core.Controller.Environments;
using Assets.Core.Controller.Handlers.app;
using Assets.Core.Utils.Monitoring;
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

        [Header("Menu Buttons")]
        public Button supportButton;

        public Button serverButton;
        public Button playButton;
        public Button accountButton;
        public Button legendsButton;

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

        private Text version;

        public void LoadAccount()
        {
            var isAccountNull = AccountController.account == null;

            serverButton.interactable = !isAccountNull;
            legendsButton.interactable = !isAccountNull;

            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(() =>
            {
                if (isAccountNull) OpenPopup(loginPopup);
                else playButton.onClick.AddListener(() => ChangeSceneAsync(GameScene.Play, LoadSceneMode.Additive));
            });

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

        private void Awake()
        {
            GetGameObjects();
            InitializeGameObjects();
            InitializeMenuButtons();
            InitializeTopButtons();
        }

        private void GetGameObjects() =>
            version = GetComponentByTag<Text>(versionTag);

        private void InitializeGameObjects()
        {
            version.text = "[ {ENVIRONMENT} ] {APPLICATION} - version: <color=yellow>{VERSION}</color>";
            version.text = version.text
                .Replace(environmentKey, GBE.GetEnvironment().GetName())
                .Replace(applicationKey, Application.productName)
                .Replace(versionKey, Application.version);

            if (Application.companyName.ToLower().Contains("lite")) Instantiate(litePrefab, transform);

            AccountController.onAccountChange += LoadAccount;
            AccountController.load();
        }

        private void InitializeMenuButtons()
        {
            supportButton.onClick.AddListener(() => Application.OpenURL(GBE.GetEnvironment().GetSupportLink()));
            serverButton.onClick.AddListener(() => ChangeSceneAsync(GameScene.Servers, LoadSceneMode.Additive));
            accountButton.onClick.AddListener(() => Log.Warn("Account Screen isn't implemented yet."));
            legendsButton.onClick.AddListener(() => Log.Warn("Legends Screen isn't implemented yet."));
        }

        private void InitializeTopButtons()
        {
            loginButton.onClick.AddListener(() => OpenPopup(loginPopup));
            registerButton.onClick.AddListener(() => OpenPopup(registerPopup));
            logoutButton.onClick.AddListener(() => AccountController.delete());
        }

        private void OpenPopup(GameObject popup) =>
            Instantiate(popup, transform);
    }
}
