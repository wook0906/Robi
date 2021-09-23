using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterNormalAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.MonsterNormal1;
        Stat = gameObject.AddComponent<SkillStat>();
        base.Init(owner, muzzleTransform, parent);
        Stat.InitSkillStat(_type);
        _prefab = Stat._bulletPrefab;
        //TODO: 각 스킬의 특성에 따른 추가 로직을 넣어줘야할 듯함...
        
    }

    public override bool UseSkill()
    {

        GameObject target = MonsterController.target;

        if (target == null)
            return false;
        GameObject projectileGO = Instantiate(_prefab, _parent);
        projectileGO.transform.rotation = Quaternion.identity;
        if (_muzzleTransform == null)
            projectileGO.transform.position = _owner.CenterPosition;
        else
            projectileGO.transform.position = _muzzleTransform.position;

        MonsterStat ownerStat = _owner.GetComponent<MonsterStat>();
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, ownerStat.Damage, ownerStat.AttackRange,
            Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
            Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Player"));

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;


        OnFire();
        return true;
    }

    public override void OnFire()
    {
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
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