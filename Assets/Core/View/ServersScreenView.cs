using Assets.Core.Controller;
using Assets.Core.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Core.Utils.GU;

namespace Assets.Core.View
{
    public class ServersScreenView : MonoBehaviour
    {
        public Button backButton;
        public GameObject server;
        public GameObject serverGroup;

        private void Awake()
        {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => UnloadSceneAsync(GameScene.Servers.ToSceneName()));

            load();
        }

        private void load()
        {
            if (ServerController.servers == null) return;

            foreach (var serv in ServerController.servers)
            {
                if (serv.adminonly && !AccountController.account.admin) continue;

                var go = Instantiate(server, serverGroup.transform);
                go.GetComponent<ServerView>().init(serv);
            }
        }
    }
}
