using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Screens.Main {
    public class PlayScreen : MonoBehaviour {

        public Button backButton;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI fameText;
        public TextMeshProUGUI creditsText;

        public static string Name;
        public static string Fame;
        public static string Credits;

        public static void Init(AccountData acc) {
            if (acc is null) {
                Debug.Log("Account not found.");
                return;
            }
            Name = acc.Name;
            Fame = acc.Fame.ToString();
            Credits = acc.Credits.ToString();
        }

        private void Awake() {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => Utils.UnloadSceneAsync(GameScene.Play.ToSceneName()));

            nameText.text = Name;
            fameText.text = $"Fame: {Fame}";
            creditsText.text = $"Gold: {Credits}";
        }
    }
}
