using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit_Popup : PopupUI
{
    enum Buttons
    {
        Confirm_Button,
        Continue_Button,
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Confirm_Button).onClick.AddListener(OnClickConfirmButton);
        Get<Button>((int)Buttons.Continue_Button).onClick.AddListener(OnClickContinueButton);
    }
    void OnClickConfirmButton()
    {
        //TODO 로비로 돌아갈 떄 해야할것....?
        
        Managers.Scene.LoadSceneAsync(Define.Scene.LobbyScene);
    }
    void OnClickContinueButton()
    {
        ClosePopupUI();
    }
}
