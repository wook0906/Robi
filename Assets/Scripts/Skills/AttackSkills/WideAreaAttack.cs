using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideAreaAttack : MonoBehaviour
{
    private BaseController _owner;

    private int     _damage;
    public  int     Damage { get { return _damage; } }
    private float   _attackRange;
    public  float   AttackRange { get { return _attackRange; } }
    private float   _duration;
    public  float   Duration { get { return _duration; } }
    private float   _delayPerAttack;
    public  float   DelayPerAttack { get { return _delayPerAttack; } }

    GameObject napalmEffects;
    ParticleSystemRenderer pr;
    ParticleSystemRenderer pr1;

    public Action<GameObject, WideAreaAttack> OnHit;
    public Action<GameObject> OnKill;

    public void Init(BaseController owner,int damage, float attackRange,
        float duration, float delayPerAttack, NapalmAttackSkillStat stat)
    {
        napalmEffects = Managers.Resource.Instantiate("Effects/NapalmExplosion");
        napalmEffects.transform.SetParent(transform);
        napalmEffects.transform.localPosition = Vector3.zero;
        pr = napalmEffects.transform.Find("Ground").transform.Find("Crack").GetComponent<ParticleSystemRenderer>();
        pr1 = napalmEffects.transform.Find("Ground").transform.Find("Crack1").GetComponent<ParticleSystemRenderer>();

        _owner = owner;
        _damage = damage;
        _attackRange = attackRange;
        _duration = duration;
        _delayPerAttack = delayPerAttack;

        StartCoroutine(CoAttack());
        
    }

    private IEnumerator CoAttack()
    {

        int attackCount = (int)(Duration / DelayPerAttack);
        float effectAlpha = 1;
        float offsetValue = 1f / attackCount;
        while (attackCount > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange, 1 << 8);
            foreach (var item in colliders)
            {
                item.GetComponent<CreatureStat>().OnAttacked(_owner, Damage);
            }
            yield return new WaitForSeconds(DelayPerAttack);
            effectAlpha -= offsetValue;
            Debug.Log(effectAlpha);
            pr.material.SetFloat("_Val", effectAlpha);
            pr1.material.SetFloat("_Val", effectAlpha);
            attackCount--;
        }
      
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_owner == null)
            return;
        Gizmos.color = new Color(1f, 0f, 1f, .3f);
        Gizmos.DrawSphere(transform.position, AttackRange);
    }
}
