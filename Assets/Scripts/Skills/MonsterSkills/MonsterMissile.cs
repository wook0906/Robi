﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterMissile : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.MonsterMissile;
        gameObject.AddComponent<SkillStat>().SetStat(_type);
        base.Init(owner, muzzleTransform, parent);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MissileAttackProjectile");
    }

    public override bool UseSkill()
    {
        GameObject target = MonsterController.target;

        if (target == null)
            return false;

        GameObject missileGO = Instantiate(_prefab, _parent);
        missileGO.transform.rotation = Quaternion.identity;
        if (_muzzleTransform == null)
            missileGO.transform.position = _owner.CenterPosition;
        else
            missileGO.transform.position = _muzzleTransform.position;

        Projectile projectile = missileGO.GetComponent<Projectile>();
        projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, Stat.Damage, Stat.AttackRange,
            Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
            Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Player"));

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;
        projectile.OnKill -= OnKill;
        projectile.OnKill += OnKill;

        OnFire();
        return true;
    }

    public override void OnFire()
    {
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        Debug.Log("폭발!");
        ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        Vector3 pos = projectile.transform.position;
        effect.transform.position = pos;
        effect.Play();

        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange, 1 << LayerMask.NameToLayer("Player"));
        if (colliders.Length == 0)
            return;

        foreach (var collider in colliders)
        {
            CreatureStat stat = collider.GetComponent<CreatureStat>();
            if (stat == null)
                continue;
            stat.OnAttacked(_owner, projectile.ExplosionDamage);
        }
    }
}
