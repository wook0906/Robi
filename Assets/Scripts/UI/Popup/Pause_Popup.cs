using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Popup : PopupUI
{
    enum Buttons
    {
        BGMOn_Button,
        BGMOff_Button,
        ESOn_Button,
        ESOff_Button,
        VibeOn_Button,
        VibeOff_Button,
        Continue_Button,
        Exit_Button,
    }
    public override void Init()
    {
        base.Init();
        Time.timeScale = 0f;
        
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Continue_Button).onClick.AddListener(OnClickContinueButton);
        Get<Button>((int)Buttons.BGMOn_Button).onClick.AddListener(OnClickBgmOnButton);
        Get<Button>((int)Buttons.BGMOff_Button).onClick.AddListener(OnClickBgmOffButton);
        Get<Button>((int)Buttons.ESOn_Button).onClick.AddListener(OnClickEsOnButton);
        Get<Button>((int)Buttons.ESOff_Button).onClick.AddListener(OnClickEsOffButton);
        Get<Button>((int)Buttons.VibeOn_Button).onClick.AddListener(OnClickVibeOnButton);
        Get<Button>((int)Buttons.VibeOff_Button).onClick.AddListener(OnClickVibeOffButton);
        Get<Button>((int)Buttons.Exit_Button).onClick.AddListener(OnClickExitButton);

    }
    void OnClickContinueButton()
    {
        ClosePopupUI();
        Time.timeScale = 1f;
    }
    void OnClickBgmOnButton()
    {
        //TODO BGM On
    }
    void OnClickBgmOffButton()
    {
        //TODO BGM Off
    }
    void OnClickEsOnButton()
    {
        //TODO ES On
    }
    void OnClickEsOffButton()
    {
        //TODO ES Off
    }
    void OnClickVibeOnButton()
    {
        //TODO Vibe Off
    }
    void OnClickVibeOffButton()
    {
        //TODO Vibe Off
    }
    void OnClickExitButton()
    {
        Managers.UI.ShowPopupUI<Exit_Popup>();
    }
}
