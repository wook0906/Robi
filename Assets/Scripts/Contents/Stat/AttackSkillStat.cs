using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSkillStat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _coolTime;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected float _attackRange;
    //[SerializeField]
    //protected int _useCount;
    [SerializeField]
    protected int _numOfProjectilePerBurst;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected bool _isExplode;
    [SerializeField]
    protected float _explosionRange;
    [SerializeField]
    protected int _explosionDamage;
    [SerializeField]
    protected bool _isPenetrate;
    [SerializeField]
    protected float _duration;
    [SerializeField]
    protected float _delayPerAttack;

    public int Level { get { return _level; } set { _level = value; } }
    public float CoolTime { get { return _coolTime; } set { _coolTime = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    //public int UseCount { get { return _useCount; } set { _useCount = value; } }
    public int NumOfProjectilePerBurst { get { return _numOfProjectilePerBurst; } set { _numOfProjectilePerBurst = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public bool IsExplode { get { return _isExplode; } set { _isExplode = value; } }
    public float ExplosionRange { get { return _explosionRange; } set { _explosionRange = value; } }
    public int ExplosionDamage { get { return _explosionDamage; } set { _explosionDamage = value; } }
    public bool IsPenetrate { get { return _isPenetrate; } set { _isPenetrate = value; } }
    public float Duration { get { return _duration; } set { _duration = value; } }
    public float DelayPerAttack { get { return _delayPerAttack; } set { _delayPerAttack = value; } }

    //private SkillStatData _skill;
    //private void Start()
    //{
    //    _level = 1;
    //    SetStat(_skill.SkillType);
    //}

    //public void SetStat(Define.AttackSkillType skillType)
    //{
    //    Dictionary<Define.AttackSkillType, SkillStatData> dict = Managers.Data.skillStatDict;
    //    //Data.AttackSkillStat stat = dict[skillType];
    //    SkillStatData stat = dict[skillType];

    //    _coolTime = stat._coolTime;
    //    _damage = stat._damage;
    //    _attackRange = stat._attackRange;
    //    //_useCount = stat.useCount;
    //    _numOfProjectilePerBurst = stat._numOfProjectilePerBurst;
    //    _speed = stat._speed;
    //    _isExplode = stat._isExplode;
    //    _explosionDamage = stat._explosionDamage;
    //    _explosionRange = stat._explosionRange;
    //    _isPenetrate = stat._isPenetrate;
    //    _duration = stat._duration;
    //    _delayPerAttack = stat._delayPerAttack;
    //}
}
