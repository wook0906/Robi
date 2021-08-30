using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillStat : MonoBehaviour
{
    public SkillStat() {}
    public SkillStat(Define.AttackSkillType skillType, float coolTime, int damage, float attackRange, int numOfProjectilePerBurst, float speed, bool isExplode, float explosionRange, int explosionDamage, bool isPenetrate, float duration, float delayPerAttack)
    {
        this.skillType = skillType;
        _level = 1;
        _coolTime = coolTime;
        _damage = damage;
        _attackRange = attackRange;
        _numOfProjectilePerBurst = numOfProjectilePerBurst;
        _speed = speed;
        _isExplode = isExplode;
        _explosionRange = explosionRange;
        _explosionDamage = explosionDamage;
        _isPenetrate = isPenetrate;
        _duration = duration;
        _delayPerAttack = delayPerAttack;
    }
    [SerializeField]
    Define.AttackSkillType skillType;
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _coolTime;
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected float _attackRange;
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
    [SerializeField]
    protected int numOfCommon = 0;
    [SerializeField]
    protected int numOfRare = 0;
    [SerializeField]
    protected int numOfUnique = 0;

    [HideInInspector]
    public GameObject _bulletPrefab;
    

    public Define.AttackSkillType SkillType { get { return skillType; } set { skillType = value; } }
    public int Level { get { return _level; } set { _level = value; } }
    public float CoolTime { get { return _coolTime; } set { _coolTime = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public int NumOfProjectilePerBurst { get { return _numOfProjectilePerBurst; } set { _numOfProjectilePerBurst = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public bool IsExplode { get { return _isExplode; } set { _isExplode = value; } }
    public float ExplosionRange { get { return _explosionRange; } set { _explosionRange = value; } }
    public int ExplosionDamage { get { return _explosionDamage; } set { _explosionDamage = value; } }
    public bool IsPenetrate { get { return _isPenetrate; } set { _isPenetrate = value; } }
    public float Duration { get { return _duration; } set { _duration = value; } }
    public float DelayPerAttack { get { return _delayPerAttack; } set { _delayPerAttack = value; } }
    public int NumOfCommon 
    { 
        get 
        {
            if (numOfCommon <= 0)
                return 0;
            return numOfCommon;
        }
        set { numOfCommon = value; } 
    }
    public int NumOfRare 
    { 
        get 
        {
            if (numOfRare <= 0)
                return 0;
            return numOfRare;
        } 
        set { numOfRare = value; }
    }
    public int NumOfUnique 
    { 
        get 
        {
            if (numOfUnique <= 0)
                return 0;
            return numOfUnique; 
        } 
        set { numOfUnique = value; }
    }

    public virtual void InitSkillStat(Define.AttackSkillType skillType)
    {
        Dictionary<Define.AttackSkillType, SkillStatData> dict = Managers.Data.skillStatDict;

        SkillStatData stat = dict[skillType];

        this.skillType = skillType;
        _level = 1;
        _coolTime = stat._coolTime;
        _damage = stat._damage;
        _attackRange = stat._attackRange;
        //_useCount = stat.useCount;
        _numOfProjectilePerBurst = stat._numOfProjectilePerBurst;
        _speed = stat._speed;
        _isExplode = stat._isExplode;
        _explosionDamage = stat._explosionDamage;
        _explosionRange = stat._explosionRange;
        _isPenetrate = stat._isPenetrate;
        _duration = stat._duration;
        _delayPerAttack = stat._delayPerAttack;
        _bulletPrefab = stat._bulletPrefab;
    }

    public virtual void LevelUp(Define.SkillGrade grade)
    {
        switch (grade)
        {
            case Define.SkillGrade.Common:
                NumOfCommon += 1;
                break;
            case Define.SkillGrade.Rare:
                NumOfRare += 1;
                break;
            case Define.SkillGrade.Unique:
                NumOfUnique += 1;
                break;
            default:
                break;
        }
        Level++;
    }
}
