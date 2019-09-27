using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Screens.Main
{
    public class ServersScreen : MonoBehaviour
    {
        public GameObject server;
        public GameObject serverGroup;
        public Button backButton;

        private void Awake()
        {
            backButton.interactable = true;
            backButton.onClick.AddListener(() => Utils.UnloadSceneAsync(GameScene.Servers.ToSceneName()));

            load();
        }

        private void load()
        {
            if (Servers.servers == null)
                return;

            foreach (var serv in Servers.servers)
            {
                if (serv.adminonly && !Account.account.admin)
                    continue;

                GameObject go = Instantiate(server, serverGroup.transform);
                (go.GetComponent(typeof(ServerControl)) as ServerControl).init(serv);
            }
        }
    }
}