using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    GameObject player;
    GameObject field;
    GameObject sceneUI;
    GameObject monsterSpawner;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;
        Managers.Pool.CreatePool(Managers.Resource.Load<GameObject>("Prefabs/Effects/Explosion"), 20);
        Managers.Pool.CreatePool(Managers.Resource.Load<GameObject>("Prefabs/Effects/ExplosionMark"), 30);

        StartCoroutine(CoInit());
    }
    IEnumerator CoInit()
    {
        sceneUI = Managers.UI.LoadSceneUI<GameScene_UI>();
        yield return new WaitUntil(() => sceneUI);
        Managers.UI.ShowSceneUI<GameScene_UI>();
        field = Managers.Resource.Instantiate("Core/Field");
        yield return new WaitUntil(() => field);
        monsterSpawner = Managers.Resource.Instantiate("Core/MonsterSpawner");
        yield return new WaitUntil(() => monsterSpawner);

        player = Managers.Resource.Instantiate("Core/Player");
        yield return new WaitUntil(() => player);
    }
    public override void Clear()
    {
       
    }
}
