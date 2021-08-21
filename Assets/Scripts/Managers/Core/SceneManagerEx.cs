using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private AsyncOperation asyncOperation;
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    float progressValue = 0f;

    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));
    }
    public void LoadSceneAsync(Define.Scene type, float delayTime = 0)
    {
        

        Fade_Popup popup = Managers.UI.MakeDontDestroyPopupUI<Fade_Popup>();
        Managers.Instance.StartCoroutine(CoStartChangeScene(type, delayTime));
    }

    private IEnumerator CoStartChangeScene(Define.Scene type, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Managers.Instance.StartCoroutine(CoLoadSceneAsync(type));
    }

    private IEnumerator CoLoadSceneAsync(Define.Scene type)
    {
        Fade_Popup fadePopup = null;
        yield return new WaitUntil(() =>
        {
            fadePopup = GameObject.FindObjectOfType<Fade_Popup>();

            return fadePopup != null;
        });


        asyncOperation = SceneManager.LoadSceneAsync(type.ToString(),LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            progressValue = asyncOperation.progress;
            yield return null;
        }
        progressValue = 1f;

        fadePopup.FadeOut(1f);
        yield return new WaitUntil(() => { return fadePopup.IsDone; });

        if (Managers.Scene.CurrentScene != null)
            Managers.Scene.CurrentScene.Clear();
        else
            Managers.UI.CloseAllPopupUI();

        Managers.Clear();
        asyncOperation.allowSceneActivation = true;
        asyncOperation = null;
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        if(CurrentScene)
            CurrentScene.Clear();
    }

    public float GetProgressValue()
    {
        return progressValue;
    }
}
