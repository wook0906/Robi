using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    //protected Define.MonsterAttackType attackType = Define.MonsterAttackType.Short;
    
    [SerializeField]
    public static GameObject target; // Only Player
    protected bool IsInScreen;

    protected Vector3 chaseStartDir;

    public List<AttackSkillBase> attackSkills = new List<AttackSkillBase>();

    private void Start()
    {
        Init();
        _stat = gameObject.AddComponent<MonsterStat>();
        State = Define.CreatureState.Move;

    }

    void Update()
    {
        if (!target) return;

        //targetPos = target.transform.position;
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
                break;
            case Define.CreatureState.ComebackHome:
                break;
            case Define.CreatureState.Knockback:
                Knockback();
                break;
            case Define.CreatureState.Dragged:
                break;
            default:
                break;
        }

        _activeSkillDispatcher.Update(Time.deltaTime);


    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Attack player
            ShieldSkill shield = collision.GetComponentInChildren<ShieldSkill>();
            if (shield)
            {
                if (shield.ShieldLevel > 0)
                    shield.ShieldLevel--;
                else
                    collision.GetComponent<CreatureStat>().OnAttacked(this, _stat.Damage);
            }
            else
            {
                collision.GetComponent<CreatureStat>().OnAttacked(this, _stat.Damage);
            }
            Managers.Object.RemoveMonster(this);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        //TODO: 화면 밖을 나간 몬스터에대한 처리를 어떻게 할 것인가??
        Managers.Object.RemoveMonsterInScreen(this);
        if (!IsInScreen)
        {   
            return;
        }
        Destroy(gameObject, 0.3f);
    }

    private void OnBecameVisible()
    {
        if (!IsInScreen)
        {
            Managers.Object.AddMonsterInScreen(this);
            IsInScreen = true;
        }
    }

    protected virtual void Init()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        //TEMP
        if (target == null)
            return;

        //moveDir = (target.transform.position - transform.position);
        chaseStartDir = (target.transform.position - transform.position).normalized;

        _activeSkillDispatcher = new ActiveSkillDispatcher();
        _activeSkillDispatcher.Init(this);


    }


    public override void OnKilled(BaseController controller)
    {
        target = null;
    }
    protected virtual void Move()
    {
        MonsterStat stat = _stat as MonsterStat;
        transform.position += chaseStartDir * stat.MoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(chaseStartDir, Vector3.back);
        
        if (Vector3.Distance(transform.position, target.transform.position) <= stat.DetectRange)
        {
            State = Define.CreatureState.Chase;
        }
    }

    protected virtual void Chase()
    {
        MonsterStat stat = _stat as MonsterStat;
        moveDir = (target.transform.position - transform.position).normalized;
        transform.position += moveDir.normalized * stat.MoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.back);

        if (Vector3.Distance(transform.position, target.transform.position) >= stat.DetectRange)
            State = Define.CreatureState.Move;
    }
    
}
