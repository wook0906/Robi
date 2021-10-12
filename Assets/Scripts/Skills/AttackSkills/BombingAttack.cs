using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingAttack : AttackSkillBase
{
    BombingAttackStat bombingAttackStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = Define.SkillType.PlayerBombing;
        bombingAttackStat = gameObject.AddComponent<BombingAttackStat>();
        base.Init(owner, muzzleTransform, parent);
        bombingAttackStat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/BombingAttackProjectile");
    }

    public override bool UseSkill()
    {
        StartCoroutine("Fire");
        return true;
    }

    private IEnumerator Fire()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {bombingAttackStat.CoolTime}, Damage : {bombingAttackStat.Damage}, AttackRange : {bombingAttackStat.AttackRange}, NumOfProjectilePerBurst {bombingAttackStat.NumOfProjectilePerBurst}, Speed : {bombingAttackStat.Speed}, IsExplode : {bombingAttackStat.IsExplode}, ExplosionRange : {bombingAttackStat.ExplosionRange}, ExplosionDamage : {bombingAttackStat.ExplosionDamage}, isPernerate : {bombingAttackStat.IsPenetrate}, Duration : {bombingAttackStat.Duration}, DelayPerAttack : {bombingAttackStat.DelayPerAttack}");
        int attackCount = (int)(bombingAttackStat.Duration / bombingAttackStat.DelayPerAttack);
        while (attackCount > 0)
        {
            attackCount--;
            for (int i = 0; i < bombingAttackStat.NumOfProjectilePerBurst; i++)
            {
                GameObject go = Instantiate<GameObject>(_prefab);
                Vector3 dir = Random.onUnitSphere;
                Vector3 pos = transform.position + dir * bombingAttackStat.AttackRange;
                pos.z = -20f;
                pos.x += 10f;
                go.transform.position = pos;

                pos.z = 0f;
                pos.x -= 10f;
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.Init(_owner, pos, bombingAttackStat.Damage, bombingAttackStat.AttackRange, bombingAttackStat.Speed,
                    bombingAttackStat.IsExplode, bombingAttackStat.ExplosionRange, bombingAttackStat.ExplosionDamage,
                    bombingAttackStat.IsPenetrate, bombingAttackStat.Duration, LayerMask.NameToLayer("Enemy"));

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
        _owner.State = Define.CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
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
        }
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        bombingAttackStat.LevelUp(grade);
    }
    public override void OnArrive(Vector3 targetPos, Projectile projectile)
    {
        Debug.Log($"{this.name} On Arrived!");
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
        }
    }

}
