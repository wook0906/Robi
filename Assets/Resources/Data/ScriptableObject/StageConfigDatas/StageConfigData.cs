using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StageConfigData")]
public class StageConfigData : ScriptableObject
{
    public Wave[] waves;
    public int termBetweenWaveToWave;
}
[Serializable]
public class Wave
{
    public int expPerMonster;
    public MonsterConfig[] monsterConfigs;
}
[Serializable]
public class MonsterConfig
{
    public Define.MonsterType mobType;
    public int numOfSpawn;
}
