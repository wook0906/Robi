using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWaveAttackStat : SkillStat
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
        Damage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);

        AttackRange += (NumOfRare * _rareRangeCoefficients) + (NumOfUnique * _uniqueRangeCoefficients);
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.1f;
        _rareDamageCoefficients = Damage * 0.15f;
        _uniqueDamageCoefficients = Damage * 0.2f;
        _uniqueCooltimeCoefficients = CoolTime * 0.03f;
        _rareRangeCoefficients = AttackRange * 0.03f;
        _uniqueRangeCoefficients = AttackRange * 0.05f;
    }
}
