using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillBenefitDictionary : SerializableDictionary<Define.SkillBenefitType, float> { }

[CreateAssetMenu(menuName = "SkillBenefitDescription")]
public class SkillBenefitDescription : ScriptableObject
{
    public List<SkillBenefitDictionary> normal;
    public List<SkillBenefitDictionary> rare;
    public List<SkillBenefitDictionary> unique;

}
