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
    public static int charHDAStatCoefficient = 1;
    public static int charElseStatCoefficient = 40;
    public CharacterType SelectedCharacter
    {
        get 
        {
            if (PlayerPrefs.HasKey("Selected_Character"))
                return (CharacterType)PlayerPrefs.GetInt("Selected_Character");
            else
            {
                PlayerPrefs.SetInt("Selected_Character", (int)CharacterType.Robi);
                return CharacterType.Robi;
            }
        }
        set 
        {
            PlayerPrefs.SetInt("Selected_Character",(int)value);
        }
    }

    private int asset1 = 0;
    public int Asset1
    {
        get { return asset1; }
        set
        {
            PlayerPrefs.SetInt("Asset1", value);
            asset1 = value;
        }
    }
    
    private int asset2 = 0;
    public int Asset2
    {
        get { return asset2; }
        set 
        {
            PlayerPrefs.SetInt("Asset2", value);
            asset2 = value; 
        }
    }
    public Dictionary<Define.SkillType, SkillStatData> skillStatDict { get; private set; } = new Dictionary<SkillType, SkillStatData>();
    public Dictionary<Define.CharacterType, CharacterStatData> characterStatDict { get; private set; } = new Dictionary<CharacterType, CharacterStatData>();
    public Dictionary<Define.MonsterType, MonsterStatData> monsterStatDict { get; private set; } = new Dictionary<MonsterType, MonsterStatData>();

    public Dictionary<Define.SkillGrade, PassiveSkillCoefficientsData> passiveSkillCoefficientDict { get; private set; } = new Dictionary<SkillGrade, PassiveSkillCoefficientsData>();

    public Dictionary<Define.StageType, StageConfigData> stageConfigDataDict { get; private set; } = new Dictionary<StageType, StageConfigData>();

    public Dictionary<Define.CharacterType, CharacterStatUpGradeInfo> charStatUpgradeDict { get; private set; } = new Dictionary<CharacterType, CharacterStatUpGradeInfo>();
    

    public void Init()
    {
        for (SkillGrade i = SkillGrade.Common; i < SkillGrade.Max; i++)
        {
            passiveSkillCoefficientDict.Add(i, Managers.Resource.Load<PassiveSkillCoefficientsData>($"Data/ScriptableObject/PassiveSkillCoefficients/{i}"));
        }

        for (SkillType skillType = SkillType.PlayerNormal; skillType < SkillType.MonsterMax; skillType++)
        {
            skillStatDict.Add(skillType, Managers.Resource.Load<SkillStatData>($"Data/ScriptableObject/SkillStats/{skillType}"));
        }
        for (CharacterType charType = CharacterType.Robi; charType < CharacterType.MAX; charType++)
        {
            if (!PlayerPrefs.HasKey($"{charType}_HpUpgrade"))
                PlayerPrefs.SetInt($"{charType}_HpUpgrade", 0);
            if (!PlayerPrefs.HasKey($"{charType}_DefUpgrade"))
                PlayerPrefs.SetInt($"{charType}_DefUpgrade", 0);
            if (!PlayerPrefs.HasKey($"{charType}_AtkUpgrade"))
                PlayerPrefs.SetInt($"{charType}_AtkUpgrade", 0);
            if (!PlayerPrefs.HasKey($"{charType}_MoveSpeedUpgrade"))
                PlayerPrefs.SetInt($"{charType}_MoveSpeedUpgrade", 0);
            if (!PlayerPrefs.HasKey($"{charType}_RootRangeUpgrade"))
                PlayerPrefs.SetInt($"{charType}_RootRangeUpgrade", 0);
            if (!PlayerPrefs.HasKey($"{charType}_ExpUpgrade"))
                PlayerPrefs.SetInt($"{charType}_ExpUpgrade", 0);
        }
        for (CharacterType characterType = CharacterType.Robi; characterType < CharacterType.MAX; characterType++)
        {
            CharacterStatData originData = Managers.Resource.Load<CharacterStatData>($"Data/ScriptableObject/CharacterStats/{characterType}");
            CharacterStatData dataInstance = new CharacterStatData();
            dataInstance.characterType = originData.characterType;
            dataInstance._maxHp = originData._maxHp;
            dataInstance._atk = originData._atk;
            dataInstance._def = originData._def;
            dataInstance._moveSpeed = originData._moveSpeed;
            dataInstance._itemRootingRangeRadius = originData._itemRootingRangeRadius;
            dataInstance._expAcquirePercentage = originData._expAcquirePercentage;


            characterStatDict.Add(characterType, dataInstance);
            CharacterStatUpGradeInfo upgrageInfo = new CharacterStatUpGradeInfo();
            upgrageInfo.HpUpgrade = PlayerPrefs.GetInt($"{characterType}_HpUpgrade");
            upgrageInfo.DefUpgrade = PlayerPrefs.GetInt($"{characterType}_DefUpgrade");
            upgrageInfo.AtkUpgrade = PlayerPrefs.GetInt($"{characterType}_AtkUpgrade");
            upgrageInfo.MoveSpeedUpgrade = PlayerPrefs.GetInt($"{characterType}_MoveSpeedUpgrade");
            upgrageInfo.RootRangeUpgrade = PlayerPrefs.GetInt($"{characterType}_RootRangeUpgrade");
            upgrageInfo.ExpUpgrade = PlayerPrefs.GetInt($"{characterType}_ExpUpgrade");
            charStatUpgradeDict.Add(characterType, upgrageInfo);
        }
        for (MonsterType monsterType = MonsterType.C01; monsterType < MonsterType.MAX; monsterType++)
        {
            monsterStatDict.Add(monsterType, Managers.Resource.Load<MonsterStatData>($"Data/ScriptableObject/MonsterStats/{monsterType}"));
        }
        for (StageType i = StageType.Stage1; i < StageType.MaxCount; i++)
        {
            stageConfigDataDict.Add(i, Managers.Resource.Load<StageConfigData>($"Data/ScriptableObject/StageConfigDatas/{i}"));
        }


        if (PlayerPrefs.HasKey("Asset1"))
            Asset1 = PlayerPrefs.GetInt("Asset1");
        else
            PlayerPrefs.SetInt("Asset1", 0);

        if (PlayerPrefs.HasKey("Asset2"))
            Asset2 = PlayerPrefs.GetInt("Asset2");
        else
            PlayerPrefs.SetInt("Asset2", 0);
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    
}
