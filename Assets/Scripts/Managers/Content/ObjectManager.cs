using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private List<MonsterController> _monsters = new List<MonsterController>();
    public List<MonsterController> Monsters { get { return _monsters; } }

    private List<MonsterController> _monstersInScreen = new List<MonsterController>();
    public List<MonsterController> MonstersInScreen { get { return _monstersInScreen; } }

    public void AddMonsterInScreen(MonsterController mc)
    {
        if (_monstersInScreen.Contains(mc))
            return;

        Debug.Log("Add MonsterInScreen");
        _monstersInScreen.Add(mc);
    }

    public void RemoveMonsterInScreen(MonsterController mc)
    {
        if (!_monstersInScreen.Contains(mc))
            return;

        Debug.Log("Remove MonsterInScreen");
        _monstersInScreen.Remove(mc);
    }

    public void AddMonster(MonsterController mc)
    {
        if (_monsters.Contains(mc))
            return;

        //Debug.Log("Add Monster");
        _monsters.Add(mc);
    }

    public void RemoveMonster(MonsterController mc)
    {
        if (!_monsters.Contains(mc))
            return;

        RemoveMonsterInScreen(mc);
        //Debug.Log("Remove Monster");
        _monsters.Remove(mc);
    }
    public void Clear()
    {
        _monsters.Clear();
        _monstersInScreen.Clear();
    }
    public bool IsAllMonsterDead()
    {
        if(_monsters.Count  ==  0 && _monstersInScreen.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void MonsterLevelUp(int playerLevel)
    {
        foreach (var item in _monsters)
        {
            if (item)
            {
                MonsterStat stat = item.Stat as MonsterStat;
                stat.LevelUp(playerLevel);
            }
        }
        foreach (var item in _monstersInScreen)
        {
            if (item)
            {
                MonsterStat stat = item.Stat as MonsterStat;
                stat.LevelUp(playerLevel);
            }
        }
    }
}
