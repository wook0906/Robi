using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected float _maxHp;
    [SerializeField]
    protected float _hpRecoveryPerSecond;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected float _moveSpeed;
    

    public int Level { get { return _level; } set { _level = value; } }
    public virtual float Hp { get { return _hp; } set { _hp = value; } }
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float HpRecoveryPerSecond { get { return _hpRecoveryPerSecond; } set { _hpRecoveryPerSecond = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
 

    private void Start()
    {
        _level = 1;
    }

    public virtual void OnAttacked(BaseController attacker, float damage)
    {
        this.Hp -= damage;

        if (this.Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(BaseController attacker)
    {
        //Managers.Game.Despawn(this.gameObject);
    }
}
