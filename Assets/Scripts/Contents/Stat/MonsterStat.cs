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
    protected bool _useImmortal;
    bool _isOnImmortal;
    protected bool _useShield;

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
        _useBerserk = stat._useBerserk;
        _useImmortal = stat._useImmortal;
        _useShield = stat._useShield;
        _useSuicideBombing = stat._useSuicideBombing;

        _normalAttack = stat.normalAttack;
        _launchBomb = stat.launchBomb;
        _missile = stat.missile;
        _immortalDuration = stat._immortalDuration;
        _immortalCoolTime = stat._immortalCoolTime;



        if (_useImmortal)
        {
            GameObject go = new GameObject() { name = "MonsterImmortal" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterImmortalShield immortal = go.AddComponent<MonsterImmortalShield>();
            immortal.Init(owner, null, null);
        }
        if (_useShield)
        {
            GameObject go = new GameObject() { name = "MonsterShield" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterShield shield = go.AddComponent<MonsterShield>();
            shield.Init(owner, null, null);
        }

        if (stat._useSkills.Count == 0) return;

        foreach (var item in stat._useSkills)
        {
            AttackSkillBase skill = Instantiate(item, transform.position, Quaternion.identity, transform);
            skill.Init(owner, null, null);
            skill.name = item.name;
            owner.AttackSkillDispatcher.Add(0, item);
        }
        
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
