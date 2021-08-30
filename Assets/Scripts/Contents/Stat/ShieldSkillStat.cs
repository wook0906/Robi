using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkillStat : SkillStat
{
    BaseController owner;
    float _commonCoolTimeCoefficients;
    float _rareCoolTimeCoefficients;
    float _uniqueCooltimeCoefficients;

    float _uniqueHpRecoveryCoefficients;

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        CoolTime -= ((NumOfCommon) * _commonCoolTimeCoefficients) +
            (NumOfRare * _rareCoolTimeCoefficients) +
            (NumOfUnique * _uniqueCooltimeCoefficients);

        //나노회복 패시브모듈로 인한 회복효과 추가해야함


    }
    public override void InitSkillStat(Define.AttackSkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonCoolTimeCoefficients = ExplosionDamage * 0.02f;
        _rareCoolTimeCoefficients = ExplosionDamage * 0.03f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;

        owner = transform.root.GetComponent<BaseController>();
    }
}
