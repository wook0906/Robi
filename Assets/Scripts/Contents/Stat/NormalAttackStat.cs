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
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.1f;
        _rareDamageCoefficients = Damage * 0.15f;
        _uniqueDamageCoefficients = Damage * 0.2f;
        _rareCooltimeCoefficients = CoolTime * 0.02f;
        _uniqueCooltimeCoefficients = CoolTime * 0.04f;
    }
}
