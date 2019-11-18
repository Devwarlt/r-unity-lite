using Assets.Core.Controller;
using Assets.Core.Controller.Handlers.app;
using Assets.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.View
{
    public class LoginPopupView : MonoBehaviour
    {
        [Header("Inputs")]
        public TMP_InputField emailInput;

        public TMP_InputField passwordInput;

        [Header("Buttons")]
        public Button cancelButton;

        public Button loginButton;

        [Header("Response Textbox")]
        public TextMeshProUGUI responseBox;

        private void InitializeGameObjects()
        {
            responseBox.gameObject.SetActive(false);
            cancelButton.onClick.AddListener(() => Destroy(gameObject));
            loginButton.onClick.AddListener(() => VerifyLogin());
        }

        private void Awake() =>
            InitializeGameObjects();

        private void VerifyLogin()
        {
            responseBox.gameObject.SetActive(true);

            if (!GU.IsValidEmail(emailInput.text))
            {
                responseBox.text = "<color=red>Invalid email!";
                return;
            }

            if (string.IsNullOrEmpty(passwordInput.text))
            {
                responseBox.text = "<color=red>Please enter a password!";
                return;
            }

            var verify = new VerifyHandler(emailInput.text, passwordInput.text);

            if (verify.SendRequest())
            {
                responseBox.text = "<color=green>Successfully logged in!";

                AccountController.set(emailInput.text, passwordInput.text);
                AccountController.onAccountChange();

                Destroy(gameObject);
            }
            else responseBox.text = "<color=red>Invalid Login!";
        }
    }
}
