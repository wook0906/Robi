using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackStat : SkillStat
{
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;
    float _uniqueCooltimeCoefficients;
    float _rareCooltimeCoefficients;

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
            int numOfProjectilePerBurst = _numOfProjectilePerBurst + Mathf.RoundToInt(_numOfProjectilePerBurst * player.passiveSkill.skillDict[Define.SkillType.ProjectileIncrease]);

            //Debug.Log($"원래는 {_numOfProjectilePerBurst}발 쏘는데, {(float)(_numOfProjectilePerBurst * player.passiveSkill.skillDict[Define.SkillType.ProjectileIncrease])}의 반올림인 {Mathf.RoundToInt(_numOfProjectilePerBurst * player.passiveSkill.skillDict[Define.SkillType.ProjectileIncrease])}을 더해서 {numOfProjectilePerBurst}발 발사함");
            
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
        Damage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfRare * _rareCooltimeCoefficients) + (NumOfUnique * _uniqueCooltimeCoefficients);
        if (Level % 3 == 0)
            NumOfProjectilePerBurst++;
      
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.1f;
        _rareDamageCoefficients = Damage * 0.15f;
        _uniqueDamageCoefficients = Damage * 0.2f;
        _rareCooltimeCoefficients = CoolTime * 0.02f;
        _uniqueCooltimeCoefficients = CoolTime * 0.04f;
    }
}
