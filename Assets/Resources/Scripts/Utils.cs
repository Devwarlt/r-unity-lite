using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

namespace Assets.Resources.Scripts
{
    public static class Utils
    {
        public static bool HasQuitSupport(this RuntimePlatform platform)
            => platform != RuntimePlatform.WebGLPlayer && !platform.OnEditor();

        public static bool OnEditor(this RuntimePlatform platform)
            => platform == RuntimePlatform.LinuxEditor || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.WindowsEditor;

        public static void UnloadSceneAsync(string name) => SceneManager.UnloadSceneAsync(name);

        public static void ChangeSceneAsync(GameScene scene, LoadSceneMode mode) => SceneManager.LoadSceneAsync(scene.ToSceneName(), mode);

        public static string ToSceneName(this GameScene scene) => scene.ToString() + "Scene";

        public static T GetComponentByTag<T>(string tag) => GameObject.FindWithTag(tag).GetComponent<T>();

        public static bool IsValidEmail(string email) => Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", RegexOptions.IgnoreCase);
    }
}