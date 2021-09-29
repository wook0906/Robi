using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ImpactWaveAttack : AttackSkillBase
{
    GameObject hitEffect;
    ImpactWaveAttackStat impactWaveStat;

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.ImpactWave;
        impactWaveStat = gameObject.AddComponent<ImpactWaveAttackStat>();
        base.Init(owner, muzzleTransform, parent);
        impactWaveStat.InitSkillStat(_type);
        _prefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/ImpactWave");
        hitEffect = Managers.Resource.Load<GameObject>("Prefabs/Effects/ImpactWaveHit");
    }

    protected override GameObject[] SearchTargets(int targetCount = 0)
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        if (monsters.Count == 0)
            return null;

        if (targetCount == 0)
            targetCount = monsters.Count;

        List<GameObject> closeTargets = new List<GameObject>();
        monsters.Sort((m1, m2) =>
        {
            float dist1 = (m1.transform.position - _owner.transform.position).sqrMagnitude;
            float dist2 = (m2.transform.position - _owner.transform.position).sqrMagnitude;
            return dist1.CompareTo(dist2);
        });

        for (int i = 0; i < monsters.Count; i++)
        {
            if((transform.position - monsters[i].transform.position).magnitude < Stat.AttackRange)
            {
                closeTargets.Add(monsters[i].gameObject);
            }
        }
        return closeTargets.ToArray();
    }
    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");
        GameObject[] targets = SearchTargets();

        GameObject impactEffect = Instantiate(_prefab, _owner.CenterPosition, Quaternion.identity);
        //Vector3 newScale = new Vector3(Stat.AttackRange, Stat.AttackRange * 0.5f, Stat.AttackRange * 0.5f);
        impactEffect.transform.localScale *= Stat.AttackRange * 2f;
        if (targets == null || targets.Length == 0)
        {
            OnFire();
            return true;
        }

        _owner.State = Define.CreatureState.Attack;

       

        foreach (var target in targets)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Vector3 destPos = dir * Stat.AttackRange;
            target.GetComponent<BaseController>().DestPos = destPos;
            target.GetComponent<BaseController>().State = Define.CreatureState.Knockback;
            //Debug.DrawLine(transform.position, destPos, Color.blue, 1f);
            Instantiate(hitEffect, target.transform.position, Quaternion.identity);
            target.GetComponent<CreatureStat>().OnAttacked(_owner, Stat.Damage);
        }
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
        Debug.Log($"Hit target:{target.name}");
    }

    public override void OnKill(GameObject target)
    {
        Debug.Log($"Kill target:{target.name}");
    }

    private void OnDrawGizmos()
    {
        if (Stat == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, Stat.AttackRange);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        impactWaveStat.LevelUp(grade);
    }
}
