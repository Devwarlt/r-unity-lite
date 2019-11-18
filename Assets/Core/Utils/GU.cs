using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Core.Utils
{
    public static partial class GU
    {
        public static void ChangeSceneAsync(GameScene scene, LoadSceneMode mode) =>
            SceneManager.LoadSceneAsync(scene.ToSceneName(), mode);

        public static int ComputeFileID(Type type, string name)
        {
            var toBeHashed = "s\0\0\0" + type.Namespace + type.Name + name;

            using (var hash = new MD4())
            {
                var hashed = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(toBeHashed));
                var result = 0;

                for (var i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
            }
        }

        public static T GetComponentByTag<T>(string tag) =>
            GameObject.FindWithTag(tag).GetComponent<T>();

        public static bool HasQuitSupport(this RuntimePlatform platform) =>
            platform != RuntimePlatform.WebGLPlayer && !platform.OnEditor();

        public static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", RegexOptions.IgnoreCase);

        public static bool OnEditor(this RuntimePlatform platform) =>
            platform == RuntimePlatform.LinuxEditor || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.WindowsEditor;

        public static string RepeatCharByAmount(char c, int amount) =>
            new string(c, amount);

        public static string ToSceneName(this GameScene scene) =>
            scene.ToString() + "Scene";

        public static void UnloadSceneAsync(string name) =>
            SceneManager.UnloadSceneAsync(name);
    }
}
