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
        ActiveSkillCoefficientsData data = Managers.Data.activeSkillCoefficientDict[skillType];

        _commonCooltimeCoefficients = CoolTime * data.common.coolTime;
        _rareCooltimeCoefficients = CoolTime * data.rare.coolTime;
        _uniqueCooltimeCoefficients = CoolTime * data.unique.coolTime;

    }
}
