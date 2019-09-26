using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeBlinkEffect : MonoBehaviour
{
    [Header("Values")]
    public float maxSize;
    public float minSize;
    public float duration;

    Vector3 max;
    Vector3 min;

    float startTime;
    bool growing;

    void Start()
    {
        startTime = Time.time;
        growing = true;
        max = new Vector3(maxSize, maxSize, maxSize);
        min = new Vector3(minSize, minSize, minSize);
    }

    void Update()
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

        if (growing)
            grow();
        else
            shrink();
    }

    void grow()
    {
        var transition = (Time.time - startTime) / duration;
        transform.localScale = Vector3.Lerp(min, max, transition);
    }
    
    void shrink()
    {
        var transition = (Time.time - startTime) / duration;
        transform.localScale = Vector3.Lerp(max, min, transition);
    }
}
