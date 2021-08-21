using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool isReculsive = true)
    {
        return FindChild<Transform>(go, name, isReculsive).gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool isReculsive = true) where T : UnityEngine.Object
    {
        if (go == null)
        {
            Debug.LogWarning("Failed Find Child because go is null");
            return null;
        }

        if(isReculsive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name))
                    return component;

                if (component.name == name)
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                T component = go.transform.GetChild(i).GetComponent<T>();
                if (component == null)
                    continue;

                if (string.IsNullOrEmpty(name))
                    return component;

                if (component.name == name)
                    return component;
            }
        }
        return null;
    }
}
