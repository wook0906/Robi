using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class LaserAttack : AttackSkillBase
{
    PlayerController ownerPlayer;
    LaserAttackSkillStat laserStat;

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        ownerPlayer = owner as PlayerController;
        _type = AttackSkillType.PlayerLaser;
        laserStat = gameObject.AddComponent<LaserAttackSkillStat>();
        base.Init(ownerPlayer, ownerPlayer.launchPoints[(int)LaunchPointType.RS], parent);
        laserStat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/PlayerLaser");
        
    }

    public override bool UseSkill()
    {
        GameObject target = SearchTarget();
        if (target == null)
            return false;
        _owner.State = Define.CreatureState.Attack;

        Vector3 targetPos = target.GetComponent<BaseController>().CenterPosition;

        //Debug.Log($"Target:{target.name}");
        Debug.DrawRay(transform.position, (targetPos - _owner.CenterPosition).normalized * Stat.AttackRange, Color.red, 1f);
        GameObject projectileGO = GameObject.Instantiate(_prefab, ownerPlayer.launchPoints[(int)LaunchPointType.RS]);
        if (_muzzleTransform == null)
            projectileGO.transform.position = _owner.CenterPosition;
        else
            projectileGO.transform.position = _muzzleTransform.position;

        LaserProjectile projectile = projectileGO.GetComponent<LaserProjectile>();
        projectile.Init(_owner, Stat.Damage, Stat.Duration, LayerMask.NameToLayer("Enemy"), laserStat.laserScale);

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;

        OnFire();
        return true;
    }


    public override void OnFire()
    {
        Debug.Log("Fire");
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public void OnHit(GameObject target, LaserProjectile projectile)
    {
        Debug.Log($"Hit target:{target.name}");
    }
    
    public override void LevelUp(Define.SkillGrade grade)
    {
        laserStat.LevelUp(grade);
    }
}
