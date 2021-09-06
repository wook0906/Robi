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

    Field field;

    private IEnumerator Start()
    {
        Managers.Game.MonsterSpawner = this;
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        field = GameObject.FindGameObjectWithTag("Field").GetComponent<Field>();
        float horizontalSize = Camera.main.orthographicSize * 2f * ((float)Screen.width / Screen.height);
        rightTopPos = new Vector2(horizontalSize * 0.5f, Camera.main.orthographicSize);
        radius = rightTopPos.magnitude * 5f;
        cam = Camera.main;

        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1f);
        while(true)
        {
           
            //GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/{(Define.MonsterType)Random.Range((int)Define.MonsterType.C01,(int)Define.MonsterType.MAX)}");
            GameObject enemyGO = Managers.Resource.Instantiate($"Creatures/Enemy/F01");
            Vector2 pos;
            do
            {
                pos = player.transform.position + (Random.onUnitSphere * radius * 2f);
            }
            while (pos.x > field.min.x &&
                    pos.y > field.min.y &&
                    pos.x < field.max.x &&
                    pos.y < field.max.y);
            //pos += Vector3.right * radius;
            //pos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * pos;
            enemyGO.transform.position = pos;
            Managers.Object.AddMonster(enemyGO.GetComponent<MonsterController>());
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
    }
}
