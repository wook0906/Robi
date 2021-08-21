using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{
    private BaseController _owner;
    private GameObject _target;
    private float attackTimer;
    private float durationTimer;

    //AttackSkillStat _stat;
    SkillStat _stat;
    private string _targetTag;
    GameObject effectPrefab;

    Vector3 targetScale;

    public Action<GameObject, BlackHoleProjectile> OnHit;
    public Action<GameObject> OnKill;

    //public void Init(BaseController onwer, GameObject target, AttackSkillStat stat)
    //{
    //    _owner = onwer;
    //    _target = target;

    //    _stat = stat;
    //    _targetTag = target.tag;

    //    targetScale = transform.localScale * 3f;

    //    effectPrefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/BlackHoleExplosion");
    //}
    public void Init(BaseController onwer, GameObject target, SkillStat stat)
    {
        _owner = onwer;
        _target = target;

        _stat = stat;
        _targetTag = target.tag;

        targetScale = transform.localScale * 3f;

        effectPrefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/BlackHoleExplosion");
    }
    public void DragToCenter()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, (_stat.AttackRange * 2.5f) * transform.localScale.x, 1 << 8);
        foreach (var item in colls)
        {
            MonsterController monster = item.GetComponent<MonsterController>();
            monster.State = Define.CreatureState.Dragged;
            //damage를 흡입력으로 사용함.
            float deltaDist = (_stat.Damage/5f) * transform.localScale.x * Time.fixedDeltaTime;
            Vector3 dir = (transform.position - item.transform.position);
            if (dir.magnitude <= (_stat.AttackRange / 2f) * transform.localScale.x)
            {
                Instantiate(effectPrefab, transform.position,Quaternion.identity);
                item.GetComponent<CreatureStat>().OnAttacked(_owner, Mathf.Infinity);
                continue;
            }
            deltaDist = Mathf.Clamp(deltaDist, 0, dir.magnitude);
            item.transform.position += dir.normalized * deltaDist;
        }
    }

    private void LateUpdate()
    {
        DragToCenter();
        durationTimer += Time.deltaTime;

        if (durationTimer <= _stat.Duration - 1)
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, targetScale, Time.deltaTime);
        else
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, Time.deltaTime * 2f);
        if (durationTimer >= _stat.Duration)
            Managers.Resource.Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, (_stat.AttackRange * 2.5f) * targetScale.x, 1 << 8))
        {
            item.GetComponent<MonsterController>().State = Define.CreatureState.Move;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f,0f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, (_stat.AttackRange * 2.5f) * targetScale.x);
        Gizmos.color = new Color(1f, 0f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, _stat.AttackRange / 2f);
    }
}
