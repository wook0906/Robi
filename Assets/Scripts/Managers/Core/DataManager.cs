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
    //public Dictionary<int, Data.CreatureStat> PlayerStatDict { get; private set; } = new Dictionary<int, Data.CreatureStat>();
    //public Dictionary<int, Data.CreatureStat> MonsterStatDict { get; private set; } = new Dictionary<int, Data.CreatureStat>();
    //public Dictionary<int, Data.AttackSkillStat>[] AttackSkillDicts { get; private set; } = new Dictionary<int, Data.AttackSkillStat>[(int)AttackSkillType.PlayerMax];

    public Dictionary<Define.AttackSkillType, SkillStatData> skillStatDict { get; private set; } = new Dictionary<AttackSkillType, SkillStatData>();
    public Dictionary<Define.CharacterType, CharacterStatData> characterStatDict { get; private set; } = new Dictionary<CharacterType, CharacterStatData>();
    public Dictionary<Define.MonsterType, MonsterStatData> monsterStatDict { get; private set; } = new Dictionary<MonsterType, MonsterStatData>();

    public void Init()
    {
        //PlayerStatDict = LoadJson<Data.CreatureStatData, int, Data.CreatureStat>("Creatures/PlayerStatData").MakeDict();
        //MonsterStatDict = LoadJson<Data.CreatureStatData, int, Data.CreatureStat>("Creatures/MonsterStatData").MakeDict(); 

        //int max = (int)AttackSkillType.MonsterMax;
        //string[] attackTypeNames = typeof(AttackSkillType).GetEnumNames();
        //for(int i = 0; i < max; ++i)
        //{
        //    AttackSkillDicts[i] = LoadJson<Data.AttackSkillStatData, int, Data.AttackSkillStat>($"Skills/{attackTypeNames[i]}SkillStatData").MakeDict();
        //}

        for (AttackSkillType skillType = AttackSkillType.PlayerNormal; skillType < AttackSkillType.MonsterMax; skillType++)
        {
            skillStatDict.Add(skillType, Managers.Resource.Load<SkillStatData>($"Data/ScriptableObject/SkillStats/{skillType}"));
        }

        for (CharacterType characterType = CharacterType.Robi; characterType < CharacterType.MAX; characterType++)
        {
            characterStatDict.Add(characterType, Managers.Resource.Load<CharacterStatData>($"Data/ScriptableObject/CharacterStats/{characterType}"));
        }
        for (MonsterType monsterType = MonsterType.C01; monsterType < MonsterType.MAX; monsterType++)
        {
            monsterStatDict.Add(monsterType, Managers.Resource.Load<MonsterStatData>($"Data/ScriptableObject/MonsterStats/{monsterType}"));
        }



    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
