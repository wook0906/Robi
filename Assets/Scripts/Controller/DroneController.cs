using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : BaseController
{
    Transform owner;
    Vector3 offset;
    DroneSkillStat stat;

    float attackDelayTimer = 0;
    GameObject _projectilePrefab;

    public void Init(Transform owner, Vector3 offset, DroneSkillStat stat)
    {
        this.owner = owner;
        this.offset = offset;
        this.stat = stat;
        _projectilePrefab = Managers.Resource.Load<GameObject>("Prefabs/Projectiles/NormalAttackProjectile");
    }
    public void SetStat(DroneSkillStat stat)
    {
        this.stat = stat;
    }
    private void LateUpdate()
    {
        transform.position = owner.transform.position + offset;
        GameObject target = SearchTarget();
        if (target != null)
        {
            this.transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized, Vector3.back);
        }
        if (Time.time - attackDelayTimer > stat.CoolTime)
        {
            Attack(target);
        }
    }
    void Attack(GameObject target)
    {
        if (target == null) return;

        attackDelayTimer = Time.time;
        GameObject projectileGo = Instantiate(_projectilePrefab);
        projectileGo.transform.position = transform.position;

        Projectile projectile = projectileGo.GetComponent<Projectile>();
        projectile.Init(owner.GetComponent<BaseController>(), target.GetComponent<BaseController>().CenterPosition, stat.Damage, stat.AttackRange, stat.Speed*2f, stat.IsExplode, stat.ExplosionRange, stat.ExplosionDamage, stat.IsPenetrate,stat.Duration, LayerMask.NameToLayer("Enemy"));
        

        projectile.OnHit -= OnHit;
        projectile.OnHit += OnHit;
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

}
