using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = Define.AttackSkillType.Bombing;
        base.Init(owner, muzzleTransform, parent);
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
                projectile.Init(_owner, pos, 0, 0f, Stat.Speed,
                    Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
                    Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Enemy"));

                projectile.OnHit -= OnHit;
                projectile.OnHit += OnHit;
                projectile.OnKill -= OnKill;
                projectile.OnKill += OnKill;
            }
            OnFire();
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        _owner.State = Define.CreatureState.Idle;
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


        Collider[] colliders = Physics.OverlapSphere(projectile.transform.position, projectile.ExplosionRange);
        if (colliders.Length == 0)
            return;

        int count = 0;

        foreach (var collider in colliders)
        {
            if (_owner == null)
                return;

            if (collider.gameObject == projectile.gameObject || collider.gameObject == _owner.gameObject)
                continue;

            CreatureStat stat = collider.GetComponent<CreatureStat>();
            if (stat == null)
                continue;
            stat.OnAttacked(_owner, projectile.ExplosionDamage);
            count++;
        }
    }
}
