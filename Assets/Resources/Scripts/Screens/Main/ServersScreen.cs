using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Screens.Main
{
    public class ServersScreen : MonoBehaviour
    {
        public Button backButton;

        private void Awake()
        {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => Utils.UnloadSceneAsync(GameScene.Servers.ToSceneName()));
        }
    }
}