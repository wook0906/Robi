using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerStat : CreatureStat
{
    [SerializeField]
    protected float _atkCoefficient;
    [SerializeField]
    protected float _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected int _totalExp;

    public PassiveSkills passiveSkill;
    
    private GameScene_UI gameSceneUI;

    public float Exp
    {
        get { return _exp; }
        set
        {
            float inputValue = value * (1 + passiveSkill.skillDict[Define.SkillType.ExpIncrease]);
            _exp = inputValue;

            //Debug.Log($"Exp:{_exp}");
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
        get 
        {
            float atkCoefficient = _atkCoefficient * (1 + passiveSkill.skillDict[Define.SkillType.ATKIncrease]);
            
            return atkCoefficient;
        } 
        set { _atkCoefficient = value; }
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public override float Hp
    {
        get => base.Hp;
        set
        {
            _hp = value;
            if(gameSceneUI)
                gameSceneUI.UpdateHpUI(_hp / _maxHp);
        }
    }
    private IEnumerator Start()
    {
        passiveSkill = new PassiveSkills();
        passiveSkill.Init(this);

        _level = 1;
        gameSceneUI = FindObjectOfType<GameScene_UI>();
        yield return new WaitUntil(() => gameSceneUI);
    }


    public void SetStat()
    {
        Dictionary<Define.CharacterType, CharacterStatData> dict = Managers.Data.characterStatDict;
        CharacterStatData stat = dict[(Define.CharacterType)System.Enum.Parse(typeof(Define.CharacterType), name)];

        Hp = stat._maxHp;
        _maxHp = stat._maxHp;
        _atkCoefficient = stat._atk;
        _moveSpeed = stat._moveSpeed;
        _totalExp = 110;
    }

    public override void OnAttacked(BaseController attacker, float damage)
    {
        base.OnAttacked(attacker, damage);
    }

    protected override void OnDead(BaseController attacker)
    {
        Debug.Log("Player Dead");
        Destroy(gameObject);
        Managers.Game.OnGameOver();
    }

    public void PassiveSkillLevelUp(Define.SkillType skill, Define.SkillGrade grade)
    {
        passiveSkill.LevelUp(skill, grade);
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
    PlayerStat _stat;

    public void Init(PlayerStat stat)
    {
        for (Define.SkillType i = Define.SkillType.ATKIncrease; i < Define.SkillType.PassiveMax; i++)
        {
            skillDict.Add(i, 0);
        }
        _stat = stat;
    }
    public void LevelUp(Define.SkillType skillType, Define.SkillGrade skillGrade)
    {
        if(skillDict.Count >= 3)
        {
            Debug.Log("Full");
            return;
        }
        switch (skillType)
        {
            case Define.SkillType.ATKIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].ATKIncrease;
                _stat.AtkCoefficient *= skillDict[skillType];
                break;
            case Define.SkillType.MaxHPIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].MaxHPIncrease;
                _stat.MaxHp *= skillDict[skillType];
                break;
            case Define.SkillType.DEFIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].DEFIncrease;
                // 방어력...?_stat. *= passiveSkills[skillType];
                break;
            case Define.SkillType.MoveSpeedIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].MoveSpeedIncrease;
                _stat.MoveSpeed *= skillDict[skillType];
                break;
            case Define.SkillType.ProjectileIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].ProjectileIncrease;
                //투사체 증가스탯_stat. *= passiveSkills[skillType];
                break;
            case Define.SkillType.ExpIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].ExpIncrease;
                //경험치 얻을 때 비율 스탯 _stat.MoveSpeed *= passiveSkills[skillType];
                break;
            case Define.SkillType.CoolTimeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].CoolTimeIncrease;
                //쿨타임 스탯                 _stat.MoveSpeed *= passiveSkills[skillType];
                break;
            case Define.SkillType.RootRangeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].RootRangeIncrease;
                //아이템 루팅 충돌체 크기 증가_stat.MoveSpeed *= passiveSkills[skillType];
                break;
            case Define.SkillType.HPRecoveryAssistIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].HPRecoveryAssistIncrease;
                //레벨업시, 회복 보조 스탯 _stat.MoveSpeed *= passiveSkills[skillType];
                break;
            case Define.SkillType.EnemyHpDownIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].EnemyHpDownIncrease;
                //감소치 스탯 만들어서 실시간 반영
                break;
            case Define.SkillType.HPRecoveryAmountIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].HPRecoveryAmountIncrease;
                //회복량 스탯;
                break;
            case Define.SkillType.DurationIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].DurationIncrease;
                //지속시간 스탯 _stat.MoveSpeed *= passiveSkills[skillType];
                break;
            case Define.SkillType.RangeIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].RangeIncrease;
                //범위증가 스탯 *= passiveSkills[skillType];
                break;
            case Define.SkillType.HPRecoveryPerSecIncrease:
                skillDict[skillType] += Managers.Data.passiveSkillCoefficients[skillGrade].HPRecoveryPerSecIncrease;
                _stat.MoveSpeed *= skillDict[skillType];
                //초당 회복 스탯 만들어서 1초마다 돌리기
                break;
            default:
                break;
        }
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