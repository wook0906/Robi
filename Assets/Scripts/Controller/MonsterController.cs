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

    Rigidbody rigidBody;

    float knockbackTimer;

    MonsterStat Stat;

    private void Start()
    {
        Stat = gameObject.GetOrAddComponent<MonsterStat>();
        _stat = Stat;
        State = Define.CreatureState.Move;
        rigidBody = GetComponent<Rigidbody>();
        Init();
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
            case Define.CreatureState.Controlled:
                //외부 요인으로 인해 자동으로 상태해제 될 때까지 아무것도 하지 않는다. 
                break;
            default:
                break;
        }

        _attackSkillDispatcher.Update(Time.deltaTime);


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.GetComponent<PlayerStat>().OnAttacked(this, Stat.Damage);

            if (Stat.mobType >= Define.MonsterType.EC01 &&
                Stat.mobType <= Define.MonsterType.EF07)
            {
                moveDir *= -1f;
                State = Define.CreatureState.Knockback;
            }
            else
            {
                Managers.Object.RemoveMonster(this);
                gameObject.SetActive(false);
            }
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

        _attackSkillDispatcher = new ActiveSkillDispatcher();
        _attackSkillDispatcher.Init(this);
     
    }


    public override void OnKilled(BaseController controller)
    {
        target = null;
    }
    protected virtual void Move()
    {
        //MonsterStat stat = base._stat as MonsterStat;
        //transform.position += chaseStartDir * stat.MoveSpeed * Time.deltaTime;
        //rigidBody.MovePosition(transform.position + chaseStartDir * stat.MoveSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(chaseStartDir, Vector3.back);
        //rigidBody.velocity = Vector3.zero;

        //if (Vector3.Distance(transform.position, target.transform.position) <= stat.DetectRange)
        //{
        State = Define.CreatureState.Chase;
        //}
    }

    protected virtual void Chase()
    {
        MonsterStat stat = base._stat as MonsterStat;
        moveDir = (target.transform.position - transform.position).normalized;
        moveDir.z = 0f;
        transform.position += moveDir.normalized * stat.MoveSpeed * Time.deltaTime;
        rigidBody.velocity = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.back);

        //if (Vector3.Distance(transform.position, target.transform.position) >= stat.DetectRange)
        //    State = Define.CreatureState.Move;
    }
    public override void Knockback()
    {

        MonsterStat stat = base._stat as MonsterStat;
        transform.position += moveDir.normalized * stat.MoveSpeed * 10f * Time.deltaTime;
        //rigidBody.MovePosition(transform.position + moveDir * stat.MoveSpeed * 10f * Time.deltaTime);
        rigidBody.velocity = Vector3.zero;

        //transform.rotation = Quaternion.LookRotation(moveDir, Vector3.back);
        if (knockbackTimer >= 0.15f)
        {
            State = Define.CreatureState.Chase;
            knockbackTimer = 0f;
            return;
        }
        knockbackTimer += Time.deltaTime;
    }

}
