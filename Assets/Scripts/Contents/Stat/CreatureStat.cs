using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStat : MonoBehaviour
{
    
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected float _maxHp;
    [SerializeField]
    protected float _moveSpeed;
    

    public virtual float Hp { get { return _hp; } set { _hp = value; } }
    public virtual float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public virtual float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
 

    protected virtual void Update()
    {

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
