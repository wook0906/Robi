using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerAttack : AttackSkillBase
{
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = Define.AttackSkillType.FlameThrower;
        base.Init(owner, muzzleTransform, parent);
        Stat.InitSkillStat(_type);
        _muzzleTransform.transform.SetParent(owner.transform);
        _muzzleTransform.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public override bool UseSkill()
    {
        _owner.State = Define.CreatureState.Attack;
        Debug.Log("화염방사 사용");
        StartCoroutine(Fire());
        return true;
    }

    private IEnumerator Fire()
    {
        _muzzleTransform.GetComponent<ParticleSystem>().Play();

        int attackCount = (int)(Stat.Duration / Stat.DelayPerAttack);

        while (attackCount > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Stat.AttackRange * 2f, 1 << 8);

            foreach (Collider collider in colliders)
            {
                Vector3 targetDir = (collider.transform.position - this.transform.position).normalized;
                float angle = Vector3.Angle(targetDir, -transform.up);

                if (angle <= 30f && collider.GetComponent<CreatureStat>())
                {
                    collider.GetComponent<CreatureStat>().OnAttacked(_owner, Stat.Damage);
                    OnHit(collider.gameObject, null);
                }
            }
            OnFire();
            attackCount--;
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        _muzzleTransform.GetComponent<ParticleSystem>().Stop();
        _owner.State = Define.CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }


    private void OnDrawGizmos()
    {
        if (Stat == null)
            return;

        Gizmos.color = new Color(1f, 1f, 0f, 0.2f);
        Gizmos.DrawSphere(transform.position, Stat.AttackRange * 2f);
    }

}
