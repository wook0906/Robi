using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseWaveAttackSkillStat : SkillStat
{
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;

    float _uniqueCooltimeCoefficients;

  

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        Damage += ((NumOfCommon) * _commonDamageCoefficients) +
            (NumOfRare * _rareDamageCoefficients) +
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);
    
        if(Level % 2 == 1)
            NumOfProjectilePerBurst++;
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.05f;
        _rareDamageCoefficients = Damage * 0.1f;
        _uniqueDamageCoefficients = Damage * 0.15f;
        _uniqueCooltimeCoefficients = CoolTime * 0.03f;
    }
}
