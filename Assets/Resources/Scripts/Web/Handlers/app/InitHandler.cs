using Assets.Resources.Scripts.Web.Requests.app;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
    public class InitHandler : MonoBehaviour
    {
        public Button button;

        private Init init;

        private void Awake()
        {
            init = new Init();

            // button.interactable = false;
            button.onClick.AddListener(() =>
            {
                init.Configure();

                init.OnRequest();

                Debug.LogWarningFormat("Response from AppEngine request '{0}':\n{1}", init.GetRequest, init.OnResponse());
            });
        }
    }
}