using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private float radius;
    private Vector2 rightTopPos;

    private Camera cam;
    PlayerController player;

    Define.MonsterType spawnMonsterType;

    private IEnumerator Start()
    {
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float horizentalSize = Camera.main.orthographicSize * 2f * ((float)Screen.width / Screen.height);
        rightTopPos = new Vector2(horizentalSize * 0.5f, Camera.main.orthographicSize);
        //Debug.Log(rightTopPos);
        radius = rightTopPos.magnitude * 5f;
        cam = Camera.main;

        Managers.Game.MonsterSpawner = this;
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(3f);
        while(true)
        {
            //GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/{(Define.MonsterType)Random.Range((int)Define.MonsterType.C01,(int)Define.MonsterType.MAX)}");
            GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/F01");
            Vector3 pos = cam.transform.position + (Random.onUnitSphere * radius);
            pos.z = 0f;
            //pos += Vector3.right * radius;
            //pos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * pos;
            enemyGO.transform.position = pos;
            Managers.Object.AddMonster(enemyGO.GetComponent<MonsterController>());
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
    }
}
