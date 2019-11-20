using Assets.Core.Controller;
using Assets.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Core.Utils.GU;

namespace Assets.Core.View.Screens
{
    public class PlayScreen : MonoBehaviour
    {
        public Button backButton;

        [Header("AccountText")]
        public TextMeshProUGUI nameText;

        private void Awake()
        {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => UnloadSceneAsync(GameScene.Play.ToSceneName()));

            if (AccountController.account != null) nameText.text = AccountController.account.username;
        }
    }
}
