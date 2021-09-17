using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        //statData._detectRange = EditorGUILayout.FloatField("Detect Range", statData._detectRange);
        //statData._exp = EditorGUILayout.IntField("Exp", statData._exp);
        statData._useSkill = EditorGUILayout.Toggle("Use Skill", statData._useSkill);

        if (statData._useSkill)
        {
            statData._shield = EditorGUILayout.Toggle("Use Shield", statData._shield);
            statData._immortal = EditorGUILayout.Toggle("Use Immortal", statData._immortal);
            if (statData._shield)
                statData._immortal = false;
            if (statData._immortal)
            {
                statData._immortalDuration = EditorGUILayout.IntField("Immortal Duration", statData._immortalDuration);
                statData._immortalCoolTime = EditorGUILayout.IntField("Immortal CoolTime", statData._immortalCoolTime);
                statData._shield = false;
            }
            statData._berserk = EditorGUILayout.Toggle("Use Berserk", statData._berserk);
            statData._suicideBombing = EditorGUILayout.Toggle("Use SuicideBombing", statData._suicideBombing);
            statData._normalAttack = EditorGUILayout.Toggle("Use NormalAttack", statData._normalAttack);
            statData._missile = EditorGUILayout.Toggle("Use Missile", statData._missile);
            statData._laser = EditorGUILayout.Toggle("Use Laser", statData._laser);
            statData._launchBomb = EditorGUILayout.Toggle("Use LaunchBomb", statData._launchBomb);
            statData._bombing = EditorGUILayout.Toggle("Use Bombing", statData._bombing);
            statData.isNeedNumOfProjectileSetupPerSkills = EditorGUILayout.Toggle("isNeedNumOfProjectileSetupPerSkills", statData.isNeedNumOfProjectileSetupPerSkills);
        }


        if (statData.isNeedNumOfProjectileSetupPerSkills)
        {
            statData.normalAttack = EditorGUILayout.IntField("Normal attack", statData.normalAttack);
            statData.launchBomb = EditorGUILayout.IntField("Launch bomb", statData.launchBomb);
            statData.missile = EditorGUILayout.IntField("Missile", statData.missile);
        }
        GUILayout.EndVertical();
    }
}