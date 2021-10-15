using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp_Popup : PopupUI
{
    PlayerController player;
    List<SkillInfo> skillCandidateList;
    GameScene_UI gameSceneUI;

    struct SkillInfo
    {
        public bool isActiveSkill;
        public Define.SkillGrade grade;
        public Define.SkillType skillType;
        public int level;
        public SkillBenefitDescription benefitData;
    }

    SkillInfo skill1;
    SkillInfo skill2;
    SkillInfo skill3;

    enum Buttons
    {
        Item1_Button,
        Item2_Button,
        Item3_Button,
        AD_Button
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

    }
    enum Grids
    {
        Item1Description_BG,
        Item2Description_BG,
        Item3Description_BG,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<GridLayoutGroup>(typeof(Grids));

        GameScene gameScene = Managers.Scene.CurrentScene as GameScene;
        player = gameScene.player.GetComponent<PlayerController>();

        skillCandidateList = new List<SkillInfo>();
        for (Define.SkillType skillType = Define.SkillType.PlayerNormal; skillType < Define.SkillType.PlayerPassiveSkillMax; skillType++)
        {
            if (skillType == Define.SkillType.PlayerActiveSkillMax) continue;

            SkillInfo skillInfo = new SkillInfo();
            skillInfo.grade = (Define.SkillGrade)Random.Range((int)Define.SkillGrade.Common, (int)Define.SkillGrade.Max);
            if (skillType < Define.SkillType.PlayerActiveSkillMax)
            {
                skillInfo.isActiveSkill = true;
                skillInfo.level = GetLevelActiveSkill(skillType);
            }
            else
            {
                skillInfo.level = GetLevelPassiveSkill(skillType);
                skillInfo.isActiveSkill = false;
            }
            skillInfo.skillType = skillType;

            if (skillInfo.isActiveSkill)
            {
                if (skillInfo.level < 7)
                {
                    skillCandidateList.Add(skillInfo);
                }
            }
            else
            {
                if (skillInfo.level < 3)
                    skillCandidateList.Add(skillInfo);
            }
        }
        
        int randomIdx = Random.Range(0, skillCandidateList.Count);
        skill1 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item1Name_Text).text = $"{skill1.grade} \n {skill1.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item1Level_Text).text = $"Lv.{skill1.level}";
        skill1.benefitData = Managers.Data.skillBenefitDescriptionDict[skill1.skillType];
       
        int i = 1;
        if (skill1.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill1.benefitData.normal[skill1.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }   
        }
        else if (skill1.grade == Define.SkillGrade.Rare)
        {
            
            foreach (var item in skill1.benefitData.rare[skill1.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill1.benefitData.unique[skill1.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }
        }

        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill2 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item2Name_Text).text = $"{skill2.grade} \n {skill2.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item2Level_Text).text = $"Lv.{skill2.level}";
        skill2.benefitData = Managers.Data.skillBenefitDescriptionDict[skill1.skillType];

        i = 1;
        if (skill2.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill2.benefitData.normal[skill2.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }
        else if (skill2.grade == Define.SkillGrade.Rare)
        {
            foreach (var item in skill2.benefitData.rare[skill2.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill2.benefitData.unique[skill2.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }



        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill3 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item3Name_Text).text = $"{skill3.grade} \n {skill3.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item3Level_Text).text = $"Lv.{skill3.level}";
        skill3.benefitData = Managers.Data.skillBenefitDescriptionDict[skill3.skillType];

        i = 1;
        if (skill3.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill3.benefitData.normal[skill3.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }
        else if (skill3.grade == Define.SkillGrade.Rare)
        {
            foreach (var item in skill3.benefitData.rare[skill3.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill3.benefitData.unique[skill3.level-1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }


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

        Get<Button>((int)Buttons.AD_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            OnClickAdButton();
        }));

        

        Time.timeScale = 0f;
        gameSceneUI = Managers.UI.GetSceneUI<GameScene_UI>();
        gameSceneUI.levelUpPopupCnt++;
    }
    int GetLevelPassiveSkill(Define.SkillType skillType)
    {
        return player.passiveSkill.skillLevelDict[skillType] + 1;
    }
    int GetLevelActiveSkill(Define.SkillType skillType)
    {
        foreach (var item in player.attackSkills)
        {
            if (item.Type == skillType)
            {
                return item.Stat.Level + 1;
            }
        }
        return 1;
    }
    void OnClickSkillButton(SkillInfo skillInfo)
    {
        gameSceneUI.levelUpPopupCnt--;
        if (skillInfo.isActiveSkill)
        {
            foreach (var item in player.attackSkills)
            {
                if (item.Stat.SkillType == skillInfo.skillType)
                {
                    item.LevelUp(skillInfo.grade);
                    ClosePopupUI();
        
                    if(gameSceneUI.levelUpPopupCnt <= 0)
                        Time.timeScale = 1f;
                    return;
                }
            }
            AddNewActiveSkill(skillInfo);
        }
        else 
        {
            player.PlayerStat.PassiveSkillLevelUp(skillInfo.skillType, skillInfo.grade);
        }
        if (gameSceneUI.levelUpPopupCnt <= 0)
            Time.timeScale = 1f;
        ClosePopupUI();
    }
    void OnClickAdButton()
    {
        skillCandidateList = new List<SkillInfo>();
        for (Define.SkillType skillType = Define.SkillType.PlayerNormal; skillType < Define.SkillType.PlayerPassiveSkillMax; skillType++)
        {
            if (skillType == Define.SkillType.PlayerActiveSkillMax) continue;

            SkillInfo skillInfo = new SkillInfo();
            skillInfo.grade = (Define.SkillGrade)Random.Range((int)Define.SkillGrade.Common, (int)Define.SkillGrade.Max);
            if (skillType < Define.SkillType.PlayerActiveSkillMax)
            {
                skillInfo.isActiveSkill = true;
                skillInfo.level = GetLevelActiveSkill(skillType);
            }
            else
            {
                skillInfo.level = GetLevelPassiveSkill(skillType);
                skillInfo.isActiveSkill = false;
            }
            skillInfo.skillType = skillType;

            if (skillInfo.isActiveSkill)
            {
                if (skillInfo.level < 7)
                {
                    skillCandidateList.Add(skillInfo);
                }
            }
            else
            {
                if (skillInfo.level < 3)
                    skillCandidateList.Add(skillInfo);
            }
        }

        foreach (Transform item in Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform)
        {
            Destroy(item.gameObject);
        }

        int randomIdx = Random.Range(0, skillCandidateList.Count);
        skill1 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item1Name_Text).text = $"{skill1.grade} \n {skill1.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item1Level_Text).text = $"Lv.{skill1.level}";
        skill1.benefitData = Managers.Data.skillBenefitDescriptionDict[skill1.skillType];

        int i = 1;
        if (skill1.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill1.benefitData.normal[skill1.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }
        }
        else if (skill1.grade == Define.SkillGrade.Rare)
        {

            foreach (var item in skill1.benefitData.rare[skill1.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill1.benefitData.unique[skill1.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item1Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill1)}";
                i++;
            }
        }

        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill2 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item2Name_Text).text = $"{skill2.grade} \n {skill2.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item2Level_Text).text = $"Lv.{skill2.level}";
        skill2.benefitData = Managers.Data.skillBenefitDescriptionDict[skill1.skillType];

        i = 1;
        if (skill2.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill2.benefitData.normal[skill2.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }
        else if (skill2.grade == Define.SkillGrade.Rare)
        {
            foreach (var item in skill2.benefitData.rare[skill2.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill2.benefitData.unique[skill2.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item2Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill2)}";
                i++;
            }
        }



        randomIdx = Random.Range(0, skillCandidateList.Count);
        skill3 = skillCandidateList[randomIdx];
        Get<Text>((int)Texts.Item3Name_Text).text = $"{skill3.grade} \n {skill3.skillType}";
        skillCandidateList.RemoveAt(randomIdx);
        Get<Text>((int)Texts.Item3Level_Text).text = $"Lv.{skill3.level}";
        skill3.benefitData = Managers.Data.skillBenefitDescriptionDict[skill3.skillType];

        i = 1;
        if (skill3.grade == Define.SkillGrade.Common)
        {
            foreach (var item in skill3.benefitData.normal[skill3.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }
        else if (skill3.grade == Define.SkillGrade.Rare)
        {
            foreach (var item in skill3.benefitData.rare[skill3.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }
        else
        {
            foreach (var item in skill3.benefitData.unique[skill3.level - 1])
            {
                GameObject descriptionText = Managers.Resource.Instantiate("UI/SkillBenefitDescriptionItem");
                descriptionText.transform.SetParent(Get<GridLayoutGroup>((int)Grids.Item3Description_BG).transform);
                descriptionText.GetComponent<Text>().text = $"{i}.{GetSkillBenefitDescription(skill3)}";
                i++;
            }
        }
    }
    void AddNewActiveSkill(SkillInfo skillInfo)
    {
        GameObject go;
        switch (skillInfo.skillType)
        {
            case Define.SkillType.PlayerNormal:
                go = new GameObject() { name = "NormalAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                NormalAttack normalSkill = go.AddComponent<NormalAttack>();
                normalSkill.Init(player, null, null);
                player.attackSkills.Add(normalSkill);
                player.AttackSkillDispatcher.Add(1f, normalSkill);
                break;
            case Define.SkillType.FlameThrower:
                go = new GameObject() { name = "FlameThrower" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                FlameThrowerAttack flameThrower = go.AddComponent<FlameThrowerAttack>();
                flameThrower.Init(player, null, null);
                player.attackSkills.Add(flameThrower);
                player.AttackSkillDispatcher.Add(1f, flameThrower);
                break;
            case Define.SkillType.ImpactWave:
                go = new GameObject() { name = "ImpactWave" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                ImpactWaveAttack impactWave = go.AddComponent<ImpactWaveAttack>();
                impactWave.Init(player, null, null);
                player.attackSkills.Add(impactWave);
                player.AttackSkillDispatcher.Add(1f, impactWave);
                break;
            case Define.SkillType.PlayerLaser:
                go = new GameObject() { name = "LaserAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                LaserAttack laser = go.AddComponent<LaserAttack>();
                laser.Init(player, null, null);
                player.attackSkills.Add(laser);
                player.AttackSkillDispatcher.Add(1f, laser);
                break;
            case Define.SkillType.PlayerMissile:
                go = new GameObject() { name = "MissileAttack" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                MissileAttack missile = go.AddComponent<MissileAttack>();
                missile.Init(player, null, null);
                player.attackSkills.Add(missile);
                player.AttackSkillDispatcher.Add(1f, missile);
                break;
            case Define.SkillType.Napalm:
                go = new GameObject() { name = "Napalm" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                NapalmAttack Napalm = go.AddComponent<NapalmAttack>();
                Napalm.Init(player, null, null);
                player.attackSkills.Add(Napalm);
                player.AttackSkillDispatcher.Add(1f, Napalm);
                break;
            case Define.SkillType.PulseWave:
                go = new GameObject() { name = "PulseWave" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                PulseWaveAttack PulseWave = go.AddComponent<PulseWaveAttack>();
                PulseWave.Init(player, null, null);
                player.attackSkills.Add(PulseWave);
                player.AttackSkillDispatcher.Add(1f, PulseWave);
                break;
            //case Define.SkillType.NuclearBomb:
            //    go = new GameObject() { name = "NuclearBomb" };
            //    go.transform.parent = player.transform;
            //    go.transform.localPosition = Vector3.zero;
            //    NuclearBombAttack NuclearBomb = go.AddComponent<NuclearBombAttack>();
            //    NuclearBomb.Init(player, null, null);
            //    player.attackSkills.Add(NuclearBomb);
            //    player.AttackSkillDispatcher.Add(1f, NuclearBomb);
            //    break;
            case Define.SkillType.PlayerBombing:
                go = new GameObject() { name = "Bombing" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                BombingAttack Bombing = go.AddComponent<BombingAttack>();
                Bombing.Init(player, null, null);
                player.attackSkills.Add(Bombing);
                player.AttackSkillDispatcher.Add(1f, Bombing);
                break;
            case Define.SkillType.PlayerShield:
                go = new GameObject() { name = "Shield" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                ShieldSkill Shield = go.AddComponent<ShieldSkill>();
                Shield.Init(player, null, null);
                player.attackSkills.Add(Shield);
                player.AttackSkillDispatcher.Add(1f, Shield);
                break;
            case Define.SkillType.Drone:
                go = new GameObject() { name = "DroneSkill" };
                go.transform.parent = player.transform;
                go.transform.localPosition = Vector3.zero;
                DroneSkill droneSkill = go.AddComponent<DroneSkill>();
                droneSkill.Init(player, null, null);
                player.attackSkills.Add(droneSkill);
                player.AttackSkillDispatcher.Add(1f, droneSkill);
                break;
            case Define.SkillType.BlackHole:
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

    string GetSkillBenefitDescription(SkillInfo skillInfo)
    {
        switch (skillInfo.grade)
        {
            case Define.SkillGrade.Common:
                foreach (var item in skillInfo.benefitData.normal[skillInfo.level - 1])
                {
                    switch (item.Key)
                    {
                        case Define.SkillBenefitType.None:
                            return GetSkillDescription(skillInfo.skillType);
                        case Define.SkillBenefitType.DamageIncrease:
                            return $"공격력 증가 : +{item.Value}%";
                        case Define.SkillBenefitType.CoolTimeDecrease:
                            return $"쿨타임 감소 : +{item.Value}%";
                        case Define.SkillBenefitType.ProjectileAdd:
                            return $"발사체 추가 : +{item.Value}";
                        case Define.SkillBenefitType.DroneAdd:
                            return $"드론 {item.Value}기 추가";
                        case Define.SkillBenefitType.ExplosionRangeIncrease:
                            return $"폭발 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.AttackRangeIncrease:
                            return $"공격 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DurationIncrease:
                            return $"지속시간 증가 + {item.Value}%";
                        case Define.SkillBenefitType.HpRecoverContinuos:
                            return $"초당 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.MaxHpIncrease:
                            return $"최대 체력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DefIncrease:
                            return $"방어력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.SpeedIncrease:
                            return $"이동속도 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ProjectileAddPercent:
                            return $"발사체 갯수 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ExpIncrease:
                            return $"획득경험치 증가 + {item.Value}%";
                        case Define.SkillBenefitType.RootRangeIncrease:
                            return $"아이템 획득범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.levelUpHpRecover:
                            return $"레벨업 시 체력 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.EnemyHpDecrease:
                            return $"적 체력감소율 증가 + {item.Value}%";
                        default:
                            return "Error";
                    }
                }
                break;
            case Define.SkillGrade.Rare:
                foreach (var item in skillInfo.benefitData.rare[skillInfo.level - 1])
                {
                    switch (item.Key)
                    {
                        case Define.SkillBenefitType.None:
                            return GetSkillDescription(skillInfo.skillType);
                        case Define.SkillBenefitType.DamageIncrease:
                            return $"공격력 증가 : +{item.Value}%";
                        case Define.SkillBenefitType.CoolTimeDecrease:
                            return $"쿨타임 감소 : +{item.Value}%";
                        case Define.SkillBenefitType.ProjectileAdd:
                            return $"발사체 추가 : +{item.Value}";
                        case Define.SkillBenefitType.DroneAdd:
                            return $"드론 {item.Value}기 추가";
                        case Define.SkillBenefitType.ExplosionRangeIncrease:
                            return $"폭발 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.AttackRangeIncrease:
                            return $"공격 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DurationIncrease:
                            return $"지속시간 증가 + {item.Value}%";
                        case Define.SkillBenefitType.HpRecoverContinuos:
                            return $"초당 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.MaxHpIncrease:
                            return $"최대 체력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DefIncrease:
                            return $"방어력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.SpeedIncrease:
                            return $"이동속도 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ProjectileAddPercent:
                            return $"발사체 갯수 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ExpIncrease:
                            return $"획득경험치 증가 + {item.Value}%";
                        case Define.SkillBenefitType.RootRangeIncrease:
                            return $"아이템 획득범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.levelUpHpRecover:
                            return $"레벨업 시 체력 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.EnemyHpDecrease:
                            return $"적 체력감소율 증가 + {item.Value}%";
                        default:
                            return "GetSkillBenefitDescription Error";
                    }
                }
                break;
            case Define.SkillGrade.Unique:
                foreach (var item in skillInfo.benefitData.unique[skillInfo.level - 1])
                {
                    switch (item.Key)
                    {
                        case Define.SkillBenefitType.None:
                            return GetSkillDescription(skillInfo.skillType);
                        case Define.SkillBenefitType.DamageIncrease:
                            return $"공격력 증가 : +{item.Value}%";
                        case Define.SkillBenefitType.CoolTimeDecrease:
                            return $"쿨타임 감소 : +{item.Value}%";
                        case Define.SkillBenefitType.ProjectileAdd:
                            return $"발사체 추가 : +{item.Value}";
                        case Define.SkillBenefitType.DroneAdd:
                            return $"드론 {item.Value}기 추가";
                        case Define.SkillBenefitType.ExplosionRangeIncrease:
                            return $"폭발 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.AttackRangeIncrease:
                            return $"공격 범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DurationIncrease:
                            return $"지속시간 증가 + {item.Value}%";
                        case Define.SkillBenefitType.HpRecoverContinuos:
                            return $"초당 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.MaxHpIncrease:
                            return $"최대 체력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.DefIncrease:
                            return $"방어력 증가 + {item.Value}%";
                        case Define.SkillBenefitType.SpeedIncrease:
                            return $"이동속도 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ProjectileAddPercent:
                            return $"발사체 갯수 증가 + {item.Value}%";
                        case Define.SkillBenefitType.ExpIncrease:
                            return $"획득경험치 증가 + {item.Value}%";
                        case Define.SkillBenefitType.RootRangeIncrease:
                            return $"아이템 획득범위 증가 + {item.Value}%";
                        case Define.SkillBenefitType.levelUpHpRecover:
                            return $"레벨업 시 체력 회복량 증가 + {item.Value}%";
                        case Define.SkillBenefitType.EnemyHpDecrease:
                            return $"적 체력감소율 증가 + {item.Value}%";
                        default:
                            return "Error";
                    }
                }
                break;
            default:
                break;
        }
        return "Error";
    }
    string GetSkillDescription(Define.SkillType skillType)
    {
        switch (skillType)
        {
            case Define.SkillType.PlayerNormal:
                return "적에게 데미지를 입히는 투사체를 발사합니다";
            case Define.SkillType.FlameThrower:
                return "이동하는 방향 전방으로 지속적인 피해를 입히는 화염을 발사합니다";
            case Define.SkillType.ImpactWave:
                return "주변의 모든 적을 밀쳐내며 데미지주는 충격파를 발생시킵니다.";
            case Define.SkillType.PlayerLaser:
                return "일직선 상의 모든 적을 공격하는 레이저를 발사합니다.";
            case Define.SkillType.PlayerMissile:
                return "적에게 닿으면 폭발하는 미사일을 발사합니다.";
            case Define.SkillType.Napalm:
                return "적에게 닿으면 폭발하며, 넓은 범위에 지속적으로 데미지를 주는 영역을 생성합니다.";
            case Define.SkillType.PulseWave:
                return "가까운 적을 공격하는 전자기파를 발사합니다";
            case Define.SkillType.PlayerBombing:
                return "넓은 영역에 지원 폭격을 가합니다";
            case Define.SkillType.PlayerShield:
                return "적의 공격을 막아주는 방어막을 형성합니다";
            case Define.SkillType.Drone:
                return "적을 자동으로 공격하는 드론을 소환합니다";
            case Define.SkillType.BlackHole:
                return "적을 빨아들이는 블랙홀을 생성합니다. 블랙홀 가운데에 도달하는 적은 즉시 파괴됩니다";
            case Define.SkillType.ATKIncrease:
                return "공격력 증가";
            case Define.SkillType.MaxHPIncrease:
                return "최대 체력 증가";
            case Define.SkillType.DEFIncrease:
                return "방어력 증가";
            case Define.SkillType.MoveSpeedIncrease:
                return "이동속도 증가";
            case Define.SkillType.ProjectileIncrease:
                return "발사체 개수 증가";
            case Define.SkillType.ExpIncrease:
                return "경험치 획득량 증가";
            case Define.SkillType.CoolTimeIncrease:
                return "쿨타임 감소";
            case Define.SkillType.RootRangeIncrease:
                return "아이템 획득범위 증가";
            case Define.SkillType.HPRecoveryAssistIncrease:
                return "레벨업시 체력 회복량 증가";
            case Define.SkillType.EnemyHpDownIncrease:
                return "적 체력 감소율 증가";
            case Define.SkillType.HPRecoveryAmountIncrease:
                return "체력회복량 증가";
            case Define.SkillType.DurationIncrease:
                return "스킬 지속시간 증가";
            case Define.SkillType.RangeIncrease:
                return "스킬 공격범위 증가";
            case Define.SkillType.HPRecoveryPerSecIncrease:
                return "초당 체력회복량 증가";
            default:
                return $"Error! : Get {skillType}";
                break;
        }
    }
}
