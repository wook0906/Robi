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
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = ExplosionDamage * 0.1f;
        _rareDamageCoefficients = ExplosionDamage * 0.15f;
        _uniqueDamageCoefficients = ExplosionDamage * 0.2f;
        _rareCooltimeCoefficients = CoolTime * 0.03f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;
        _rareRangeCoefficients = AttackRange * 0.02f;
        _uniqueRangeCoefficients = AttackRange * 0.05f;
        _uniqueAdditionalPerBurst = NumOfProjectilePerBurst * 0.03f;
        
    }
}
