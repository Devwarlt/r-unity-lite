using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Core.Utils.GW;

namespace Assets.Core.Controller.Handlers.app
{
    public class InitHandler : MonoBehaviour
    {
        public Button button;

        private Request init;

        private void Awake()
        {
            init = new Request(HttpMethod.Post, "app/init");

            button.interactable = true;
            button.onClick.AddListener(() =>
            {
                init.OnRequest();

                Debug.LogWarningFormat("Response from AppEngine request '{0}':\n{1}", init.request, init.OnResponse());
            });
        }
    }
}
