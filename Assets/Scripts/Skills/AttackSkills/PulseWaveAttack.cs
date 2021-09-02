using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class PulseWaveAttack : AttackSkillBase
{
    PulseWaveAttackSkillStat pulseWaveStat;
    GameObject hitEffectPrefab;

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.PulseWave;
        pulseWaveStat = gameObject.AddComponent<PulseWaveAttackSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        pulseWaveStat.InitSkillStat(_type);
        _prefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/PulseWave");
        hitEffectPrefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/PulseWaveHit");
    }

    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");
        GameObject[] targets = SearchTargets(Stat.NumOfProjectilePerBurst);
        if (targets == null || targets.Length == 0)
            return false;

        _owner.State = Define.CreatureState.Attack;
        foreach (var target in targets)
        {
            if (target == null)
                continue;
            GameObject effect = Instantiate(_prefab);
            effect.transform.position = _owner.GetComponent<PlayerController>().launchPoints[(int)Define.LaunchPointType.Head].position;
            effect.transform.rotation = Quaternion.LookRotation((target.GetComponent<BaseController>().CenterPosition - _owner.transform.position).normalized, Vector3.back);
            effect.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0,0, Vector3.Distance(target.GetComponent<BaseController>().CenterPosition, _owner.transform.position)));
            effect = Instantiate(hitEffectPrefab);
            effect.transform.position = target.GetComponent<BaseController>().CenterPosition;

            //Debug.Log($"[PulseWave]Hit Target:{target.name}");
            //Debug.DrawRay(transform.position, (target.GetComponent<BaseController>().CenterPosition - transform.position).normalized * 5f, Color.green, 1f);
            CreatureStat enemyStat = target.GetComponent<CreatureStat>();
            enemyStat.OnAttacked(_owner, Stat.Damage);
            OnHit(target);
        }
        OnFire();
        return true;
    }

    public override void OnFire()
    {
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile = null)
    {
       // Debug.Log($"Hit target:{target.name}");
    }

    public override void OnKill(GameObject target)
    {
        Debug.Log($"Kill target:{target.name}");
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        pulseWaveStat.LevelUp(grade);
    }
}
