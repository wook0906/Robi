using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearSkillStat : SkillStat
{
    float _commonCooltimeCoefficients;
    float _rareCooltimeCoefficients;
    float _uniqueCooltimeCoefficients;

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients) +
            (NumOfRare * _rareCooltimeCoefficients) +
            (NumOfCommon * _commonCooltimeCoefficients);
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonCooltimeCoefficients = CoolTime * 0.02f;
        _rareCooltimeCoefficients = CoolTime * 0.03f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;

    }
}
