using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class BaseController : MonoBehaviour
{
    protected CreatureStat _stat;
    public CreatureStat Stat { get { return _stat; } }
    protected Vector3 moveDir;

    protected ActiveSkillDispatcher _attackSkillDispatcher;
    public ActiveSkillDispatcher AttackSkillDispatcher { get { return _attackSkillDispatcher; } }


    //public float attackRange = 6f;
    //public float delayPerAttack = 1f;
    //public float lastAttackTime = 0f;

    public Vector3 CenterPosition
    {
        get
        {
            Transform center = transform.Find("CenterPosition");
            if (!center)
            {
                Debug.Log("center is Null!");
            }
            return center.position;
        }
    }

    [SerializeField]
    protected CreatureState _state;
    public virtual CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            _state = value;
        }
    }
    protected Vector3 _destPos;
    public Vector3 DestPos
    {
        get { return _destPos; }
        set { _destPos = value; }
    }
    
    public virtual void OnKilled(BaseController controller)
    {

    }

    public virtual void Knockback()
    {
        float deltaDist = _stat.MoveSpeed * Time.fixedDeltaTime;
        Vector3 dir = (DestPos - transform.position);
        if(dir.magnitude <= 0.1f)
        {
            transform.position = DestPos;
            State = CreatureState.Move;
            return;
        }

        deltaDist = Mathf.Clamp(deltaDist, 0, dir.magnitude);
        transform.position += dir.normalized * deltaDist;
    }

}
