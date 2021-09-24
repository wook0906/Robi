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
        _prefab = blackHoleStat._bulletPrefab;
    }

    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {blackHoleStat.CoolTime}, Damage : {blackHoleStat.Damage}, AttackRange : {blackHoleStat.AttackRange}, NumOfProjectilePerBurst {blackHoleStat.NumOfProjectilePerBurst}, Speed : {blackHoleStat.Speed}, IsExplode : {blackHoleStat.IsExplode}, ExplosionRange : {blackHoleStat.ExplosionRange}, ExplosionDamage : {blackHoleStat.ExplosionDamage}, isPernerate : {blackHoleStat.IsPenetrate}, Duration : {blackHoleStat.Duration}, DelayPerAttack : {blackHoleStat.DelayPerAttack}");

        GameObject target = SearchNoramalStateTarget();

        if (target == null)
            return false;

        _owner.State = Define.CreatureState.Attack;

        GameObject blackHoleGO = Instantiate(_prefab, _parent);
        blackHoleGO.transform.rotation = Quaternion.identity;
        blackHoleGO.transform.position = target.GetComponent<BaseController>().CenterPosition;


        BlackHoleProjectile projectile = blackHoleGO.GetComponent<BlackHoleProjectile>();
        projectile.transform.localScale = new Vector3(blackHoleStat.AttackRange, blackHoleStat.AttackRange, blackHoleStat.AttackRange);
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
