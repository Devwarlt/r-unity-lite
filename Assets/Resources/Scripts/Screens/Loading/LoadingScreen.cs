using Assets.Resources.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Button skipButton;

    private void InitializeGameObjects()
    {
        skipButton.onClick.AddListener(() => OnLoadingComplete());
    }

    private void Awake()
    {
        InitializeGameObjects();
    }

    public void OnLoadingComplete()
    {
        Utils.ChangeSceneAsync(GameScene.Main, LoadSceneMode.Single);
    }
}