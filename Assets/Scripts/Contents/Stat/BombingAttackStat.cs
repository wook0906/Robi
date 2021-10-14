using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingAttackStat : SkillStat
{
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;
    float _uniqueCooltimeCoefficients;
    float _rareCooltimeCoefficients;
    float _rareRangeCoefficients;
    float _uniqueRangeCoefficients;
    float _uniqueAdditionalPerBurst;

    public override float CoolTime
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float coolTime = _coolTime - (_coolTime * player.passiveSkill.skillValueDict[Define.SkillType.CoolTimeIncrease]);
            return coolTime;
        }
        set { _coolTime = value; }
    }
    public override float Damage
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float damage = (_damage * (1f + player.passiveSkill.skillValueDict[Define.SkillType.ATKIncrease])) * player.PlayerStat.AtkCoefficient;
            return damage;
        }
        set { _damage = value; }
    }
    public override float ExplosionDamage
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float explosionDamage = (_explosionDamage * (1f + player.passiveSkill.skillValueDict[Define.SkillType.ATKIncrease])) * player.PlayerStat.AtkCoefficient;
            return explosionDamage;
        }
        set { _explosionDamage = value; }
    }
    public override float AttackRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float attackRange = _attackRange * (1f + player.passiveSkill.skillValueDict[Define.SkillType.RangeIncrease]);
            return attackRange;
        }
        set { _attackRange = value; }
    }
    public override int NumOfProjectilePerBurst
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            int numOfProjectilePerBurst = _numOfProjectilePerBurst + Mathf.RoundToInt(_numOfProjectilePerBurst * player.passiveSkill.skillValueDict[Define.SkillType.ProjectileIncrease]);

            return numOfProjectilePerBurst;
        }
        set { _numOfProjectilePerBurst = value; }
    }
    public override float Duration
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float duration = _duration * (1f + player.passiveSkill.skillValueDict[Define.SkillType.DurationIncrease]);
            return duration;
        }
        set { _duration = value; }
    }
    public override float ExplosionRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float explosionRange = _explosionRange * (1f + player.passiveSkill.skillValueDict[Define.SkillType.RangeIncrease]);
            return explosionRange;
        }
        set { _explosionRange = value; }
    }

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        ExplosionDamage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfRare * _rareCooltimeCoefficients) + (NumOfUnique * _uniqueCooltimeCoefficients);

        AttackRange += (NumOfRare * _rareRangeCoefficients) + (NumOfUnique * _uniqueRangeCoefficients);

        NumOfProjectilePerBurst += (int)(NumOfUnique * _uniqueAdditionalPerBurst);
      
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        ActiveSkillCoefficientsData data = Managers.Data.activeSkillCoefficientDict[skillType];
        _commonDamageCoefficients = ExplosionDamage * data.common.explostionDamage;
        _rareDamageCoefficients = ExplosionDamage * data.rare.explostionDamage;
        _uniqueDamageCoefficients = ExplosionDamage * data.unique.explostionDamage;
        _rareCooltimeCoefficients = CoolTime * data.rare.coolTime;
        _uniqueCooltimeCoefficients = CoolTime * data.unique.coolTime;
        _rareRangeCoefficients = AttackRange * data.rare.explosionRange;
        _uniqueRangeCoefficients = AttackRange * data.unique.explosionRange;
        _uniqueAdditionalPerBurst = NumOfProjectilePerBurst * data.unique.AdditionalProjectilePerBurst;
        
    }
}
