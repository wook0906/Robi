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
    public bool _useShield;
    public bool _useImmortal;
    public int _immortalDuration = 0;
    public int _immortalCoolTime = 0;
    public bool _useBerserk;
    public bool _useSuicideBombing;

    public List<AttackSkillBase> _useSkills;

    public bool isNeedNumOfProjectileSetupPerSkills = false;
    public int normalAttack = 1;
    public int launchBomb = 1;
    public int missile = 1;
}
[CustomEditor(typeof(MonsterStatData))]
public class MonsterStatDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        MonsterStatData statData = target as MonsterStatData;
        statData.monsterType = (Define.MonsterType)EditorGUILayout.EnumPopup("Monster Type", statData.monsterType);
        statData._maxHp = EditorGUILayout.IntField("Max HP", statData._maxHp);
        statData._atk = EditorGUILayout.IntField("ATK", statData._atk);
        statData._def = EditorGUILayout.IntField("DEF", statData._def);
        statData._moveSpeed = EditorGUILayout.FloatField("Move Speed", statData._moveSpeed);
        statData._attackRange = EditorGUILayout.FloatField("Attack Range", statData._attackRange);
        statData._detectRange = EditorGUILayout.FloatField("Detect Range", statData._detectRange);
        statData._exp = EditorGUILayout.IntField("Exp", statData._exp);
        statData._useShield = EditorGUILayout.Toggle("Use Shield", statData._useShield);
        statData._useImmortal = EditorGUILayout.Toggle("Use Immortal", statData._useImmortal);
        if (statData._useImmortal)
        {
            statData._immortalDuration = EditorGUILayout.IntField("Immortal Duration", statData._immortalDuration);
            statData._immortalCoolTime = EditorGUILayout.IntField("Immortal CoolTime", statData._immortalCoolTime);
        }
        statData._useBerserk = EditorGUILayout.Toggle("Use Berserk", statData._useBerserk);
        statData._useSuicideBombing = EditorGUILayout.Toggle("Use SuicideBombing", statData._useSuicideBombing);

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_useSkills"),true);
        serializedObject.ApplyModifiedProperties();

        statData.isNeedNumOfProjectileSetupPerSkills = EditorGUILayout.Toggle("isNeedNumOfProjectileSetupPerSkills",statData.isNeedNumOfProjectileSetupPerSkills);
        if (statData.isNeedNumOfProjectileSetupPerSkills)
        {
            statData.normalAttack = EditorGUILayout.IntField("Normal attack", statData.normalAttack);
            statData.launchBomb = EditorGUILayout.IntField("Launch bomb", statData.launchBomb);
            statData.missile = EditorGUILayout.IntField("Missile", statData.missile);
        }
        GUILayout.EndVertical();
    }
}


