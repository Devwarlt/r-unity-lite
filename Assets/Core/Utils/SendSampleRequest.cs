using Assets.Core.Controller.Handlers.app;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Utils
{
    public sealed class SendSampleRequest : MonoBehaviour
    {
        public Button button;

        private InitHandler request;

        private void Awake()
        {
            request = new InitHandler();

            button.interactable = true;
            button.onClick.AddListener(() => request.SendRequest());
        }
    }
}
