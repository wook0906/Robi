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
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonCooltimeCoefficients = CoolTime * 0.02f;
        _rareCooltimeCoefficients = CoolTime * 0.03f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;

        _rareDurationCoefficients = Duration * 0.02f;
        _uniqueDurationCoefficients = Duration * 0.04f;

        _uniqueRangeCoefficients = MaxAttackRange * 0.03f;
    }
}
