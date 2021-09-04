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

    [SerializeField]
    float hpRecoveryPerSec = 0f;
    float hpRecoveryTimeStamp = 0f;

    public override float CoolTime
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float coolTime = _coolTime - (_coolTime * player.passiveSkill.skillDict[Define.SkillType.CoolTimeIncrease]);
            return coolTime;
        }
        set { _coolTime = value; }
    }
    public override float Damage
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float damage = (_damage * (1f + player.passiveSkill.skillDict[Define.SkillType.ATKIncrease])) * player.PlayerStat.AtkCoefficient;
            return damage;
        }
        set { _damage = value; }
    }
    public override float AttackRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float attackRange = _attackRange * (1f + player.passiveSkill.skillDict[Define.SkillType.RangeIncrease]);
            return attackRange;
        }
        set { _attackRange = value; }
    }
    public override int NumOfProjectilePerBurst
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            int numOfProjectilePerBurst = _numOfProjectilePerBurst + Mathf.RoundToInt(_numOfProjectilePerBurst * player.passiveSkill.skillDict[Define.SkillType.ProjectileIncrease]);

            return numOfProjectilePerBurst;
        }
        set { _numOfProjectilePerBurst = value; }
    }
    public override float Duration
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float duration = _duration * (1f + player.passiveSkill.skillDict[Define.SkillType.DurationIncrease]);
            return duration;
        }
        set { _duration = value; }
    }
    public override float ExplosionRange
    {
        get
        {
            PlayerController player = _owner as PlayerController;
            float explosionRange = _explosionRange * (1f + player.passiveSkill.skillDict[Define.SkillType.RangeIncrease]);
            return explosionRange;
        }
        set { _explosionRange = value; }
    }

    public override void LevelUp(Define.SkillGrade grade)
    {
        base.LevelUp(grade);
        CoolTime -= ((NumOfCommon) * _commonCoolTimeCoefficients) +
            (NumOfRare * _rareCoolTimeCoefficients) +
            (NumOfUnique * _uniqueCooltimeCoefficients);

        //나노회복 패시브모듈로 인한 회복효과 추가해야함
    }

    private void Update()
    {
        
        if (Time.time - hpRecoveryTimeStamp > 1f)
        {
            PlayerStat playerStat = _owner.Stat as PlayerStat;
            hpRecoveryTimeStamp = Time.time;
            Debug.Log($"Shield Hp Recover {hpRecoveryPerSec}");
            playerStat.HpRecovery(hpRecoveryPerSec);
        } 
    }
    
    public override void InitSkillStat(Define.SkillType skillType)
    {
        base.InitSkillStat(skillType);
        _commonCoolTimeCoefficients = ExplosionDamage * 0.02f;
        _rareCoolTimeCoefficients = ExplosionDamage * 0.03f;
        _uniqueCooltimeCoefficients = CoolTime * 0.05f;

        owner = transform.root.GetComponent<BaseController>();
    }
}
