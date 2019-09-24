using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Resources.Scripts.Web.Handlers.app;
using Assets.Resources.Scripts;

public class LoginPopup : MonoBehaviour
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
    	cancelButton.onClick.AddListener(() => Destroy(this.gameObject));
    	loginButton.onClick.AddListener(() => VerifyLogin());
    }

    void Awake()
    {
    	InitializeGameObjects();
    }

    private void VerifyLogin()
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

    	VerifyHandler verify = new VerifyHandler(emailInput.text, passwordInput.text);

    	if (verify.SendRequest())
    		responseBox.text = "<color=green>Successfully logged in!";
    	else
    		responseBox.text = "<color=red>Invalid Login!";
    }
}
