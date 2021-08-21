using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterScene_UI : SceneUI
{
    enum Buttons
    {
        HPLevelUp_Button,
        AtkLevelUp_Button,
        DefLevelUp_Button,
        ItemRootRangeLevelUp_Button,
        ExpLevelUp_Button,
        Back_Button,
    }
    public override void Init()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = transform.Find("SelectCharacterCamera").GetComponent<Camera>();
        transform.position = new Vector3(1.1f, -1.1f, 100f);

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Back_Button).onClick.AddListener(OnClickBackButton);

        
    }
    void OnClickBackButton()
    {
        Managers.UI.ShowSceneUI<LobbyScene_UI>();
    }
    public void OnClickCharacterButton()//Character Type을 받는걸로하자
    {

    }
}
