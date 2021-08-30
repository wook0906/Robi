using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp_Popup : PopupUI
{
    PlayerController player;
    List<SkillInfo> skillCandidateList;

    struct SkillInfo
    {
        public Define.SkillGrade grade;
        public Define.AttackSkillType skillType;
    }

    SkillInfo skill1;
    SkillInfo skill2;
    SkillInfo skill3;

    enum Buttons
    {
        Item1_Button,
        Item2_Button,
        Item3_Button,
    }
    enum Images
    {
        Item1Icon_Image,
        Item2Icon_Image,
        Item3Icon_Image,
    }
    enum Texts
    {
        Item1Name_Text,
        Item2Name_Text,
        Item3Name_Text,
        Item1Level_Text,
        Item2Level_Text,
        Item3Level_Text,
        Item1Description_Text,
        Item2Description_Text,
        Item3Description_Text,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        skillCandidateList = new List<SkillInfo>();
        for (Define.AttackSkillType i = Define.AttackSkillType.PlayerNormal; i < Define.AttackSkillType.PlayerMax; i++)
        {
            SkillInfo skillInfo;
            skillInfo.grade = (Define.SkillGrade)Random.Range((int)Define.SkillGrade.Common, (int)Define.SkillGrade.Max);

            skillInfo.skillType = i;
            skillCandidateList.Add(skillInfo);
        }
        
        int randomIdx = Random.Range(0, skillCandidateList.Count);
        skill1 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item1Name_Text).text = $"{skill1.grade} : {skill1.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        

        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill2 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item2Name_Text).text = $"{skill2.grade} : {skill2.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        
        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill3 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item3Name_Text).text = $"{skill3.grade} : {skill3.skillType}";
        skillCandidateList.RemoveAt(randomIdx);

        Get<Button>((int)Buttons.Item1_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(()=>
        {
            OnClickSkillButton(skill1);
        }));
        
        Get<Button>((int)Buttons.Item2_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            OnClickSkillButton(skill2);
        }));
        
        Get<Button>((int)Buttons.Item3_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            OnClickSkillButton(skill3);
        }));

        GameScene gameScene = Managers.Scene.CurrentScene as GameScene;
        player = gameScene.player.GetComponent<PlayerController>();

        Time.timeScale = 0f;
    }
    void OnClickSkillButton(SkillInfo skillInfo)
    {
        foreach (var item in player.attackSkills)
        {
            if (item.Stat.SkillType == skillInfo.skillType)
            {
                item.LevelUp(skillInfo.grade);
                ClosePopupUI();
                Time.timeScale = 1f;
                return;
            }
        }

        AddSkill(skillInfo);
        ClosePopupUI();
        Time.timeScale = 1f;
    }
    void AddSkill(SkillInfo skillInfo)
    {
        GameObject go;
        switch (skillInfo.skillType)
        {
            case Define.AttackSkillType.PlayerNormal:
                go = new GameObject() { name = "NormalAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                NormalAttack normalSkill = go.AddComponent<NormalAttack>();
                normalSkill.Init(player, null, null);
                player.attackSkills.Add(normalSkill);
                player.AttackSkillDispatcher.Add(1f, normalSkill);
                break;
            case Define.AttackSkillType.FlameThrower:
                go = new GameObject() { name = "FlameThrower" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                FlameThrowerAttack flameThrower = go.AddComponent<FlameThrowerAttack>();
                flameThrower.Init(player, null, null);
                player.attackSkills.Add(flameThrower);
                player.AttackSkillDispatcher.Add(1f, flameThrower);
                break;
            case Define.AttackSkillType.ImpactWave:
                go = new GameObject() { name = "ImpactWave" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                ImpactWaveAttack impactWave = go.AddComponent<ImpactWaveAttack>();
                impactWave.Init(player, null, null);
                player.attackSkills.Add(impactWave);
                player.AttackSkillDispatcher.Add(1f, impactWave);
                break;
            case Define.AttackSkillType.PlayerLaser:
                go = new GameObject() { name = "LaserAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                LaserAttack laser = go.AddComponent<LaserAttack>();
                laser.Init(player, null, null);
                player.attackSkills.Add(laser);
                player.AttackSkillDispatcher.Add(1f, laser);
                break;
            case Define.AttackSkillType.PlayerMissile:
                go = new GameObject() { name = "MissileAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                MissileAttack missile = go.AddComponent<MissileAttack>();
                missile.Init(player, null, null);
                player.attackSkills.Add(missile);
                player.AttackSkillDispatcher.Add(1f, missile);
                break;
            case Define.AttackSkillType.Napalm:
                go = new GameObject() { name = "Napalm" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                NapalmAttack Napalm = go.AddComponent<NapalmAttack>();
                Napalm.Init(player, null, null);
                player.attackSkills.Add(Napalm);
                player.AttackSkillDispatcher.Add(1f, Napalm);
                break;
            case Define.AttackSkillType.PulseWave:
                go = new GameObject() { name = "PulseWave" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                PulseWaveAttack PulseWave = go.AddComponent<PulseWaveAttack>();
                PulseWave.Init(player, null, null);
                player.attackSkills.Add(PulseWave);
                player.AttackSkillDispatcher.Add(1f, PulseWave);
                break;
            case Define.AttackSkillType.NuclearBomb:
                go = new GameObject() { name = "NuclearBomb" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                NuclearBombAttack NuclearBomb = go.AddComponent<NuclearBombAttack>();
                NuclearBomb.Init(player, null, null);
                player.attackSkills.Add(NuclearBomb);
                player.AttackSkillDispatcher.Add(1f, NuclearBomb);
                break;
            case Define.AttackSkillType.PlayerBombing:
                go = new GameObject() { name = "Bombing" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                BombingAttack Bombing = go.AddComponent<BombingAttack>();
                Bombing.Init(player, null, null);
                player.attackSkills.Add(Bombing);
                player.AttackSkillDispatcher.Add(1f, Bombing);
                break;
            case Define.AttackSkillType.PlayerShield:
                go = new GameObject() { name = "Shield" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                ShieldSkill Shield = go.AddComponent<ShieldSkill>();
                Shield.Init(player, null, null);
                player.attackSkills.Add(Shield);
                player.AttackSkillDispatcher.Add(1f, Shield);
                break;
            case Define.AttackSkillType.Drone:
                go = new GameObject() { name = "DroneSkill" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                DroneSkill droneSkill = go.AddComponent<DroneSkill>();
                droneSkill.Init(player, null, null);
                player.attackSkills.Add(droneSkill);
                player.AttackSkillDispatcher.Add(1f, droneSkill);
                break;
            case Define.AttackSkillType.BlackHole:
                go = new GameObject() { name = "BlackHole" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                BlackHoleAttack blackHole = go.AddComponent<BlackHoleAttack>();
                blackHole.Init(player, null, null);
                player.attackSkills.Add(blackHole);
                player.AttackSkillDispatcher.Add(1f, blackHole);
                break;
            default:
                Debug.LogError("AddSkill Error");
                break;
        }

    }
}
