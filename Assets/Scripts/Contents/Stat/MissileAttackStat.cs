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
