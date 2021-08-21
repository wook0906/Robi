﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BaseController _owner;
    //private GameObject _target;
    private int _damage;
    public int Damage { get { return _damage; } }
    private float _attackRange;
    public float AttackRange { get { return _attackRange; } }
    private float _speed;
    public float Speed { get { return _speed; } }
    private bool _isExplode;
    public bool IsExplode { get { return _isExplode; } }
    private float _explosionRange;
    public float ExplosionRange { get { return _explosionRange; } }
    private float _explosionDamage;
    public float ExplosionDamage { get { return _explosionDamage; } }
    private bool _isPenetrate;
    public bool IsPenetrate { get { return _isPenetrate; } }
    LayerMask _targetLayer;
    public LayerMask TargetLayer { get { return _targetLayer; } }

    private Vector3 _toTarget;
    private Vector3 _targetPos;
    private string _targetTag;
    private Rigidbody _rigid;

    public Action<GameObject, Projectile> OnHit;
    public Action<GameObject> OnKill;

    

    //public void Init(BaseController onwer, GameObject target, int damage,
    //    float attackRange, float speed, bool isExplode,
    //    float explosionRange, float explosionDamage, bool isPenetrate)
    //{
    //    _owner = onwer;
    //    _target = target;
    //    _damage = damage;
    //    _attackRange = attackRange;
    //    _speed = speed;
    //    _isExplode = isExplode;
    //    _explosionRange = explosionRange;
    //    _explosionDamage = explosionDamage;
    //    _isPenetrate = isPenetrate;

    //    _toTarget = (target.GetComponent<BaseController>().CenterPosition - transform.position).normalized;
    //    _targetPos = target.GetComponent<BaseController>().CenterPosition;
    //    _targetTag = target.tag;
    //}

    public void Init(BaseController onwer, Vector3 targetPos, int damage,
        float attackRange, float speed, bool isExplode,
        float explosionRange, float explosionDamage, bool isPenetrate, LayerMask targetLayer)
    {
        _owner = onwer;
        _damage = damage;
        _attackRange = attackRange;
        _speed = speed;
        _isExplode = isExplode;
        _explosionRange = explosionRange;
        _explosionDamage = explosionDamage;
        _isPenetrate = isPenetrate;

        _toTarget = (targetPos - transform.position).normalized;
        _targetPos = targetPos;
        _targetLayer = targetLayer;

    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float deltaDist = Speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.LookRotation(_toTarget, Vector3.back);
        float dist = (_targetPos - transform.position).magnitude;
        if (_attackRange > 0)
        {
            //if(dist < 0.1f)
            //{
            //    transform.position = _targetPos;
            //    Collider[] colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, TargetLayer);
            //    foreach (var collider in colliders)
            //    {
            //        //if(collider.CompareTag(_targetTag))
            //        //{
            //            OnHit(collider.gameObject, this);
            //            collider.GetComponent<CreatureStat>().OnAttacked(_owner, _damage);
            //        //}
            //    }
            //    Destroy(gameObject);
            //}   
        }
        else
        {
            //if (dist < 0.1f)
            //{
            //    OnHit(null, this);
            //    Destroy(gameObject);
            //}
        }
        //deltaDist = Mathf.Clamp(deltaDist, 0, dist);
        transform.position += _toTarget * _speed * Time.deltaTime;
        //_rigid.MovePosition(transform.position + _toTarget * deltaDist);
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
        if (_isPenetrate)
            return;
        Destroy(gameObject);
    }
}
