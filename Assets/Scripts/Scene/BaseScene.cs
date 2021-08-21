using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IScene
{
    float Progress { get; }
    bool IsDone { get; }
}

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown;
    //protected Dictionary<Type, UIScene> uiScenes = new Dictionary<Type, UIScene>();

    protected void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        UnityEngine.Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    public abstract void Clear();
}