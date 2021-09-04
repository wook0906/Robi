﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerStat : CreatureStat
{
    [SerializeField]
    protected int _level;

    [SerializeField]
    protected float _atkCoefficient;
    [SerializeField]
    protected float _def;
    [SerializeField]
    protected float _exp;
    [SerializeField]
    protected float _expAssist;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected int _totalExp;
    private GameScene_UI gameSceneUI;
    float hpRecoveryTimeStamp = 0f;
    PlayerController owner;

    public int Level 
    {
        get { return _level; }
        set 
        { 
            _level = value;
            HpRecovery(MaxHp * owner.passiveSkill.skillDict[Define.SkillType.HPRecoveryAssistIncrease]);
        } 
    }

    public float Def
    {
        get 
        {
            float def = _def * (1 + owner.passiveSkill.skillDict[Define.SkillType.DEFIncrease]);
            return def;
        }
        set { _def = value; }
    }
    public float Exp
    {
        get { return _exp; }
        set
        {
            float inputValue = value;
            _exp = inputValue;

            int level = Level;

            gameSceneUI.UpdateExpUI((float)_exp / _totalExp);
            if (_exp < _totalExp)
                return;

            level++;

            _totalExp = RenewTotalExp(level);
            if (level != Level)
            {
                Managers.UI.ShowPopupUI<LevelUp_Popup>();
                gameSceneUI.UpdateLevelUI(level);
                
                Level = level;
                _exp = 0;
            }
        }
    }
    public float AtkCoefficient 
    {
        get {   return _atkCoefficient;} 
        set { _atkCoefficient = value; }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public override float MaxHp 
    {
        get { return _maxHp; }
        set
        {
            float maxHp = value * (1f + owner.passiveSkill.skillDict[Define.SkillType.MaxHPIncrease]);
            _maxHp = maxHp;
        }
    }
    public override float Hp
    {
        get { return _hp; }
        set
        {
            float hp = value;
            if (hp >= MaxHp)
                hp = MaxHp;
            _hp = hp;

            if(gameSceneUI)
                gameSceneUI.UpdateHpUI(_hp / _maxHp);
        }
    }
    
    public float HpRecoveryPerSecond 
    { 
        get 
        {
            float hpRecoveryPerSecond = MaxHp * owner.passiveSkill.skillDict[Define.SkillType.HPRecoveryPerSecIncrease];
            return hpRecoveryPerSecond;
        }
    }
    public float ExpAssist { get { return _expAssist; } set { _expAssist = value; } }
    private IEnumerator Start()
    {
        owner = GetComponent<PlayerController>();
        _level = 1;
        gameSceneUI = FindObjectOfType<GameScene_UI>();
        yield return new WaitUntil(() => gameSceneUI);
    }
    protected override void Update()
    {
        if (Time.time - hpRecoveryTimeStamp > 1f)
        {
            hpRecoveryTimeStamp = Time.time;
            HpRecovery(HpRecoveryPerSecond);
        }
        
    }


    public void SetStat()
    {
        Dictionary<Define.CharacterType, CharacterStatData> dict = Managers.Data.characterStatDict;
        CharacterStatData stat = dict[(Define.CharacterType)System.Enum.Parse(typeof(Define.CharacterType), name)];

        _hp = stat._maxHp;
        _maxHp = stat._maxHp;
        _def = stat._def;
        _atkCoefficient = stat._atk;
        _expAssist = stat._expAcquirePercentage;
        _moveSpeed = stat._moveSpeed;
        _totalExp = 110;
    }

    public override void OnAttacked(BaseController attacker, float damage)
    {
        float resultDamage = damage - Def;
        this.Hp -= resultDamage;

        if (this.Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }
    public void HpRecovery(float value)
    {
        Debug.Log($"Hp Recover {value * (1f + owner.passiveSkill.skillDict[Define.SkillType.HPRecoveryAmountIncrease])}");
        this.Hp += value * (1f + owner.passiveSkill.skillDict[Define.SkillType.HPRecoveryAmountIncrease]);
    }

    protected override void OnDead(BaseController attacker)
    {
        Debug.Log("Player Dead");
        Destroy(gameObject);
        Managers.Game.OnGameOver();
    }

    public void PassiveSkillLevelUp(Define.SkillType skill, Define.SkillGrade grade)
    {
        owner.passiveSkill.LevelUp(skill, grade);
    }

    //public override string ToString()
    //{
    //    StringBuilder builder = new StringBuilder();

    //    builder.Append($"Level:{Level} ");
    //    builder.Append($"HP:{MaxHp} ");
    //    builder.Append($"Damage:{Damage} ");
    //    builder.Append($"Speed:{MoveSpeed} ");
    //    builder.Append($"HpRecovery:{HpRecoveryPerSecond} ");
    //    builder.Append($"TotalExp:{Exp} ");

    //    return builder.ToString();
    //}
    int RenewTotalExp(int level)
    {
        int a = 1;
        int b = 1;
        int c = 0;
        int X = level * 10;
        return a * (X * X) + b * X + c;
    }
}

[Serializable]
public class PassiveSkills
{
    public Dictionary<Define.SkillType, float> skillDict = new Dictionary<Define.SkillType, float>();
    //{
    //    {Define.SkillType.ATKIncrease, 0f},
    //    {Define.SkillType.MaxHPIncrease, 0f},
    //    {Define.SkillType.DEFIncrease, 0f},
    //    {Define.SkillType.MoveSpeedIncrease, 0f},
    //    {Define.SkillType.ProjectileIncrease, 0f},
    //    {Define.SkillType.ExpIncrease, 0f},
    //    {Define.SkillType.CoolTimeIncrease, 0f},
    //    {Define.SkillType.RootRangeIncrease, 0f},
    //    {Define.SkillType.HPRecoveryAssistIncrease, 0f},
    //    {Define.SkillType.EnemyHpDownIncrease, 0f},
    //    {Define.SkillType.HPRecoveryAmountIncrease, 0f},
    //    {Define.SkillType.DurationIncrease, 0f},
    //    {Define.SkillType.RangeIncrease, 0f},
    //    {Define.SkillType.HPRecoveryPerSecIncrease, 0f},
    //};
    public PlayerStat _playerstat;

    public void Init()
    {
        for (Define.SkillType i = Define.SkillType.ATKIncrease; i < Define.SkillType.PlayerPassiveSkillMax; i++)
        {
            skillDict.Add(i, 0);
        }
    }
    public void LevelUp(Define.SkillType skillType, Define.SkillGrade skillGrade)
    {
        switch (skillType)
        {
            case Define.SkillType.ATKIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].ATKIncrease;
                
                //Check
                break;
            case Define.SkillType.MaxHPIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].MaxHPIncrease;
                _playerstat.MaxHp = _playerstat.MaxHp;
                break;
                //Check
            case Define.SkillType.DEFIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].DEFIncrease;
                _playerstat.Def += skillDict[skillType];
                //Check
                break;
            case Define.SkillType.MoveSpeedIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].MoveSpeedIncrease;
                _playerstat.MoveSpeed *= (1f + skillDict[skillType]);
                //Check
                break;
            case Define.SkillType.ProjectileIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].ProjectileIncrease;
                
                //Done
                break;
            case Define.SkillType.ExpIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].ExpIncrease;
                //Check
                break;
            case Define.SkillType.CoolTimeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].CoolTimeIncrease;
                //Check
                break;
            case Define.SkillType.RootRangeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].RootRangeIncrease;
                _playerstat.GetComponent<PlayerController>().SetItemRooterRange(1f + skillDict[skillType]);
                //Check
                break;
            case Define.SkillType.HPRecoveryAssistIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].HPRecoveryAssistIncrease;
                //Check
                break;
            case Define.SkillType.EnemyHpDownIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].EnemyHpDownIncrease;
                //Check
                break;
            case Define.SkillType.HPRecoveryAmountIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].HPRecoveryAmountIncrease;
                //Check
                break;
            case Define.SkillType.DurationIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].DurationIncrease;
                //Check
                break;
            case Define.SkillType.RangeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].RangeIncrease;
                //Check
                break;
            case Define.SkillType.HPRecoveryPerSecIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficientDict[skillGrade].HPRecoveryPerSecIncrease;
                //Check
                break;
            default:
                Debug.LogError($"Passive skill LevelUp Error");
                break;
        }
        Debug.Log($"{skillType} LevelUp, Currrent Value : {skillDict[skillType]}");
    }
    
    
    //public int GetNumOfCommon(Define.SkillType skillType)
    //{
    //    int cnt = 0;
    //    foreach (var item in passiveSkills[skillType])
    //    {
    //        if (item == Define.SkillGrade.Common)
    //            cnt++;
    //    }
    //    return cnt;
    //}
    //public int GetNumOfRare(Define.SkillType skillType)
    //{
    //    int cnt = 0;
    //    foreach (var item in passiveSkills[skillType])
    //    {
    //        if (item == Define.SkillGrade.Rare)
    //            cnt++;
    //    }
    //    return cnt;
    //}
    //public int GetNumOfUnique(Define.SkillType skillType)
    //{
    //    int cnt = 0;
    //    foreach (var item in passiveSkills[skillType])
    //    {
    //        if (item == Define.SkillGrade.Unique)
    //            cnt++;
    //    }
    //    return cnt;
    //}
}