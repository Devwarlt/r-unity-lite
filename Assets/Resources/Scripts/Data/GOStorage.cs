using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOStorage : MonoBehaviour
{
    public Dictionary<string, object> dict;

    void Awake()
    {
        InitializeGameObjects();
    }

    private void InitializeGameObjects()
    {
        dict = new Dictionary<string, object>();
    }

    public object get(string name)
    {
        if (!dict.ContainsKey(name))
            return null;
        return dict[name];
    }

    public void set(string name, object obj)
    {
        if (dict.ContainsKey(name))
            dict[name] = obj;
        else
            dict.Add(name, obj);
    }
}
