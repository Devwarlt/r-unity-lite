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
    public TMP_InputField confirmPassword;

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
    	
    }
}
