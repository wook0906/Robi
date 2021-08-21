using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<PopupUI> _popupStack = new Stack<PopupUI>();
    SceneUI _sceneUI = null;

    Dictionary<System.Type, GameObject> _sceneUIs = new Dictionary<System.Type, GameObject>();
    Stack<SceneUI> _sceneUIStack = new Stack<SceneUI>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Extension.GetOrAddComponent<Canvas>(go);
        //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;

        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Extension.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Extension.GetOrAddComponent<T>(go);
    }

    public GameObject LoadSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Extension.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        _sceneUIs.Add(typeof(T), go);

        go.SetActive(false);
        return go;
    }

    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        GameObject go;
        if (!_sceneUIs.TryGetValue(typeof(T), out go))
        {
            Debug.LogError($"Not Find {typeof(T)}");
            return null;
        }

        T sceneUI = Extension.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);

        if (_sceneUIStack.Count > 0)
        {
            _sceneUIStack.Peek().gameObject.SetActive(false);
        }
        _sceneUIStack.Push(sceneUI);
        _sceneUI.gameObject.SetActive(true);
        _sceneUI.OnActive();

        return sceneUI;
    }
    public T GetSceneUI<T>() where T : SceneUI
    {
        if (_sceneUI is T)
            return _sceneUI as T;
        return null;
    }
    public T ShowPopupUI<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Extension.GetOrAddComponent<T>(go);
        

        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public T MakeDontDestroyPopupUI<T>(string name = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject root = GameObject.Find("DontDestroyObject");

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Extension.GetOrAddComponent<T>(go);
        
        go.transform.SetParent(root.transform);

        return popup;
    }

    public void ClosePopupUI(PopupUI popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        PopupUI popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void CloseSceneUI(SceneUI sceneUI)
    {
        if (_sceneUIStack.Count == 1)
            return;

        if (_sceneUIStack.Peek() != sceneUI)
        {
            Debug.Log("Close Scene Failed");
            return;
        }

        CloseSceneUI();
    }
    public void CloseSceneUI()
    {
        if (_sceneUIStack.Count == 1)
        {
            return;
        }

        SceneUI scene = _sceneUIStack.Pop();
        scene.gameObject.SetActive(false);

        _sceneUIStack.Peek().gameObject.SetActive(true);
    }

    public void Clear()
    {
        CloseAllPopupUI();

        _sceneUIStack.Clear();
        foreach (var item in _sceneUIs.Values)
        {
            if (item != null)
                Managers.Resource.Destroy(item);
        }
        _sceneUIs.Clear();
        _sceneUI = null;


    }

}
