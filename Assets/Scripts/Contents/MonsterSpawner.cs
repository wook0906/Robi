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

    #region 풀링 제작 몬스터가 안나올시, <p_PoolCOunt> 값을변경하여 조절
    [System.Serializable]
    public class PoolingClass
    {
        public Define.MonsterType m_Type;
        public List<GameObject> m_ObjList = new List<GameObject>();
    }
    private int p_PoolCount = 100;
    public List<PoolingClass> m_PoolList = new List<PoolingClass>();
    #endregion

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

        CreatePool();
        StartCoroutine(Wave(stageData));
    }

    private void CreatePool()
    {
        m_PoolList = new List<PoolingClass>();
        for (int i = 0; i < (int)Define.MonsterType.MAX; i++)
        {
            PoolingClass _item = new PoolingClass();
            _item.m_Type = Define.MonsterType.C01 + i;
            for (int j = 0; j < p_PoolCount; j++)
            {
                GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/{Define.MonsterType.C01 + i}");
                if(enemyGO != null)
                {
                    enemyGO.transform.position = new Vector3(9999, 9999, 9999);
                    enemyGO.transform.parent = this.transform;
                    enemyGO.gameObject.SetActive(false);
                    _item.m_ObjList.Add(enemyGO);
                }
            }

            m_PoolList.Add(_item);
        }
    }

    private GameObject ReturnPool(Define.MonsterType _type)
    {
        for (int i = 0; i < m_PoolList.Count; i++)
        {
            if(m_PoolList[i].m_Type == _type)
            {
                for (int j = 0; j < m_PoolList[i].m_ObjList.Count; j++)
                {
                    if (!m_PoolList[i].m_ObjList[j].activeSelf)
                    {
                        return m_PoolList[i].m_ObjList[j];
                    }
                }
            }
        }

        return null;
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
                    GameObject enemyGO = null;

                    while (true)
                    {
                        Debug.Log("기달기달");
                        enemyGO = ReturnPool(stageData.waves[curWaveStep].monsterConfigs[monsterConfigIdx].mobType);
                        if(enemyGO != null) break;
                        yield return null;
                    }

                    Vector2 pos;
                    do
                    {
                        pos = player.transform.position + (Random.onUnitSphere * Random.Range(minRadius, maxRadius));
                    }
                    //while (pos.x < field.min.x &&
                    //        pos.y < field.min.y &&
                    //        pos.x > field.max.x &&
                    //        pos.y > field.max.y);
                    while (Vector3.Distance(pos, player.transform.position) < minRadius);
                    //pos += Vector3.right * radius;
                    //pos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * pos;

                    enemyGO.transform.position = pos;
                    Managers.Object.AddMonster(enemyGO.GetComponent<MonsterController>());
                    enemyGO.SetActive(true);
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
