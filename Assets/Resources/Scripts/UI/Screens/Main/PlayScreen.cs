using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Screens.Main {
    public class PlayScreen : MonoBehaviour {

        public Button backButton;

        [Header("AccountText")]
        public TextMeshProUGUI nameText;

        private void Awake() {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => Utils.UnloadSceneAsync(GameScene.Play.ToSceneName()));

            if (Account.account != null)
            {
                nameText.text = Account.account.username;
            }
        }
    }
}
