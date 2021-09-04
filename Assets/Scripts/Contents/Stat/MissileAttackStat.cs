using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttackStat : SkillStat
{
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;

    float _uniqueCooltimeCoefficients;
   
    float _rareRangeCoefficients;
    float _uniqueRangeCoefficients;


    public override float CoolTime
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float coolTime = _coolTime - (_coolTime * player.passiveSkill.skillDict[Define.SkillType.CoolTimeIncrease]);
            return coolTime;
        }
        set { _coolTime = value; }
    }
    public override float Damage
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float damage = (_damage * (1f + player.passiveSkill.skillDict[Define.SkillType.ATKIncrease])) * player.PlayerStat.AtkCoefficient;
            return damage;
        }
        set { _damage = value; }
    }
    public override float ExplosionDamage
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float explosionDamage = (_explosionDamage * (1f + player.passiveSkill.skillDict[Define.SkillType.ATKIncrease])) * player.PlayerStat.AtkCoefficient;
            return explosionDamage;
        }
        set { _explosionDamage = value; }
    }
    public override float AttackRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float attackRange = _attackRange * (1f + player.passiveSkill.skillDict[Define.SkillType.RangeIncrease]);
            return attackRange;
        }
        set { _attackRange = value; }
    }
    public override int NumOfProjectilePerBurst
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            int numOfProjectilePerBurst = _numOfProjectilePerBurst + Mathf.RoundToInt(_numOfProjectilePerBurst *  player.passiveSkill.skillDict[Define.SkillType.ProjectileIncrease]);

            return numOfProjectilePerBurst;
        }
        set { _numOfProjectilePerBurst = value; }
    }
    public override float Duration
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float duration = _duration * (1f + player.passiveSkill.skillDict[Define.SkillType.DurationIncrease]);
            return duration;
        }
        set { _duration = value; }
    }
    public override float ExplosionRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float explosionRange = _explosionRange * (1f + player.passiveSkill.skillDict[Define.SkillType.RangeIncrease]);
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

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);

        ExplosionRange += (NumOfRare * _rareRangeCoefficients) + (NumOfUnique * _uniqueRangeCoefficients);

        if (Level == 4 || Level == 7)
            NumOfProjectilePerBurst++;
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = ExplosionDamage * 0.1f;
        _rareDamageCoefficients = ExplosionDamage * 0.15f;
        _uniqueDamageCoefficients = ExplosionDamage * 0.2f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;
        _rareRangeCoefficients = AttackRange * 0.02f;
        _uniqueRangeCoefficients = AttackRange * 0.05f;
        
    }

}
