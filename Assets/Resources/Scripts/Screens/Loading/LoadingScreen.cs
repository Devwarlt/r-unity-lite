using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Resources.Scripts;

public class LoadingScreen : MonoBehaviour
{
	public Button skipButton;

    private void InitializeGameObjects()
    {
        skipButton.onClick.AddListener(() => OnLoadingComplete());
    }

    void Awake()
    {
    	InitializeGameObjects();
    }

    public void OnLoadingComplete()
    {
    	Utils.ChangeSceneAsync(GameScene.Main, LoadSceneMode.Single);
    }
}
