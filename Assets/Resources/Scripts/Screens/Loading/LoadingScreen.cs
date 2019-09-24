using Assets.Resources.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Button skip;

    [Header("Custom Dots effect")]
    public bool hasDotsAnimation = true;

    public TextMeshProUGUI loading;
    public string dotsKey;
    public float startsAnimationWithin = 0f;
    public float animationRepeatDelay = 0.5f;
    public int maximumDots = 3;

    private string loadingText;
    private int dots = 0;

    private void InitializeGameObjects()
    {
        skip.gameObject.SetActive(Application.platform.OnEditor());
        skip.interactable = false;
        skip.onClick.AddListener(() => Invoke("OnLoadingComplete", 1f));

        loadingText = loading.text;

        if (!hasDotsAnimation) loading.text = loadingText.Replace(dotsKey, "...");
        else InvokeRepeating("DotsAnimation", startsAnimationWithin, animationRepeatDelay);
    }

    private void Awake()
    {
        InitializeGameObjects();

        if (Application.platform.OnEditor()) skip.interactable = true;
        else Invoke("OnLoadingComplete", 1f);
    }

    private void OnLoadingComplete() => Utils.ChangeSceneAsync(GameScene.Main, LoadSceneMode.Single);

    private void DotsAnimation()
    {
        if (dots > maximumDots) dots = 0;

        loading.text = loadingText.Replace(dotsKey, Utils.RepeatCharByAmount('.', dots++));
    }
}