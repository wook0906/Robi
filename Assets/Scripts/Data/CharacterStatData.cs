using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterStatData")]
public class CharacterStatData : ScriptableObject
{
    public Define.CharacterType characterType;
    public int _maxHp;
    public float _atk;
    public int _def;
    public float _moveSpeed;
    public float _itemRootingRangeRadius;
    public float _expAcquirePercentage;
}
