using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAttackSkillStat : SkillStat
{
    private float _maxAttackRange;
    public float MaxAttackRange { get { return _maxAttackRange; } set { _maxAttackRange = value; } }

    float _commonCooltimeCoefficients;
    float _rareCooltimeCoefficients;
    float _uniqueCooltimeCoefficients;

    float _rareDurationCoefficients;
    float _uniqueDurationCoefficients;

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

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients) +
            (NumOfRare * _rareCooltimeCoefficients) +
            (NumOfCommon * _commonCooltimeCoefficients);

        MaxAttackRange += (NumOfUnique * _uniqueRangeCoefficients);
        //AttackRange += (numOfUnique * _uniqueRangeCoefficients);

        Duration += (NumOfRare * _rareDurationCoefficients) + (NumOfUnique * _uniqueDurationCoefficients);
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        ActiveSkillCoefficientsData data = Managers.Data.activeSkillCoefficientDict[skillType];
        _commonCooltimeCoefficients = CoolTime * data.common.coolTime;
        _rareCooltimeCoefficients = CoolTime * data.rare.coolTime;
        _uniqueCooltimeCoefficients = CoolTime * data.unique.coolTime;

        _rareDurationCoefficients = Duration * data.rare.duration;
        _uniqueDurationCoefficients = Duration * data.unique.duration;

        _uniqueRangeCoefficients = MaxAttackRange * data.unique.attackRange;
    }
}
