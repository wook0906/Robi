using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene_UI : SceneUI
{
    DateTime GameStartTime;
    Text timerText;
    Image hpBar;
    Image expBar;
    Text levelText;

    enum Texts
    {
        Timer_Text,
        Level_Text,
    }
    enum Images
    {
        HPFill,
        ExpFill
    }
    enum Buttons
    {
        Pause_Button
    }

    public override void Init()
    {
        GameStartTime = DateTime.Now;

        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GameObject fadePopup = GameObject.Find("Fade_Popup");
        if (fadePopup)
            fadePopup.GetComponent<Fade_Popup>().FadeIn(1f);

        GetButton((int)Buttons.Pause_Button).onClick.AddListener(OnClickPauseButton);
        hpBar = GetImage((int)Images.HPFill);
        hpBar.fillAmount = 1f;
        expBar = GetImage((int)Images.ExpFill);
        expBar.fillAmount = 0f;
        levelText = GetText((int)Texts.Level_Text);
        levelText.text = "1";
        timerText = GetText((int)Texts.Timer_Text);
        timerText.text = "00:00";

    }
    void OnClickPauseButton()
    {
        Managers.UI.ShowPopupUI<Pause_Popup>();
    }

    public void UpdateHpUI(float value)
    {
        if(hpBar)
            hpBar.fillAmount = value;
    }

    public void UpdateExpUI(float value)
    {
        if(expBar)
            expBar.fillAmount = value;
    }
    public void UpdateLevelUI(int level)
    {
        if(levelText)
            GetText((int)Texts.Level_Text).text = level.ToString();
    }
    private void Update()
    {
        TimeSpan elapsedTime = DateTime.Now - GameStartTime;
        timerText.text = elapsedTime.ToString(@"mm\:ss");
    }

}
