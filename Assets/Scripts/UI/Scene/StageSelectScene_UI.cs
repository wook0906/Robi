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
            PlayerPrefs.SetInt("SelectedMap", (int)Define.StageType.Stage1);
        }

        SetMapInfo((Define.StageType)PlayerPrefs.GetInt("SelectedMap"));

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

        SetMapInfo((Define.StageType)mapType);
    }
    void OnClickNextMapButton()
    {
        int mapType = PlayerPrefs.GetInt("SelectedMap");
        mapType++;
        if (mapType >= (int)Define.StageType.MaxCount)
            mapType = (int)Define.StageType.MaxCount - 1;

        PlayerPrefs.SetInt("SelectedMap", mapType);

        SetMapInfo((Define.StageType)mapType);
    }
    void SetMapInfo(Define.StageType mapType)
    {
        switch (mapType)
        {
            case Define.StageType.Stage1:
                break;
            case Define.StageType.Stage2:
                break;
            case Define.StageType.Stage3:
                break;
            case Define.StageType.Stage4:
                break;
            default:
                break;
        }
    }
}
