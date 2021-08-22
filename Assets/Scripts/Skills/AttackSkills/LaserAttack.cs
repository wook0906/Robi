using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class LaserAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.PlayerLaser;
        base.Init(owner, muzzleTransform, parent);
        _prefab = Resources.Load<GameObject>("Prefabs/Laser");
    }

    public override bool UseSkill()
    {
        GameObject[] targets = SearchTargets(Stat.NumOfProjectilePerBurst);
        if (targets == null)
            return false;

        _owner.State = Define.CreatureState.Attack;
        foreach (var target in targets)
        {
            if (target == null)
                continue;

            //Debug.Log($"Target:{target.name}");
            Debug.DrawRay(transform.position, (target.transform.position - transform.position).normalized * 5f, Color.red, 1f);
            GameObject projectileGO = GameObject.Instantiate(_prefab, _parent);
            projectileGO.transform.rotation = Quaternion.identity;
            if (_muzzleTransform == null)
                projectileGO.transform.position = _owner.transform.position;
            else
                projectileGO.transform.position = _muzzleTransform.position;

            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, Stat.Damage, Stat.AttackRange,
                Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
                Stat.IsPenetrate,Stat.Duration, LayerMask.NameToLayer("Enemy"));

            projectile.OnHit -= OnHit;
            projectile.OnHit += OnHit;
            projectile.OnKill -= OnKill;
            projectile.OnKill += OnKill;
        }
        
        OnFire();
        return true;
    }

    public override void OnFire()
    {
        Debug.Log("Fire");
        _owner.State = CreatureState.Idle;
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        Debug.Log($"Hit target:{target.name}");
    }

    public override void OnKill(GameObject target)
    {
        Debug.Log($"Kill target:{target.name}");
    }
}
