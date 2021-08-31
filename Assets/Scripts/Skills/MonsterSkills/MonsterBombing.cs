using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBombing : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = Define.SkillType.MonsterBombing;
        base.Init(owner, muzzleTransform, parent);
        Stat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/BombingAttackProjectile");
    }

    public override bool UseSkill()
    {
        StartCoroutine("Fire");
        return true;
    }

    private IEnumerator Fire()
    {
        int attackCount = (int)(Stat.Duration / Stat.DelayPerAttack);
        while (attackCount > 0)
        {
            attackCount--;
            for (int i = 0; i < Stat.NumOfProjectilePerBurst; i++)
            {
                GameObject go = Instantiate<GameObject>(_prefab);
                Vector3 dir = Random.onUnitSphere;
                Vector3 pos = transform.position + dir * Stat.AttackRange;
                pos.z = -20f;
                pos.x += 10f;
                go.transform.position = pos;

                pos.z = 0f;
                pos.x -= 10f;
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.Init(_owner, pos, 0, Stat.AttackRange, Stat.Speed,
                    Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
                    Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Player"));

                projectile.OnHit -= OnHit;
                projectile.OnHit += OnHit;
                projectile.OnKill -= OnKill;
                projectile.OnKill += OnKill;
                projectile.OnArrive -= OnArrive;
                projectile.OnArrive += OnArrive;
            }
            OnFire();
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        //_owner.State = Define.CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        Debug.Log("폭격 폭발!");
        ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        Vector3 pos = projectile.transform.position;
        pos.z -= Stat.ExplosionRange;
        effect.transform.position = pos;
        effect.Play();


        effect = Managers.Resource.Instantiate("Effects/ExplosionMark").GetComponent<ParticleSystem>();
        effect.GetComponent<ParticleAutoDestroy>().Init(0f, Stat.ExplosionRange * 2f);
        pos = projectile.transform.position;
        pos.z = -0.02f;
        effect.transform.position = pos;
        effect.Play();


        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange, 1<<LayerMask.NameToLayer("Player"));
        if (colliders.Length == 0)
            return;


        foreach (var collider in colliders)
        {
            CreatureStat stat = collider.GetComponent<CreatureStat>();
            if (stat == null)
                continue;
            stat.OnAttacked(_owner, projectile.ExplosionDamage);
        }
    }
    public override void OnArrive(Vector3 targetPos, Projectile projectile)
    {
        Debug.Log("OnArrive!");
        ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        Vector3 pos = projectile.transform.position;
        pos.z -= Stat.ExplosionRange;
        effect.transform.position = pos;
        effect.Play();


        effect = Managers.Resource.Instantiate("Effects/ExplosionMark").GetComponent<ParticleSystem>();
        effect.GetComponent<ParticleAutoDestroy>().Init(0f, Stat.ExplosionRange * 2f);
        pos = projectile.transform.position;
        pos.z = -0.02f;
        effect.transform.position = pos;
        effect.Play();


        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange, 1<< LayerMask.NameToLayer("Player"));
        if (colliders.Length == 0)
            return;


        foreach (var collider in colliders)
        {
            CreatureStat stat = collider.GetComponent<CreatureStat>();
            if (stat == null)
                continue;
            stat.OnAttacked(_owner, projectile.ExplosionDamage);
        }
    }
}
