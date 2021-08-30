using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp_Popup : PopupUI
{
    PlayerController player;
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

        Get<Button>((int)Buttons.Item1_Button).onClick.AddListener(OnClickSkillButton);
        Get<Button>((int)Buttons.Item2_Button).onClick.AddListener(OnClickSkillButton);
        Get<Button>((int)Buttons.Item3_Button).onClick.AddListener(OnClickSkillButton);

        GameScene gameScene = Managers.Scene.CurrentScene as GameScene;
        player = gameScene.player.GetComponent<PlayerController>();

        Time.timeScale = 0f;
    }
    void OnClickSkillButton()
    {
        foreach (var item in player.attackSkills)
        {
            if (item.Stat.SkillType == Define.AttackSkillType.PlayerNormal)
            {
                item.LevelUp(Define.SkillGrade.Unique);
                Debug.Log("skill LevelUp~");
            }
        }
        ClosePopupUI();
        Time.timeScale = 1f;
    }
}
