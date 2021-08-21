using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Time.timeScale = 1f;
        SceneType = Define.Scene.LobbyScene;

        StartCoroutine(LoadSceneUIs());
    }
    public override void Clear()
    {
        
    }
    IEnumerator LoadSceneUIs()
    {
        GameObject go = Managers.UI.LoadSceneUI<LobbyScene_UI>();
        yield return new WaitUntil(() => go);
        go = null;
        go = Managers.UI.LoadSceneUI<SelectCharacterScene_UI>();
        yield return new WaitUntil(() => go != null);
        go = null;
        go = Managers.UI.LoadSceneUI<StageSelectScene_UI>();
        yield return new WaitUntil(() => go != null);
        go = null;
        go = Managers.UI.LoadSceneUI<ModuleDescriptionScene_UI>();
        yield return new WaitUntil(() => go != null);
        go = null;

        Managers.UI.ShowSceneUI<LobbyScene_UI>();
    }
}
