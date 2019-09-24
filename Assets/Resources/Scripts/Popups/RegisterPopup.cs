using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Resources.Scripts.Web.Handlers.app;
using Assets.Resources.Scripts;

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
    		// some pserver sources require an age verification request, i'll leave this here
    		// VerifyAgeHandler verifyAgeHandler = new VerifyAgeHandler(emailInput.text, passwordInput.text);
    		// if (!verifyAgeHandler.SendRequest()) 
    		// 	responseBox.text = "<color=red>Error while verifying account age!";
    		// else
    			responseBox.text = "<color=green>Successfully Registered!";
    	}
    	else
    		responseBox.text = "<color=red>Error!";
    }
}
