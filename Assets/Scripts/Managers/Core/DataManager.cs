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
    
    public Dictionary<Define.SkillType, SkillStatData> skillStatDict { get; private set; } = new Dictionary<SkillType, SkillStatData>();
    public Dictionary<Define.CharacterType, CharacterStatData> characterStatDict { get; private set; } = new Dictionary<CharacterType, CharacterStatData>();
    public Dictionary<Define.MonsterType, MonsterStatData> monsterStatDict { get; private set; } = new Dictionary<MonsterType, MonsterStatData>();

    public Dictionary<Define.SkillGrade, PassiveSkillCoefficientsData> passiveSkillCoefficients { get; private set; } = new Dictionary<SkillGrade, PassiveSkillCoefficientsData>();

    public void Init()
    {
        for (SkillGrade i = SkillGrade.Common; i < SkillGrade.Max; i++)
        {
            passiveSkillCoefficients.Add(i, Managers.Resource.Load<PassiveSkillCoefficientsData>($"Data/ScriptableObject/PassiveSkillCoefficients/{i}"));
        }

        for (SkillType skillType = SkillType.PlayerNormal; skillType < SkillType.MonsterMax; skillType++)
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
