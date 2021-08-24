using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterController : MonsterController
{
    //private Vector3 homeDir;
    float lastAttackTime;


    public override Define.CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            _state = value;

            //switch (_state)
            //{
            //    case Define.CreatureState.ComebackHome:
            //        homeDir = (chaseStartPos - transform.position).normalized;
            //        break;
            //    default:
            //        break;
            //}
        }
    }
    private void Update()
    {
        if (target == null)
            return;

        switch (State)
        {
            case Define.CreatureState.Idle:
                break;
            case Define.CreatureState.Move:
                Move();
                break;
            case Define.CreatureState.Chase:
                Chase();
                break;
            case Define.CreatureState.Attack:
                Attack();
                break;
            case Define.CreatureState.ComebackHome:
                //ComebackHome();
                break;
            default:
                break;
        }

        _attackSkillDispatcher.Update(Time.deltaTime);
    }

    protected override void Init()
    {
        base.Init();
        State = Define.CreatureState.Move;

    }

    private bool IsPlayerInAttackRange()
    {
        MonsterStat stat = _stat as MonsterStat;
        float dist = (target.transform.position - transform.position).sqrMagnitude;
        if (dist <= (stat.AttackRange * stat.AttackRange))
            return true;
        return false;
    }

    private void Attack()
    {
        //MonsterStat stat = _stat as MonsterStat;
        if (!IsPlayerInAttackRange())
        {
            State = Define.CreatureState.Chase;
            return;
        }

        //if (Time.time - lastAttackTime < stat.AttackInterval)
        //    return;

        //Projectile projectile = Managers.Resource.Instantiate("Projectiles/NormalAttackProjectile").GetComponent<Projectile>();
        //projectile.transform.position = CenterPosition;
        //projectile.OnHit -= OnHit;
        //projectile.OnHit += OnHit;
        //projectile.Init(this, target.GetComponent<BaseController>().CenterPosition, stat.Damage, stat.AttackRange, stat.MoveSpeed, false, 0f, 0f, false, LayerMask.NameToLayer("Player"));

        //lastAttackTime = Time.time;
    }

    //private bool CanChase()
    //{
    //    float dist = (target.transform.position - transform.position).sqrMagnitude;
    //    if (dist <= (ChaseRange * ChaseRange))
    //        return true;
    //    return false;
    //}

    protected override void Chase()
    {
        MonsterStat stat = _stat as MonsterStat;
        moveDir = (target.transform.position - transform.position).normalized;
        transform.position += moveDir.normalized * stat.MoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.back);

        if (IsPlayerInAttackRange())
            State = Define.CreatureState.Attack;
        else if (Vector3.Distance(transform.position, target.transform.position) >= stat.DetectRange)
            State = Define.CreatureState.Move;
    }

    public void OnHit(GameObject target, Projectile projectile)
    {
        //ParticleSystem effect = Managers.Resource.Instantiate("Effects/Explosion").GetComponent<ParticleSystem>();
        //Vector3 pos = projectile.transform.position;
        //effect.transform.position = pos;
        //effect.Play();
    }

    //private void ComebackHome()
    //{
    //    if(CanChase())
    //    {
    //        State = Define.CreatureState.Chase;
    //        return;
    //    }

    //    float dist = (chaseStartPos - transform.position).magnitude;
    //    if(dist <= .1f)
    //    {
    //        transform.position = chaseStartPos;
    //        State = Define.CreatureState.Move;
    //        return;
    //    }

    //    float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0f, dist);
    //    transform.position += homeDir * moveDist;
    //}
}
