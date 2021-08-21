using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    //public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    //{
    //    UIBase.BindEvent(go, action, type);
    //}
}
