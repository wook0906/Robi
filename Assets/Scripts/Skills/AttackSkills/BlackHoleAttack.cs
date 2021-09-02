using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BlackHoleAttack : AttackSkillBase
{
    BlackHoleAttackSkillStat blackHoleStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.BlackHole;
        blackHoleStat = gameObject.AddComponent<BlackHoleAttackSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        blackHoleStat.InitSkillStat(_type);
        _prefab = Stat._bulletPrefab;
    }

    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");

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
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        blackHoleStat.LevelUp(grade);
    }

}
