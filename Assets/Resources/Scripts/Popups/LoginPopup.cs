using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopup : MonoBehaviour
{
    [Header("Buttons")]
    public Button cancelButton;
    public Button loginButton;

    void Awake()
    {
    	cancelButton.onClick.AddListener(() => Destroy(this.gameObject));
    	//loginButton.onClick.AddListener(() => VerifyLogin());
    }
}
