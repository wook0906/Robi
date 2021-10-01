﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerAttack : AttackSkillBase
{
    PlayerController player;
    FlameThrowerAttackSkillStat flameThrowerStat;

    ParticleSystem effect;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        player = owner as PlayerController;
        _type = Define.SkillType.FlameThrower;
        flameThrowerStat = gameObject.AddComponent<FlameThrowerAttackSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        flameThrowerStat.InitSkillStat(_type);
        effect = Managers.Resource.Instantiate("Effects/Flamethrower").GetComponent<ParticleSystem>();
        effect.transform.position = player.launchPoints[(int)Define.LaunchPointType.Waist].position;
        effect.transform.SetParent(player.transform);
        effect.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public override bool UseSkill()
    {
        _owner.State = Define.CreatureState.Attack;
        StartCoroutine(Fire());
        return true;
    }

    private IEnumerator Fire()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");


        effect.GetComponent<ParticleSystem>().Play();

        int attackCount = (int)(Stat.Duration / Stat.DelayPerAttack);

        while (attackCount > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Stat.AttackRange * 2f, 1 << 8);

            foreach (Collider collider in colliders)
            {
                if (!collider.transform) continue;

                Vector3 targetDir = (collider.transform.position - _owner.transform.position).normalized;
                float angle = Vector3.Angle(targetDir, transform.up);

                if (angle <= flameThrowerStat.targetAngle)
                {
                    Debug.Log($"{angle} , {flameThrowerStat.targetAngle}");
                    collider.GetComponent<CreatureStat>().OnAttacked(_owner, Stat.Damage);
                    OnHit(collider.gameObject, null);
                }
            }
            OnFire();
            attackCount--;
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        effect.Stop();
        _owner.State = Define.CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }


    private void OnDrawGizmos()
    {
        if (Stat == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, Stat.AttackRange * 2f);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        flameThrowerStat.LevelUp(grade);
    }
}
