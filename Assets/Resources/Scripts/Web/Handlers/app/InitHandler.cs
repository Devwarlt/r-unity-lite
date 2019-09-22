using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts.Web.Handlers.app
{
    public class InitHandler : MonoBehaviour
    {
        public Button button;

        private GameWebRequest init;

        private void Awake()
        {
            init = new GameWebRequest(HttpMethod.Post, "app/init");

            button.interactable = true;
            button.onClick.AddListener(() =>
            {
                init.OnRequest();

                Debug.LogWarningFormat("Response from AppEngine request '{0}':\n{1}", init.request, init.OnResponse());
            });
        }
    }
}