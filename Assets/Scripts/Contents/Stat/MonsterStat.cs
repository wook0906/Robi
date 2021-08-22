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

    public int Exp { get { return _exp; } set { _exp = value; } }
    public float DetectRange { get { return _detectRange; } set { _detectRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }


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
        this.Hp -= damage;

        if (this.Hp <= 0)
        {
            Hp = 0;
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
