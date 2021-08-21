using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : BaseController
{
    public Vector3 CameraOffset;
    public PlayerStat Stat { get { return _stat as PlayerStat; } }
    private Vector2 touchStartPos;
    [SerializeField]
    private Transform camTransform;
    
    private List<AttackSkillBase> attackSkills = new List<AttackSkillBase>();
    [SerializeField]
    private Field field;
    private LayerMask tileMask;
    private GameObject body;

    public List<Transform> launchPoints;


    #region Unity Function
    private void Start()
    {
        tileMask = LayerMask.GetMask("Tile");
        State = CreatureState.Idle;
        camTransform = Camera.main.transform;
        _stat = gameObject.AddComponent<PlayerStat>();
        _activeSkillDispatcher = new ActiveSkillDispatcher();
        _activeSkillDispatcher.Init(this);
        body = transform.Find("Model").transform.Find("Robi").transform.Find("Body").gameObject;



        field = GameObject.FindGameObjectWithTag("Field").GetComponent<Field>();
        transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.back);

        

        //1 기본
        GameObject normalAttack = new GameObject() { name = "NormalAttack" };
        normalAttack.transform.parent = transform;
        normalAttack.transform.localPosition = Vector3.zero;
        NormalAttack normalSkill = normalAttack.AddComponent<NormalAttack>();
        normalAttack.AddComponent<SkillStat>().SetStat(normalSkill.Type);
        normalSkill.Init(this, null, null);
        attackSkills.Add(normalSkill);

        //2 지원폭격
        //GameObject bombingGO = new GameObject() { name = "Bombing" };
        //bombingGO.transform.parent = transform;
        //bombingGO.transform.localPosition = Vector3.zero;
        //BombingAttack bombingSkill = bombingGO.AddComponent<BombingAttack>();
        //bombingGO.AddComponent<AttackSkillStat>();
        //bombingSkill.Init(this, null, null);
        //attackSkills.Add(bombingSkill);

        //3 미사일
        //GameObject missileAttack = new GameObject() { name = "Missle" };
        //missileAttack.transform.parent = transform;
        //missileAttack.transform.localPosition = Vector3.zero;
        //MissileAttack missile = missileAttack.AddComponent<MissileAttack>();
        //missileAttack.AddComponent<AttackSkillStat>();
        //missile.Init(this, null, null);
        //attackSkills.Add(missile);

        //4 레이저
        //GameObject laserAttack = new GameObject() { name = "Laser" };
        //laserAttack.transform.parent = transform;
        //laserAttack.transform.localPosition = Vector3.zero;
        //laserAttack.AddComponent<LaserAttackSkillStat>();
        //laserAttack.AddComponent<LaserAttack>().Init(this, null, null);
        //attackSkills.Add(laserAttack.GetComponent<LaserAttack>());

        //5 화염방사
        //GameObject flamethrower = new GameObject() { name = "FlameThrower" };
        //flamethrower.transform.parent = transform;
        //flamethrower.transform.localPosition = Vector3.zero;
        //flamethrower.AddComponent<FlameThrowerAttackSkillStat>();
        //flamethrower.AddComponent<FlameThrowerAttack>().Init(this, Managers.Resource.Instantiate("Effects/Flamethrower").transform, null);
        //attackSkills.Add(flamethrower.GetComponent<FlameThrowerAttack>());

        //6 충격파
        //GameObject impactWaveAttack = new GameObject() { name = "ImpactWave" };
        //impactWaveAttack.transform.parent = transform;
        //impactWaveAttack.transform.localPosition = Vector3.zero;
        //impactWaveAttack.AddComponent<ImpactWaveAttackStat>();
        //impactWaveAttack.AddComponent<ImpactWaveAttack>().Init(this, null, null);
        //attackSkills.Add(impactWaveAttack.GetComponent<ImpactWaveAttack>());

        //7 전자기파
        //GameObject pulseWaveAttack = new GameObject() { name = "PulseWave" };
        //pulseWaveAttack.transform.parent = transform;
        //pulseWaveAttack.transform.localPosition = Vector3.zero;
        //pulseWaveAttack.AddComponent<PulseWaveAttackSkillStat>();
        //pulseWaveAttack.AddComponent<PulseWaveAttack>().Init(this, null, null);
        //attackSkills.Add(pulseWaveAttack.GetComponent<PulseWaveAttack>());

        //8 네이팜탄
        //GameObject napalmAttack = new GameObject() { name = "Napalm" };
        //napalmAttack.transform.parent = transform;
        //napalmAttack.transform.localPosition = Vector3.zero;
        //napalmAttack.AddComponent<NapalmAttackSkillStat>();
        //napalmAttack.AddComponent<NapalmAttack>().Init(this, null, null);
        //attackSkills.Add(napalmAttack.GetComponent<NapalmAttack>());

        //9 방어막
        //GameObject shield = new GameObject() { name = "Shield" };
        //shield.transform.parent = transform;
        //shield.transform.localPosition = Vector3.zero;
        //shield.AddComponent<ShieldSkillStat>();
        //shield.AddComponent<Shield>().Init(this, null, null);
        //attackSkills.Add(shield.GetComponent<Shield>());

        //10 드론
        //GameObject drone = new GameObject() { name = "Drone" };
        //drone.transform.parent = transform;
        //drone.transform.localPosition = Vector3.zero;
        //drone.AddComponent<DroneSkillStat>();
        //drone.AddComponent<DroneSkill>().Init(this, null, null);
        //attackSkills.Add(drone.GetComponent<DroneSkill>());

        //11 핵폭발
        //GameObject nuclear = new GameObject() { name = "Nuclear" };
        //nuclear.transform.parent = transform;
        //nuclear.transform.localPosition = Vector3.zero;
        //nuclear.AddComponent<NuclearSkillStat>();
        //nuclear.AddComponent<NuclearBombAttack>().Init(this, null, null);
        ////attackSkills.Add(nuclear.GetComponent<NuclearBombAttack>());
        ////_activeSkillDispatcher.Add(nuclear.GetComponent<NuclearSkillStat>().CoolTime, nuclear.GetComponent<NuclearBombAttack>());
        //_activeSkillDispatcher.Add(0, nuclear.GetComponent<NuclearBombAttack>());

        //12 시공의폭풍
        //GameObject blackHole = new GameObject() { name = "BlackHole" };
        //blackHole.transform.parent = transform;
        //blackHole.transform.localPosition = Vector3.zero;
        //blackHole.AddComponent<BlackHoleAttackSkillStat>();
        //blackHole.AddComponent<BlackHoleAttack>().Init(this, null, null);
        //attackSkills.Add(blackHole.GetComponent<BlackHoleAttack>());


        foreach (var item in attackSkills)
        {
            _activeSkillDispatcher.Add(0f, item);
        }

    }

    private void Update()
    {
        //Time.timeScale = Mathf.Lerp(Time.timeScale, Input.GetKey(KeyCode.LeftShift) ? 0.02f : 1, Time.unscaledDeltaTime * 30);
        

        switch (State)
        {
            case CreatureState.Idle:
                LookAtClosestEnemy();
                InputMove();
                break;
            case CreatureState.Move:
                InputMove();
                Move();
                LookAtClosestEnemy();
                break;
            case CreatureState.Attack:
                LookAtClosestEnemy();
                InputMove();
                Move();
                break;
            default:
                break;
        }

        _activeSkillDispatcher.Update(Time.deltaTime);
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    GameObject.FindObjectOfType<DroneSkill>().UseSkill();
        //}
    }

    private void LateUpdate()
    {
       
        // 카메라가 플레이어를 따라가게
        Vector3 pos = transform.position;
        //pos.z = camTransform.position;
        pos += CameraOffset;
        camTransform.position = pos;


        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one, Quaternion.identity, tileMask);
        if (colliders.Length == 0)
            return;

        field.UpdateSector(colliders[0].transform.gameObject);

    }
    #endregion



    #region Helper Method
    private void InputMove()
    {
        moveDir = Vector3.zero;
        State = CreatureState.Idle;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.mousePosition;
            moveDir = (touchPos - touchStartPos);
            State = CreatureState.Move;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.touches[0];
        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                break;
            case TouchPhase.Moved:
                moveDir = (touch.position - touchStartPos);
                State = CreatureState.Move;
                break;
            default:
                break;
        }
#endif
    }

    private void Move()
    {
        transform.position += moveDir.normalized * _stat.MoveSpeed * Time.deltaTime;
        if(moveDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDir.normalized,Vector3.back);
    }

    public override void OnKilled(BaseController controller)
    {
        MonsterController mc = controller as MonsterController;
        if (mc != null)
        {
            Stat.Exp += mc.GetComponent<MonsterStat>().Exp;
        }
    }

    public void LookAtClosestEnemy()
    {
        GameObject closestTarget = SearchTarget();
        if (!closestTarget) return;
        Vector3 targetPos = closestTarget.transform.position;
        targetPos.z = 0f;
        Vector3 myPos = transform.position;
        myPos.z = 0f;
        body.transform.rotation = Quaternion.LookRotation((targetPos - myPos).normalized, Vector3.back);
    }

    private GameObject SearchTarget()
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        GameObject closetTarget = null;
        float closetDist = float.MaxValue;

        foreach (MonsterController target in monsters)
        {
            float dist = (target.transform.position - transform.position).sqrMagnitude;
            if (dist < closetDist)
            {
                closetTarget = target.gameObject;
                closetDist = dist;
            }
        }

        return closetTarget;
    }
    #endregion
}
