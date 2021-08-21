using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterNormalAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.MonsterNormal;
        gameObject.AddComponent<SkillStat>().SetStat(_type);
        base.Init(owner, muzzleTransform, parent);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/NormalAttackProjectile");
        
        //TODO: 각 스킬의 특성에 따른 추가 로직을 넣어줘야할 듯함...
    }

    public override bool UseSkill()
    {

        GameObject target = MonsterController.target;

        if (target == null)
            return false;

        GameObject projectileGO = GameObject.Instantiate(_prefab, _parent);
        projectileGO.transform.rotation = Quaternion.identity;
        if (_muzzleTransform == null)
            projectileGO.transform.position = _owner.CenterPosition;
        else
            projectileGO.transform.position = _muzzleTransform.position;

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, Stat.Damage, Stat.AttackRange,
            Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
            Stat.IsPenetrate, LayerMask.NameToLayer("Player"));

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;


        OnFire();
        return true;
    }

    public override void OnFire()
    {
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        if (target == null)
            return;
    }

    public override void OnKill(GameObject target)
    {
        if (target == null)
            return;
    }
}