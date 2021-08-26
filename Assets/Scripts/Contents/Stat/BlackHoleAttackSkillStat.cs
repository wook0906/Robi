using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAttackSkillStat : SkillStat
{
    private float _maxAttackRange;
    public float MaxAttackRange { get { return _maxAttackRange; } set { _maxAttackRange = value; } }
}
