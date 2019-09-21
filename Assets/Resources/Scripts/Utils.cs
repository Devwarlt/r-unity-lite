using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Resources.Scripts
{
    public static class Utils
    {
        public static void UnloadSceneAsync(string name) => SceneManager.UnloadSceneAsync(name);

        public static void ChangeSceneAsync(GameScene scene, LoadSceneMode mode) => SceneManager.LoadSceneAsync(scene.ToSceneName(), mode);

        public static string ToSceneName(this GameScene scene) => scene.ToString() + "Scene";

        public static T GetComponentByTag<T>(string tag) => GameObject.FindWithTag(tag).GetComponent<T>();
    }
}