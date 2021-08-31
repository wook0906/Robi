using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackSkillStat : SkillStat
{
    public float laserScale = 1f;
    float _commonDamageCoefficients;
    float _rareDamageCoefficients;
    float _uniqueDamageCoefficients;
    float _uniqueCooltimeCoefficients;
    float _rareScaleCoefficients;
    float _uniqueScaleCoefficients;
    
    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        Damage += ((NumOfCommon) * _commonDamageCoefficients) + 
            (NumOfRare * _rareDamageCoefficients) + 
            (NumOfUnique * _uniqueDamageCoefficients);

        CoolTime -= (NumOfUnique * _uniqueCooltimeCoefficients);
        
        laserScale += (NumOfRare * _rareScaleCoefficients) +
            (NumOfUnique * _uniqueScaleCoefficients);
    }
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonDamageCoefficients = Damage * 0.1f;
        _rareDamageCoefficients = Damage * 0.15f;
        _uniqueDamageCoefficients = Damage * 0.2f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;
        _rareScaleCoefficients = laserScale * 0.03f;
        _uniqueScaleCoefficients = laserScale * 0.05f;
    }
}
