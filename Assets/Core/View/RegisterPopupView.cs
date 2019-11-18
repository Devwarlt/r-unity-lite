using Assets.Core.Controller;
using Assets.Core.Controller.Handlers.app;
using Assets.Core.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.View
{
    public class RegisterPopupView : MonoBehaviour
    {
        [Header("Inputs")]
        public TMP_InputField emailInput;

        public TMP_InputField passwordInput;
        public TMP_InputField confirmPasswordInput;

        [Header("Buttons")]
        public Button cancelButton;

        public Button registerButton;

        [Header("Response Textbox")]
        public TextMeshProUGUI responseBox;

        private void InitializeGameObjects()
        {
            responseBox.gameObject.SetActive(false);
            cancelButton.onClick.AddListener(() => Destroy(gameObject));
            registerButton.onClick.AddListener(() => VerifyRegister());
        }

        private void Awake() =>
            InitializeGameObjects();

        private void VerifyRegister()
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

            if (!confirmPasswordInput.text.Equals(passwordInput.text))
            {
                responseBox.text = "<color=red>Passwords do not match!";
                return;
            }

            var register = new RegisterHandler(emailInput.text, passwordInput.text);

            if (register.SendRequest())
            {
                responseBox.text = "<color=green>Successfully Registered!";

                AccountController.set(emailInput.text, passwordInput.text);
                AccountController.onAccountChange();

                Destroy(gameObject);
            }
            else responseBox.text = "<color=red>Error!";
        }
    }
}
