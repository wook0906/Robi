using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : BaseController
{
    Transform owner;
    Vector3 offset;
    DroneSkillStat droneSKillStat;

    float attackDelayTimer = 0;
    GameObject _projectilePrefab;

    public void Init(Transform owner, Vector3 offset, DroneSkillStat stat)
    {
        this.owner = owner;
        this.offset = offset;
        this.droneSKillStat = stat;
        _projectilePrefab = stat._bulletPrefab;// Managers.Resource.Load<GameObject>("Prefabs/Projectiles/NormalAttackProjectile");
    }
    public void SetStat(DroneSkillStat stat)
    {
        this.droneSKillStat = stat;
    }
    private void LateUpdate()
    {
        transform.position = owner.transform.position + offset;
        GameObject target = SearchTarget();
        if (target != null)
        {
            this.transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized, Vector3.back);
        }
        if (Time.time - attackDelayTimer > droneSKillStat.CoolTime)
        {
            Attack(target);
        }
    }
    void Attack(GameObject target)
    {
        Debug.Log($"Drone Controller Fired. #Info : CoolTime : {droneSKillStat.CoolTime}, Damage : {droneSKillStat.Damage}, AttackRange : {droneSKillStat.AttackRange}, NumOfProjectilePerBurst {droneSKillStat.NumOfProjectilePerBurst}, Speed : {droneSKillStat.Speed}, IsExplode : {droneSKillStat.IsExplode}, ExplosionRange : {droneSKillStat.ExplosionRange}, ExplosionDamage : {droneSKillStat.ExplosionDamage}, isPernerate : {droneSKillStat.IsPenetrate}, Duration : {droneSKillStat.Duration}, DelayPerAttack : {droneSKillStat.DelayPerAttack}");

        if (target == null) return;

        attackDelayTimer = Time.time;
        GameObject projectileGo = Instantiate(_projectilePrefab);
        projectileGo.transform.position = transform.position;

        Projectile projectile = projectileGo.GetComponent<Projectile>();
        projectile.Init(owner.GetComponent<BaseController>(), target.GetComponent<BaseController>().CenterPosition, droneSKillStat.Damage, droneSKillStat.AttackRange, droneSKillStat.Speed*2f, droneSKillStat.IsExplode, droneSKillStat.ExplosionRange, droneSKillStat.ExplosionDamage, droneSKillStat.IsPenetrate,droneSKillStat.Duration, LayerMask.NameToLayer("Enemy"));
        

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;
        projectile.OnArrive -= OnArrive;
        projectile.OnArrive += OnArrive;
    }

    protected GameObject SearchTarget()
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        GameObject closetTarget = null;
        float closetDist = float.MaxValue;

        foreach (MonsterController target in monsters)
        {
            float dist = (target.transform.position - transform.position).sqrMagnitude;
            if (dist < closetDist)
            {
                closetTarget = target.gameObject;
                closetDist = dist;
            }
        }
        return closetTarget;
    }

    public void OnHit(GameObject target, Projectile projectile)
    {
        Debug.Log("드론 총알 적중!");
        //Debug.Log($"Hit target:{target.name}", this);
        //TODO: 나중에 타켓 레이어를 미리 Init 함수에서 셋팅할 수 있게해서 targetLayer의
        // 물체들만 걸리게하자!


        //ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        //Vector3 pos = projectile.transform.position;
        //effect.transform.position = pos;
        //effect.Play();
    }
    public void OnArrive(Vector3 targetPos, Projectile projectile)
    {
        projectile.StartCoroutine(projectile.GraduallyDisappear());
    }
}
