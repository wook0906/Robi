using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillStatData")]
public class SkillStatData : ScriptableObject
{
    public Define.AttackSkillType SkillType;
    public float _coolTime;
    [Tooltip("0이면 owner의 능력치에 따름")]
    public int _damage;
    [Tooltip("0이면 무한대")]
    public float _attackRange;
    [Tooltip("한 공격 싸이클에서 발사될 투사체의 갯수")]
    public int _numOfProjectilePerBurst;
    [Tooltip("투사체의 속도 0이면 즉시 목표에 도착")]
    public float _speed;
    public bool _isExplode;
    [Tooltip("폭발 범위")]
    public float _explosionRange;
    [Tooltip("폭발 범위안에 들어왔을때의 데미지")]
    public int _explosionDamage;
    public bool _isPenetrate;
    [Tooltip("지속시간 0이면 무한대")]
    public float _duration;
    [Tooltip("한 공격 싸이클에서 발사될 투사체가 여러개일 때, 발사 사이의 간격")]
    public float _delayPerAttack;
}
