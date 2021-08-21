using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Start()
    {
        Init();
    }
    public virtual void Init()
    {

    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = type.GetEnumNames();
        if (names.Length == 0)
        {
            Debug.LogWarning("UI Bind Nothing!");
            return;
        }

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            UnityEngine.Object obj;
            if(typeof(T) == typeof(GameObject))
            {
                obj = Util.FindChild(gameObject, names[i]);
            }
            else
            {
                obj = Util.FindChild<T>(gameObject, names[i]);
            }

            objects[i] = obj;
        }

        _objects.Add(typeof(T), objects);
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects;

        if(_objects.TryGetValue(typeof(T), out objects))
        {
            return objects[index] as T;
        }

        return null;
    }

    protected Slider GetSlider(int index) { return Get<Slider>(index); }
    protected GameObject GetGameObject(int index) { return Get<GameObject>(index); }
    protected Text GetText(int index) { return Get<Text>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
}
