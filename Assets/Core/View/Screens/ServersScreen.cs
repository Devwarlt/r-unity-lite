using Assets.Core.Controller;
using Assets.Core.Model.Server;
using Assets.Core.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Core.Utils.GU;

namespace Assets.Core.View.Screens
{
    public class ServersScreen : MonoBehaviour
    {
        public Button backButton;
        public GameObject serverItemList;
        public ServerItemModel serverItemPrefab;

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

                var serverItem = Instantiate(serverItemPrefab, serverItemList.transform);
                serverItem.GetComponent<ServerItemModel>().init(serv);
            }
        }
    }
}
