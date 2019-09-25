using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Resources.Scripts.Web.Handlers.app;
using Assets.Resources.Scripts;
using Assets.Resources.Scripts.Screens.Main;

public class RegisterPopup : MonoBehaviour
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

    [Header("Storage")]
    public GOStorage goStorage;

    private void InitializeGameObjects()
    {
    	responseBox.gameObject.SetActive(false);
    	cancelButton.onClick.AddListener(() => Destroy(this.gameObject));
    	registerButton.onClick.AddListener(() => VerifyRegister());
    }

    void Awake()
    {
    	InitializeGameObjects();
    }

    private void VerifyRegister()
    {
    	responseBox.gameObject.SetActive(true);

    	if (!Utils.IsValidEmail(emailInput.text)) {
    		responseBox.text = "<color=red>Invalid email!";
    		return;
    	}
    	else if (string.IsNullOrEmpty(passwordInput.text)) {
    		responseBox.text = "<color=red>Please enter a password!";
    		return;
    	}
    	else if (!confirmPasswordInput.text.Equals(passwordInput.text)) {
    		responseBox.text = "<color=red>Passwords do not match!";
    		return;
    	}

    	RegisterHandler register = new RegisterHandler(emailInput.text, passwordInput.text);

    	if (register.SendRequest()) {
    	    responseBox.text = "<color=green>Successfully Registered!";
            Account.set(emailInput.text, passwordInput.text);
            (goStorage.get("screen") as MainScreen).loadAccount();
            Destroy(this.gameObject);
        }
    	else
    		responseBox.text = "<color=red>Error!";
    }
}
