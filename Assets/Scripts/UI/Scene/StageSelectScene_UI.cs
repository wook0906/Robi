using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScene_UI : SceneUI
{
    enum Images
    {
        Stage_Image,
    }
    enum Buttons
    {
        PrevMapSelect_Button,
        NextMapSelect_Button,
        Play_Button,
        Back_Button,
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        if (PlayerPrefs.HasKey("SelectedMap"))
        {
            PlayerPrefs.SetInt("SelectedMap", (int)Define.MapType.map1);
        }

        SetMapInfo((Define.MapType)PlayerPrefs.GetInt("SelectedMap"));

        Get<Button>((int)Buttons.Back_Button).onClick.AddListener(OnClickBackButton);
        Get<Button>((int)Buttons.Play_Button).onClick.AddListener(OnClickPlayButton);
        Get<Button>((int)Buttons.PrevMapSelect_Button).onClick.AddListener(OnClickPrevMapButton);
        Get<Button>((int)Buttons.NextMapSelect_Button).onClick.AddListener(OnClickNextMapButton);

    }
    void OnClickBackButton()
    {
        Managers.UI.ShowSceneUI<LobbyScene_UI>();
    }
    void OnClickPlayButton()
    {
        Managers.Scene.LoadSceneAsync(Define.Scene.GameScene);
    }
    void OnClickPrevMapButton()
    {
        int mapType = PlayerPrefs.GetInt("SelectedMap");
        mapType--;
        if (mapType < 0)
            mapType = 0;
        PlayerPrefs.SetInt("SelectedMap", mapType);

        SetMapInfo((Define.MapType)mapType);
    }
    void OnClickNextMapButton()
    {
        int mapType = PlayerPrefs.GetInt("SelectedMap");
        mapType++;
        if (mapType >= (int)Define.MapType.MaxCount)
            mapType = (int)Define.MapType.MaxCount - 1;

        PlayerPrefs.SetInt("SelectedMap", mapType);

        SetMapInfo((Define.MapType)mapType);
    }
    void SetMapInfo(Define.MapType mapType)
    {
        switch (mapType)
        {
            case Define.MapType.map1:
                break;
            case Define.MapType.map2:
                break;
            case Define.MapType.map3:
                break;
            case Define.MapType.map4:
                break;
            default:
                break;
        }
    }
}
