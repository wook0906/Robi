using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BlackHoleAttack : AttackSkillBase
{
    BlackHoleAttackSkillStat blackHoleStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.BlackHole;
        blackHoleStat = gameObject.AddComponent<BlackHoleAttackSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        blackHoleStat.InitSkillStat(_type);
        _prefab = Stat._bulletPrefab;
    }

    public override bool UseSkill()
    {
        GameObject target = SearchNoramalStateTarget();

        if (target == null)
            return false;

        _owner.State = Define.CreatureState.Attack;

        GameObject blackHoleGO = Instantiate(_prefab, _parent);
        blackHoleGO.transform.rotation = Quaternion.identity;
        blackHoleGO.transform.position = target.GetComponent<BaseController>().CenterPosition;


        BlackHoleProjectile projectile = blackHoleGO.GetComponent<BlackHoleProjectile>();
        projectile.transform.localScale = new Vector3(Stat.AttackRange,Stat.AttackRange,Stat.AttackRange);
        projectile.Init(_owner, target, blackHoleStat);
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
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        blackHoleStat.LevelUp(grade);
    }

}
