using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BlackHoleAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.BlackHole;
        base.Init(owner, muzzleTransform, parent);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/BlackHoleProjectile");
    }

    public override bool UseSkill()
    {
        GameObject target = SearchNoramalStateTarget();

        if (target == null)
            return false;

        _owner.State = Define.CreatureState.Attack;

        GameObject blackHoleGO = Instantiate(_prefab, _parent);
        blackHoleGO.transform.rotation = Quaternion.identity;
        blackHoleGO.transform.position = target.transform.position + (Vector3.up/2f);


        BlackHoleProjectile projectile = blackHoleGO.GetComponent<BlackHoleProjectile>();
        projectile.Init(_owner, target, Stat as BlackHoleAttackSkillStat);
        projectile.DragToCenter();

        projectile.OnKill -= OnKill;
        projectile.OnKill += OnKill;

        OnFire();
        return true;
    }

    public override void OnFire()
    {
        Debug.Log("Fire");
        _owner.State = CreatureState.Idle;
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }

}
