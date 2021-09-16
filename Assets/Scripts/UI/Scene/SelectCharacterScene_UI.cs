using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterScene_UI : SceneUI
{
    enum Texts
    {
        Asset1_Text,
        Asset2_Text,
        HP_Text,
        Def_Text,
        Atk_Text,
        MoveSpeed_Text,
        ItemRootRange_Text,
        Exp_Text,
    }
    enum Buttons
    {
        HPLevelUp_Button,
        AtkLevelUp_Button,
        DefLevelUp_Button,
        MoveSpeedLevelUp_Button,
        ItemRootRangeLevelUp_Button,
        ExpLevelUp_Button,
        Back_Button,
    }
    enum Images
    {
        HPLevel_Slider,
        DefLevel_Slider,
        AtkLevel_Slider,
        MoveSpeedLevel_Slider,
        ItemRootRangeLevel_Slider,
        ExpLevel_Slider,
    }
    public override void Init()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = transform.Find("SelectCharacterCamera").GetComponent<Camera>();
        transform.position = new Vector3(1.1f, -1.1f, 100f);

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Button>((int)Buttons.Back_Button).onClick.AddListener(OnClickBackButton);

        Get<Text>((int)Texts.Asset1_Text).text = Managers.Data.Asset1.ToString();
        Get<Text>((int)Texts.Asset2_Text).text = Managers.Data.Asset2.ToString();

        RenewDatas();

        Get<Button>((int)Buttons.HPLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].HpUpgrade;
            int cost = (((newValue + 1) * 2) * ((newValue + 1) * 2) * DataManager.charHDAStatCoefficient);
            
            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].HpUpgrade++;
                RenewDatas();
            }
        }));
        Get<Button>((int)Buttons.DefLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].DefUpgrade;
            int cost = (((newValue + 1) * 2) * ((newValue + 1) * 2) * DataManager.charHDAStatCoefficient);
            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].DefUpgrade++;
                RenewDatas();
            }
        }));
        Get<Button>((int)Buttons.AtkLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].AtkUpgrade;
            int cost = (((newValue + 1) * 2) * ((newValue + 1) * 2) * DataManager.charHDAStatCoefficient);
            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].AtkUpgrade++;
                RenewDatas();
            }
        }));
        Get<Button>((int)Buttons.MoveSpeedLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].MoveSpeedUpgrade;
            int cost = (((newValue + 1) * (newValue + 1)) * DataManager.charElseStatCoefficient);

            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].MoveSpeedUpgrade++;
                RenewDatas();
            }
        }));
        Get<Button>((int)Buttons.ItemRootRangeLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].RootRangeUpgrade;
            int cost = (((newValue + 1) * (newValue + 1)) * DataManager.charElseStatCoefficient);

            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].RootRangeUpgrade++;
                RenewDatas();
            }
        }));
        Get<Button>((int)Buttons.ExpLevelUp_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            int newValue = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].ExpUpgrade;
            int cost = (((newValue + 1) * (newValue + 1)) * DataManager.charElseStatCoefficient);

            if (Managers.Data.Asset2 < cost)
            {
                Debug.Log($"자산부족! 필요자산 : {cost}, 현재 자산 : {Managers.Data.Asset2}");
                return;
            }
            else
            {
                Managers.Data.Asset2 -= cost;
                Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter].ExpUpgrade++;
                RenewDatas();
            }
        }));

    }
    void RenewDatas()
    {
        CharacterStatData basicStatData = Managers.Data.characterStatDict[Managers.Data.SelectedCharacter];
        CharacterStatUpGradeInfo statUpgradeData = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter];

        Get<Text>((int)Texts.Asset1_Text).text = Managers.Data.Asset1.ToString();
        Get<Text>((int)Texts.Asset2_Text).text = Managers.Data.Asset2.ToString();

        Get<Text>((int)Texts.HP_Text).text = (basicStatData._maxHp + (statUpgradeData.HpUpgrade * (basicStatData._maxHp * 0.1f))).ToString();
        Get<Text>((int)Texts.Def_Text).text = (basicStatData._def + (statUpgradeData.DefUpgrade * (basicStatData._def * 0.1f))).ToString();
        Get<Text>((int)Texts.Atk_Text).text = (basicStatData._atk + (statUpgradeData.AtkUpgrade * (basicStatData._atk * 0.025f))).ToString();
        Get<Text>((int)Texts.MoveSpeed_Text).text = (basicStatData._moveSpeed + (statUpgradeData.MoveSpeedUpgrade * 0.05)).ToString();
        Get<Text>((int)Texts.ItemRootRange_Text).text = (basicStatData._itemRootingRangeRadius + (statUpgradeData.RootRangeUpgrade * 0.05)).ToString();
        Get<Text>((int)Texts.Exp_Text).text = (basicStatData._expAcquirePercentage + (statUpgradeData.ExpUpgrade * 0.05)).ToString();

        Get<Image>((int)Images.HPLevel_Slider).fillAmount = statUpgradeData.HpUpgrade / 30f;
        Get<Image>((int)Images.DefLevel_Slider).fillAmount = statUpgradeData.DefUpgrade / 30f;
        Get<Image>((int)Images.AtkLevel_Slider).fillAmount = statUpgradeData.AtkUpgrade / 30f;
        Get<Image>((int)Images.MoveSpeedLevel_Slider).fillAmount = statUpgradeData.MoveSpeedUpgrade / 5f;
        Get<Image>((int)Images.ItemRootRangeLevel_Slider).fillAmount = statUpgradeData.RootRangeUpgrade / 5f;
        Get<Image>((int)Images.ExpLevel_Slider).fillAmount = statUpgradeData.ExpUpgrade / 5f;

        if(statUpgradeData.HpUpgrade >= 30)
            Get<Button>((int)Buttons.HPLevelUp_Button).interactable = false;
        if (statUpgradeData.AtkUpgrade >= 30)
            Get<Button>((int)Buttons.AtkLevelUp_Button).interactable = false;
        if (statUpgradeData.DefUpgrade >= 30)
            Get<Button>((int)Buttons.DefLevelUp_Button).interactable = false;
        if (statUpgradeData.MoveSpeedUpgrade >= 5)
            Get<Button>((int)Buttons.MoveSpeedLevelUp_Button).interactable = false;
        if (statUpgradeData.RootRangeUpgrade >= 5)
            Get<Button>((int)Buttons.ItemRootRangeLevelUp_Button).interactable = false;
        if (statUpgradeData.ExpUpgrade >= 5)
            Get<Button>((int)Buttons.ExpLevelUp_Button).interactable = false;
    }
    void OnClickBackButton()
    {
        Managers.UI.ShowSceneUI<LobbyScene_UI>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Managers.Data.Asset2 += 1000;
            RenewDatas();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            CharacterStatUpGradeInfo statUpgradeData = Managers.Data.charStatUpgradeDict[Managers.Data.SelectedCharacter];
            statUpgradeData.HpUpgrade = 0;
            statUpgradeData.DefUpgrade = 0;
            statUpgradeData.AtkUpgrade = 0;
            statUpgradeData.MoveSpeedUpgrade = 0;
            statUpgradeData.RootRangeUpgrade = 0;
            statUpgradeData.ExpUpgrade = 0;
            RenewDatas();
        }
    }
    public void OnClickCharacterButton()//Character Type을 받는걸로하자
    {

    }
}
