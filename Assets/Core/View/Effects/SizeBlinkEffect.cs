using UnityEngine;

namespace Assets.Core.View.Effects
{
    public class SizeBlinkEffect : MonoBehaviour
    {
        [Header("Values")]
        public float maxSize;

        public float minSize;
        public float duration;

        private Vector3 max;
        private Vector3 min;

        private float startTime;
        private bool growing;

        private void Start()
        {
            startTime = Time.time;
            growing = true;
            max = new Vector3(maxSize, maxSize, maxSize);
            min = new Vector3(minSize, minSize, minSize);
        }

        private void Update()
        {
            var scaleSize = transform.localScale.y;

            if (scaleSize >= max.y)
            {
                growing = false;
                startTime = (float)(Time.time - .01);
            }

            if (scaleSize <= min.y)
            {
                growing = true;
                startTime = (float)(Time.time - .01);
            }

            if (growing) grow();
            else shrink();
        }

        private void grow()
        {
            var transition = (Time.time - startTime) / duration;
            transform.localScale = Vector3.Lerp(min, max, transition);
        }

        private void shrink()
        {
            var transition = (Time.time - startTime) / duration;
            transform.localScale = Vector3.Lerp(max, min, transition);
        }
    }
}
