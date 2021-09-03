using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterStatData")]
public class MonsterStatData : ScriptableObject
{
    public Define.MonsterType monsterType;
    public int _maxHp;
    public int _atk;
    public int _def;
    public float _moveSpeed;
    public float _attackRange;
    [Tooltip("0 이면 무한대")]
    public float _detectRange;
    public int _exp;

    public bool _useSkill;
    public bool _normalAttack;
    public bool _missile;
    public bool _laser;
    public bool _launchBomb;
    public bool _bombing;
    public bool _shield;
    public bool _immortal;
    public int _immortalDuration = 0;
    public int _immortalCoolTime = 0;
    public bool _berserk;
    public bool _suicideBombing;

    //public List<AttackSkillBase> _useSkills;

    public bool isNeedNumOfProjectileSetupPerSkills = false;
    public int normalAttack = 1;
    public int launchBomb = 1;
    public int missile = 1;
}



