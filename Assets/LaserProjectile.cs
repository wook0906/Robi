using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private BaseController _owner;
    private float _damage;
    public float Damage { get { return _damage; } }
    private float _duration;
    public float Duration { get { return _duration; } }
    LayerMask _targetLayer;
    public LayerMask TargetLayer { get { return _targetLayer; } }



    public Action<GameObject, LaserProjectile> OnHit;
    public Action<GameObject> OnKill;

    [SerializeField]
    public Transform mainParticle;
    Vector3 _targetPos;

    public void Init(BaseController onwer, float damage,
        float duration, LayerMask targetLayer, float scaleValue = 1f, float colliderActiveDelayTime = 0.75f)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        mainParticle.transform.localScale = new Vector3(scaleValue, mainParticle.transform.localScale.y, mainParticle.transform.localScale.z);
        collider.size = new Vector3(scaleValue, collider.size.y, collider.size.z);
        _owner = onwer;
        _damage = damage;
        _duration = duration;
        _targetLayer = targetLayer;
        StartCoroutine(AttackProgress(colliderActiveDelayTime));
    }
    IEnumerator AttackProgress(float colliderActiveDelayTime)
    {
        float timer = 0f;
        while (timer <= colliderActiveDelayTime)
        {
            //transform.rotation = Quaternion.LookRotation(SearchTarget().GetComponent<BaseController>().CenterPosition, Vector3.back);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.SetParent(null);
        BaseController target = SearchTarget().GetComponent<BaseController>();
        transform.rotation = Quaternion.LookRotation((target.CenterPosition - _owner.CenterPosition).normalized, Vector3.back);
        GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.gameObject.layer != TargetLayer)
        {
            return;
        }

        OnHit(other.gameObject, this);
        other.GetComponent<CreatureStat>().OnAttacked(_owner, Damage);
    }

    protected GameObject SearchTarget()
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        GameObject closetTarget = null;
        float closetDist = float.MaxValue;

        foreach (MonsterController target in monsters)
        {
            float dist = (target.transform.position - _owner.transform.position).sqrMagnitude;
            if (dist < closetDist)
            {
                closetTarget = target.gameObject;
                closetDist = dist;
            }
        }

        return closetTarget;
    }


}
