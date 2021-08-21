using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene_UI : SceneUI
{
    Image progressBar;
    enum Images
    {
        LoadingProgressBar,
    }

    private void Awake()
    {
        GameObject dontDestroyObject = new GameObject { name = "DontDestroyObject" };
        DontDestroyOnLoad(dontDestroyObject);
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));

        progressBar = GetImage((int)Images.LoadingProgressBar);
        progressBar.fillAmount = 0f;

        Managers.Scene.LoadSceneAsync(Define.Scene.LobbyScene);
    }

    private void Update()
    {
        progressBar.fillAmount = Managers.Scene.GetProgressValue();
    }
}
