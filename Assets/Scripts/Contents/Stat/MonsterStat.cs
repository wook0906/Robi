using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : CreatureStat
{
    protected BaseController owner;

    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected float _defense;
    [SerializeField]
    protected float _detectRange;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected float _attackInterval;
    [SerializeField]
    protected int _exp;
    
    public int _normalAttack = 1;
    public int _launchBomb = 1;
    public int _missile = 1;
    public int _immortalDuration;
    public int _immortalCoolTime;

    protected bool _useBerserk = false;
    bool isOnBerserk = false;
    protected bool _useSuicideBombing = false;
    bool _isOnImmortal;

    public int Damage { get { return _damage; } set { _damage = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public float DetectRange { get { return _detectRange; } set { _detectRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }
    public bool IsOnImmortal { get { return _isOnImmortal; } set { _isOnImmortal = value; } }
    public int Exp { get { return _exp; } set { _exp = value; } }
    public float ShieldHp 
    { 
        get 
        {
            MonsterShield shieldSkill = GetComponentInChildren<MonsterShield>();
            if (shieldSkill == null) return 0;
            else return GetComponentInChildren<MonsterShield>().ShieldHp;
        }
        set 
        {
            MonsterShield shieldSkill = GetComponentInChildren<MonsterShield>();
            if (shieldSkill == null) return;
            GetComponentInChildren<MonsterShield>().ShieldHp = value;
        }
    }

    private void Start()
    {
        _level = 1;
        SetStat();
    }

    public void SetStat()
    {
        //Dictionary<int, Data.CreatureStat> dict = Managers.Data.MonsterStatDict;
        Dictionary<Define.MonsterType, MonsterStatData> dict = Managers.Data.monsterStatDict;
        MonsterStatData stat = dict[(Define.MonsterType)System.Enum.Parse(typeof(Define.MonsterType),name)];
        owner = GetComponent<BaseController>();

        _hp = stat._maxHp;
        _maxHp = stat._maxHp;
        _damage = stat._atk;
        _defense = stat._def;
        _moveSpeed = stat._moveSpeed;
        _attackRange = stat._attackRange;
        _detectRange = stat._detectRange;
        _exp = stat._exp;
        _useBerserk = stat._berserk;
        _useSuicideBombing = stat._suicideBombing;

        _normalAttack = stat.normalAttack;
        _launchBomb = stat.launchBomb;
        _missile = stat.missile;
        _immortalDuration = stat._immortalDuration;
        _immortalCoolTime = stat._immortalCoolTime;


        #region skill Entry
        if (stat._immortal)
        {
            GameObject go = new GameObject() { name = "MonsterImmortal" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterImmortalShield immortal = go.AddComponent<MonsterImmortalShield>();
            immortal.Init(owner, null, null);
        }
        if (stat._shield)
        {
            GameObject go = new GameObject() { name = "MonsterShield" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterShield shield = go.AddComponent<MonsterShield>();
            shield.Init(owner, null, null);
        }

        if (stat._normalAttack)
        {
            GameObject go = new GameObject() { name = "MonsterNormalAttack" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterNormalAttack monsterNormalAttack = go.AddComponent<MonsterNormalAttack>();
            monsterNormalAttack.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterNormalAttack);
        }
        if (stat._missile)
        {
            GameObject go = new GameObject() { name = "MonsterMissile" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterMissile monsterMissile = go.AddComponent<MonsterMissile>();
            monsterMissile.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterMissile);
        }
        if (stat._laser)
        {
            GameObject go = new GameObject() { name = "MonsterLaser" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterLaserAttack monsterLaser = go.AddComponent<MonsterLaserAttack>();
            monsterLaser.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterLaser);
        }
        if (stat._launchBomb)
        {
            GameObject go = new GameObject() { name = "MonsterLaunchBomb" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterLaunchBomb monsterLaunchBomb = go.AddComponent<MonsterLaunchBomb>();
            monsterLaunchBomb.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterLaunchBomb);
        }
        if (stat._bombing)
        {
            GameObject go = new GameObject() { name = "MonsterBombing" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterBombing monsterBombing = go.AddComponent<MonsterBombing>();
            monsterBombing.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterBombing);
        }
        #endregion

    }

    public override void OnAttacked(BaseController attacker, float damage)
    {
        if (IsOnImmortal) return;

        float resultDamage = ShieldHp - damage;
        if (resultDamage >= 0f)
        {
            ShieldHp = resultDamage;
            return;
        }
        this.Hp += resultDamage;
        if (_useBerserk && !isOnBerserk)
        {
            isOnBerserk = true;
            _damage *= 2;
            _moveSpeed *= 2;
        }
        if (this.Hp <= 0)
        {
            Hp = 0;
            if (_useSuicideBombing)
            {
                if(Vector3.Distance(this.transform.position, attacker.transform.position)<= 10f)
                {
                    attacker.Stat.OnAttacked(GetComponent<BaseController>(), 100);
                }
            }
            OnDead(attacker);
        }
    }

    protected override void OnDead(BaseController attacker)
    {
        PlayerStat stat = attacker.GetComponent<PlayerStat>();
        if(stat != null)
        {
            stat.Exp += _exp;
        }
        Managers.Object.RemoveMonster(GetComponent<MonsterController>());
        Destroy(gameObject);
    }
}
