using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : CreatureStat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected float _detectRange;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected float _attackInterval;
    
    
    [SerializeField]
    protected bool _useBerserk = false;
    bool isOnBerserk = false;
    [SerializeField]
    protected bool _useSuicideBombing = false;
    public bool _useImmortal;
    bool _isOnImmortal;
    public bool _useShield;

    public int Exp { get { return _exp; } set { _exp = value; } }
    public float DetectRange { get { return _detectRange; } set { _detectRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }
    public bool IsOnImmortal { get { return _isOnImmortal; } set { _isOnImmortal = value; } }
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
        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.CreatureStat> dict = Managers.Data.MonsterStatDict;
        Data.CreatureStat stat = dict[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _damage = stat.damage;
        _hpRecoveryPerSecond = stat.hpRecoveryPerSecond;
        _moveSpeed = stat.speed;
        _exp = stat.totalExp;
        _attackRange = stat.attackRange;
        _attackInterval = stat.attackInterval;
        _detectRange = stat.detectRange;
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
