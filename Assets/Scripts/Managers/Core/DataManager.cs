using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.CreatureStat> PlayerStatDict { get; private set; } = new Dictionary<int, Data.CreatureStat>();
    public Dictionary<int, Data.CreatureStat> MonsterStatDict { get; private set; } = new Dictionary<int, Data.CreatureStat>();
    //public Dictionary<int, Data.AttackSkillStat>[] AttackSkillDicts { get; private set; } = new Dictionary<int, Data.AttackSkillStat>[(int)AttackSkillType.PlayerMax];

    public Dictionary<Define.AttackSkillType, SkillStatData> skillStatDict { get; private set; } = new Dictionary<AttackSkillType, SkillStatData>();

    public void Init()
    {
        PlayerStatDict = LoadJson<Data.CreatureStatData, int, Data.CreatureStat>("Creatures/PlayerStatData").MakeDict();
        MonsterStatDict = LoadJson<Data.CreatureStatData, int, Data.CreatureStat>("Creatures/MonsterStatData").MakeDict(); 

        int max = (int)AttackSkillType.MonsterMax;
        string[] attackTypeNames = typeof(AttackSkillType).GetEnumNames();
        //for(int i = 0; i < max; ++i)
        //{
        //    AttackSkillDicts[i] = LoadJson<Data.AttackSkillStatData, int, Data.AttackSkillStat>($"Skills/{attackTypeNames[i]}SkillStatData").MakeDict();
        //}

        for (AttackSkillType skillType = AttackSkillType.PlayerNormal; skillType < AttackSkillType.MonsterMax; skillType++)
        {
            skillStatDict.Add(skillType, Managers.Resource.Load<SkillStatData>($"ScriptableObject/SkillStats/{skillType}"));
        }

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
