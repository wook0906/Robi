using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_Popup : PopupUI
{
    enum Buttons
    {
        Korean_Button,
        English_Button,
        BGMOn_Button,
        BGMOff_Button,
        ESOn_Button,
        ESOff_Button,
        VibeOn_Button,
        VibeOff_Button,
        CloudSave_Button,
        CloudLoad_Button,
        Back_Button,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Back_Button).onClick.AddListener(OnClickBackButton);
        Get<Button>((int)Buttons.Korean_Button).onClick.AddListener(OnClickKoreanButton);
        Get<Button>((int)Buttons.English_Button).onClick.AddListener(OnClickEnglishButton);
        Get<Button>((int)Buttons.BGMOn_Button).onClick.AddListener(OnClickBgmOnButton);
        Get<Button>((int)Buttons.BGMOff_Button).onClick.AddListener(OnClickBgmOffButton);
        Get<Button>((int)Buttons.ESOn_Button).onClick.AddListener(OnClickEsOnButton);
        Get<Button>((int)Buttons.ESOff_Button).onClick.AddListener(OnClickEsOffButton);
        Get<Button>((int)Buttons.VibeOn_Button).onClick.AddListener(OnClickVibeOnButton);
        Get<Button>((int)Buttons.VibeOff_Button).onClick.AddListener(OnClickVibeOffButton);
        Get<Button>((int)Buttons.CloudSave_Button).onClick.AddListener(OnClickCloudSaveButton);
        Get<Button>((int)Buttons.CloudLoad_Button).onClick.AddListener(OnClickCloudLoadButton);

        //TODO 초기세팅
        //언어는 기기세팅 따라
        //BGM ON
        //ES ON
        //Vibe ON
    }
    void OnClickBackButton()
    {
        ClosePopupUI();
    }
    void OnClickKoreanButton()
    {
        //TODO 언어 한국어로 세팅
    }
    void OnClickEnglishButton()
    {
        //TODO 언어 영어로 세팅
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
    void OnClickCloudSaveButton()
    {
        //TODO Cloud Save
    }
    void OnClickCloudLoadButton()
    {
        //TODO Cloud Load
    }
}
