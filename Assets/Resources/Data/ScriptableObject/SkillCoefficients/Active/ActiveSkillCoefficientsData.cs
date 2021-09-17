using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillCoefficientsData/Active")]
public class ActiveSkillCoefficientsData : ScriptableObject
{
    public CoefficientsType common;
    public CoefficientsType rare;
    public CoefficientsType unique;
    public List<int> ProjectileAddLevel;
}
[Serializable]
public class CoefficientsType
{
    public float damage;
    public float explostionDamage;
    public float continuousDamage;
    public float coolTime;
    public float explosionRange;
    public float attackRange;
    public float duration;
    public float hpRecovery;
    public float AdditionalProjectilePerBurst;
}
