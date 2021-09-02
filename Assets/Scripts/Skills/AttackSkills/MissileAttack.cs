using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MissileAttack : AttackSkillBase
{
    MissileAttackStat missileStat;

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.PlayerMissile;
        missileStat = gameObject.AddComponent<MissileAttackStat>();
        base.Init(owner, muzzleTransform, parent);
        missileStat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MissileAttackProjectile");
    }

    public override bool UseSkill()
    {
        GameObject target = SearchTarget();

        if (target == null)
            return false;

        _owner.State = Define.CreatureState.Attack;

        GameObject missileGO = Instantiate(_prefab, _parent);
        missileGO.transform.rotation = Quaternion.identity;
        if (_muzzleTransform == null)
            missileGO.transform.position = _owner.GetComponent<PlayerController>().launchPoints[(int)Define.LaunchPointType.RS].transform.position;
        else
            missileGO.transform.position = _muzzleTransform.position;

        Projectile projectile = missileGO.GetComponent<Projectile>();
        projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, Stat.Damage, Stat.AttackRange,
            Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
            Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Enemy"));

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;
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

    public override void OnHit(GameObject target, Projectile projectile)
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");

        //Debug.Log($"Hit target:{target.name}", this);
        //TODO: 나중에 타켓 레이어를 미리 Init 함수에서 셋팅할 수 있게해서 targetLayer의
        // 물체들만 걸리게하자!
        ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        Vector3 pos = projectile.transform.position;
        effect.transform.position = pos;
        effect.Play();

        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange, 1 << 8);
        if (colliders.Length == 0)
            return;

        //Debug count
        int count = 0;

        foreach (var collider in colliders)
        {
            if (collider.gameObject == projectile.gameObject || collider.gameObject == _owner.gameObject)
                continue;
            CreatureStat stat = collider.GetComponent<CreatureStat>();
            if (stat == null)
                continue;
            stat.OnAttacked(_owner, projectile.ExplosionDamage);
            count++;
        }
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        missileStat.LevelUp(grade);
    }
}
