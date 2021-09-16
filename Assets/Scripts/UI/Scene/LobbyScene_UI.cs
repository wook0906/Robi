using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene_UI : SceneUI
{
    [SerializeField]
    GameObject mainCam;


    enum Texts
    {
        Asset1_Text,
        Asset2_Text,
    }
    enum Buttons
    {
        Event_Button,
        Quest_Button,
        CharacterSelect_Button,
        Shop_Button,
        Option_Button,
        Start_Button,
    }

    public override void Init()
    {
        mainCam = GameObject.Find("MainCamera");
        GetComponent<Canvas>().worldCamera = Camera.main;
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GameObject fadePopup = GameObject.Find("Fade_Popup");
        if(fadePopup)
            fadePopup.GetComponent<Fade_Popup>().FadeIn(1f);

        Get<Button>((int)Buttons.Event_Button).onClick.AddListener(OnClickEventButton);
        Get<Button>((int)Buttons.Quest_Button).onClick.AddListener(OnClickQuestButton);
        Get<Button>((int)Buttons.CharacterSelect_Button).onClick.AddListener(OnClickCharacterSelectButton);
        Get<Button>((int)Buttons.Shop_Button).onClick.AddListener(OnClickShopButton);
        Get<Button>((int)Buttons.Option_Button).onClick.AddListener(OnClickOptionButton);
        Get<Button>((int)Buttons.Start_Button).onClick.AddListener(OnClickStartButton);

        Get<Text>((int)Texts.Asset1_Text).text = Managers.Data.Asset1.ToString();
        Get<Text>((int)Texts.Asset2_Text).text = Managers.Data.Asset2.ToString();

        //TODO DataManager의 SelectedCharacter 변수로 로비 화면에서 띄워줄 로봇 캐릭터 초기화

    }
    void OnClickEventButton()
    {
        //TODO
        //이벤트 UI띄움
    }
    void OnClickQuestButton()
    {
        //TODO
        //퀘스트 UI띄움
    }
    void OnClickCharacterSelectButton()
    {
        Managers.UI.ShowSceneUI<SelectCharacterScene_UI>();
        mainCam.SetActive(false);
    }
    void OnClickShopButton()
    {
        //TODO
        //샵 UI띄움
        //Managers.UI.ShowSceneUI<ShopScene_UI>();
    }
    void OnClickOptionButton()
    {
        Managers.UI.ShowPopupUI<Option_Popup>();
    }
    void OnClickStartButton()
    {
        Managers.UI.ShowSceneUI<StageSelectScene_UI>();
        //Managers.Scene.LoadSceneAsync(Define.Scene.GameScene);
    }
    public override void OnActive()
    {
        base.OnActive();
        if(mainCam)
            mainCam.SetActive(true);
    }



}
