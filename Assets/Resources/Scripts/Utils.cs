using UnityEngine;

namespace Assets.Resources.Scripts
{
    public static class Utils
    {
        public static T GetComponentByTag<T>(string tag) => GameObject.FindWithTag(tag).GetComponent<T>();
    }
}