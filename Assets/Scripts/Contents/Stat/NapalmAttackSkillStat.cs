using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmAttackSkillStat : SkillStat
{
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;

    float _uniqueCooltimeCoefficients;

    float _rareRangeCoefficients;
    float _uniqueRangeCoefficients;

    float _commonContinuousDamage;
    float _rareContinuousDamage;
    float _uniqueContinuousDamage;

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        ExplosionDamage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);

        ExplosionRange += (NumOfRare * _rareRangeCoefficients) + (NumOfUnique * _uniqueRangeCoefficients);
        
        //지속데미지
        Damage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareContinuousDamage) +
            (NumOfUnique * _uniqueContinuousDamage);
    }
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = ExplosionDamage * 0.05f;
        _rareDamageCoefficients = ExplosionDamage * 0.1f;
        _uniqueDamageCoefficients = ExplosionDamage * 0.15f;
        _uniqueCooltimeCoefficients = CoolTime * 0.02f;
        _rareRangeCoefficients = ExplosionRange * 0.03f;
        _uniqueRangeCoefficients = ExplosionRange * 0.05f;
        _commonContinuousDamage = Damage * 0.03f;
        _rareContinuousDamage = Damage * 0.05f;
        _uniqueContinuousDamage = Damage * 0.08f;
    }
}
