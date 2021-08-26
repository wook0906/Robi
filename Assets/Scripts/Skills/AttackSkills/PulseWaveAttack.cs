using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class PulseWaveAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.PulseWave;
        base.Init(owner, muzzleTransform, parent);
        Stat.InitSkillStat(_type);
    }

    public override bool UseSkill()
    {
        GameObject[] targets = SearchTargets(Stat.NumOfProjectilePerBurst);
        if (targets == null || targets.Length == 0)
            return false;

        _owner.State = Define.CreatureState.Attack;
        foreach (var target in targets)
        {
            if (target == null)
                continue;
            GameObject effect = Managers.Resource.Instantiate("Effects/PulseWave");
            effect.transform.position = _owner.GetComponent<PlayerController>().launchPoints[(int)Define.LaunchPointType.Head].position;
            effect.transform.rotation = Quaternion.LookRotation((target.GetComponent<BaseController>().CenterPosition - _owner.transform.position).normalized, Vector3.back);
            effect.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0,0, Vector3.Distance(target.GetComponent<BaseController>().CenterPosition, _owner.transform.position)));
            effect = Managers.Resource.Instantiate("Effects/PulseWaveHit");
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
        Debug.Log("Fire");
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile = null)
    {
        Debug.Log($"Hit target:{target.name}");
    }

    public override void OnKill(GameObject target)
    {
        Debug.Log($"Kill target:{target.name}");
    }
}
