using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver_Popup : PopupUI
{
    enum Buttons
    {
        Back_Button
    }
    public override void Init()
    {
        base.Init();
        Time.timeScale = 0f;

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Back_Button).onClick.AddListener(OnClickBackButton);

    }
    void OnClickBackButton()
    {
        Time.timeScale = 1f;
        Managers.Scene.LoadSceneAsync(Define.Scene.LobbyScene);
    }
}
