using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class NapalmAttack : AttackSkillBase
{
    private GameObject _wideAreaAttackPrefab;
    NapalmAttackSkillStat napalmStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.Napalm;
        napalmStat = gameObject.AddComponent<NapalmAttackSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        napalmStat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/NapalmAttackProjectile");
        _wideAreaAttackPrefab = Resources.Load<GameObject>("Prefabs/WideAreaAttack");
    }

    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");
        GameObject target = SearchTarget();

        if (target == null)
            return false;

        _owner.State = Define.CreatureState.Attack;
        //Debug.Log($"Target:{target.name}");
        GameObject napalmGO = Instantiate(_prefab, _parent);
        napalmGO.transform.rotation = Quaternion.identity;
        if (_muzzleTransform == null)
            napalmGO.transform.position = _owner.transform.position;
        else
            napalmGO.transform.position = _muzzleTransform.position;

        PlayerController owner = _owner as PlayerController;
        Projectile projectile = napalmGO.GetComponent<Projectile>();
        projectile.Init(_owner, target.GetComponent<BaseController>().CenterPosition, napalmStat.Damage * (1f + owner.passiveSkill.skillDict[SkillType.ATKIncrease]), napalmStat.AttackRange,
            napalmStat.Speed, napalmStat.IsExplode, napalmStat.ExplosionRange, napalmStat.ExplosionDamage,
            napalmStat.IsPenetrate, napalmStat.Duration, LayerMask.NameToLayer("Enemy"));

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
        _owner.AttackSkillDispatcher.Add(napalmStat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        //Debug.Log($"Hit target:{target.name}", this);
        //TODO: 나중에 타켓 레이어를 미리 Init 함수에서 셋팅할 수 있게해서 targetLayer의
        // 물체들만 걸리게하자!
        if (!target) return;

        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange, 1<<8);
        if (colliders.Length == 0)
            return;

        Vector3 wideAreaAttackPos = target.transform.position;
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

        GameObject wideAreaAttackGO = Instantiate(_wideAreaAttackPrefab);
        wideAreaAttackPos.z -= 0.2f;
        wideAreaAttackGO.transform.position = wideAreaAttackPos;
        wideAreaAttackGO.GetComponent<WideAreaAttack>().Init(_owner, Mathf.RoundToInt(napalmStat.ExplosionDamage * 0.3f),
            napalmStat.ExplosionRange, napalmStat.Duration, napalmStat.DelayPerAttack);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        napalmStat.LevelUp(grade);
    }
}
