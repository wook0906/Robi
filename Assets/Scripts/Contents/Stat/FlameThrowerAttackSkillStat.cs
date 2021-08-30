using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerAttackSkillStat : SkillStat
{
    public float targetAngle = 30f;

    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;

    float _uniqueCooltimeCoefficients;

    float _uniqueAngleCoefficients;

    float _rareDurationCoefficients;
    float _uniqueDurationCoefficients;


    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        Damage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);

        Duration += (NumOfRare * _rareDurationCoefficients) + (NumOfUnique * _uniqueDurationCoefficients);

        targetAngle += (NumOfUnique * _uniqueAngleCoefficients);
    }
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.1f;
        _rareDamageCoefficients = Damage * 0.15f;
        _uniqueDamageCoefficients = Damage * 0.2f;
        _uniqueCooltimeCoefficients = CoolTime * 0.02f;
        _uniqueAngleCoefficients = AttackRange * 0.05f;
        _rareDurationCoefficients = Duration * 0.02f;
        _uniqueDurationCoefficients = Duration * 0.05f;
    }
}
