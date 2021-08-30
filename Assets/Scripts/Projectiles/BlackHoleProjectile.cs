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
    BlackHoleAttackSkillStat _stat;
    private string _targetTag;
    GameObject effectPrefab;

    Vector3 maxScale;

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
    public void Init(BaseController onwer, GameObject target, BlackHoleAttackSkillStat stat)
    {
        _owner = onwer;
        _target = target;

        _stat = stat;
        _targetTag = target.tag;

        maxScale = transform.localScale * 3f;

        effectPrefab = Managers.Resource.Load<GameObject>("Prefabs/Effects/BlackHoleExplosion");
    }
    public void DragToCenter()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, (transform.localScale.x * 4.5f) / 2f , 1 << 8);
        foreach (var item in colls)
        {
            MonsterController monster = item.GetComponent<MonsterController>();
            monster.State = Define.CreatureState.Dragged;
            //damage를 흡입력으로 사용함.
            float deltaDist = (_stat.Damage/5f) * transform.localScale.x * Time.deltaTime;
            Vector3 dir = (transform.position - monster.CenterPosition);
            if (dir.magnitude <= (transform.localScale.x / 2f))
            {
                Instantiate(effectPrefab, monster.CenterPosition, Quaternion.identity);
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
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, maxScale, Time.deltaTime);
        else
            transform.localScale = Vector3.LerpUnclamped(transform.localScale, Vector3.zero, Time.deltaTime * 2f);
        if (durationTimer >= _stat.Duration)
            Managers.Resource.Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, (maxScale.x * 4.5f) / 2f, 1 << 8))
        {
            item.GetComponent<MonsterController>().State = Define.CreatureState.Move;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f,0f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, (transform.localScale.x * 4.5f) / 2f);
        Gizmos.color = new Color(1f, 0f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, (transform.localScale.x) / 2f);
    }
}
