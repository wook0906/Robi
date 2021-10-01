using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float minRadius = 15f;
    public float maxRadius = 25f;
    private Vector2 rightTopPos;

    private Camera cam;
    PlayerController player;

    Define.MonsterType spawnMonsterType;

    Field field;

    int curWaveStep = 0;

    private IEnumerator Start()
    {
        Managers.Game.MonsterSpawner = this;
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        field = GameObject.FindGameObjectWithTag("Field").GetComponent<Field>();
        //float horizontalSize = Camera.main.orthographicSize * 2f * ((float)Screen.width / Screen.height);
        //rightTopPos = new Vector2(horizontalSize * 0.5f, Camera.main.orthographicSize);
        cam = Camera.main;
        Define.StageType stageType = (Define.StageType)PlayerPrefs.GetInt("SelectedMap");
        StageConfigData stageData =  Managers.Data.stageConfigDataDict[stageType];
        StartCoroutine(Wave(stageData));
    }

    private IEnumerator Wave(StageConfigData stageData)
    {
        yield return new WaitForSeconds(stageData.termBetweenWaveToWave);

        for (int i = 0; i < stageData.waves.Length; i++)
        {
            Managers.UI.GetSceneUI<GameScene_UI>().UpdateWaveLevelUI(curWaveStep + 1);
            GameScene gameScene = Managers.Scene.CurrentScene as GameScene;
            gameScene.currentWaveLevel = curWaveStep;
            for (int monsterConfigIdx = 0; monsterConfigIdx < stageData.waves[curWaveStep].monsterConfigs.Length; monsterConfigIdx++)
            {
                for (int j = 0; j < stageData.waves[curWaveStep].monsterConfigs[monsterConfigIdx].numOfSpawn; j++)
                {
                    //GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/{(Define.MonsterType)Random.Range((int)Define.MonsterType.C01,(int)Define.MonsterType.MAX)}");
                    GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/{stageData.waves[curWaveStep].monsterConfigs[monsterConfigIdx].mobType}");
                    Vector2 pos;
                    do
                    {
                        pos = player.transform.position + (Random.onUnitSphere * Random.Range(minRadius,maxRadius));
                    }
                    //while (pos.x < field.min.x &&
                    //        pos.y < field.min.y &&
                    //        pos.x > field.max.x &&
                    //        pos.y > field.max.y);
                    while (Vector3.Distance(pos,player.transform.position) < minRadius);
                    //pos += Vector3.right * radius;
                    //pos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * pos;

                    enemyGO.transform.position = pos;
                    Managers.Object.AddMonster(enemyGO.GetComponent<MonsterController>());
                }
            }
            yield return new WaitUntil(() => Managers.Object.IsAllMonsterDead());
            yield return new WaitForSeconds(stageData.termBetweenWaveToWave);
            curWaveStep++;
        }
    }
    public void Clear()
    {
        StopAllCoroutines();
    }
}
