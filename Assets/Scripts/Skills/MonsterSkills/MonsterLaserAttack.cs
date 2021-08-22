using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterLaserAttack : AttackSkillBase
{
    GameObject target;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.MonsterLaser;
        gameObject.AddComponent<SkillStat>().SetStat(_type);
        base.Init(owner, muzzleTransform, parent);
        target = MonsterController.target;
    }

    public override bool UseSkill()
    {
        StartCoroutine(Fire());
        return true;
    }

    private IEnumerator Fire()
    {
        //_muzzleTransform.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(.5f); //파티클에 맞춰 차징 시간 설정

        int attackCount = (int)(Stat.Duration / Stat.DelayPerAttack);

        Debug.DrawRay(target.GetComponent<BaseController>().CenterPosition, _owner.CenterPosition, Color.cyan, Stat.Duration);
        while (attackCount > 0)
        {
            Vector3 targetDir = (target.GetComponent<BaseController>().CenterPosition - _owner.CenterPosition).normalized;
            RaycastHit hit;
            
            if(Physics.Raycast(_owner.CenterPosition, targetDir, out hit, Stat.AttackRange, 1 << LayerMask.NameToLayer("Player")))
            {
                hit.transform.GetComponent<CreatureStat>().OnAttacked(_owner, Stat.Damage);
                OnHit(hit.transform.gameObject, null);
            }
            OnFire();
            attackCount--;
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        //_muzzleTransform.GetComponent<ParticleSystem>().Stop();
        //_owner.State = Define.CreatureState.Idle;
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }


    private void OnDrawGizmos()
    {
        if (Stat == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, Stat.AttackRange * 2f);
    }
}