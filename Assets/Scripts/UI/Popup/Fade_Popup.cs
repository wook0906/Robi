using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Popup : PopupUI
{
    enum Images
    {
        ScreenImage
    }

    public bool IsDone { private set; get; }
    public bool IsStartRightAway { set; get; } = false;
    Image screenImage;

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));

        screenImage = Get<Image>((int)Images.ScreenImage);
        IsDone = false;
    }

    public void FadeIn(float time, float delayTime = 0f)
    {
        IsDone = false;
        IsStartRightAway = false;
        StartCoroutine(CoFadeIn(time, delayTime));
    }

    private IEnumerator CoFadeIn(float time, float delayTime)
    {
        yield return new WaitUntil(()=>screenImage != null);
        while (delayTime > 0 && !IsStartRightAway)
        {
            delayTime -= 0.02f;
            yield return null;
        }
        screenImage.color = Color.black;
        float elaspedTime = 0f;
        while (time > elaspedTime)
        {
            screenImage.color = new Color(screenImage.color.r, screenImage.color.g, screenImage.color.b, Mathf.Lerp(1f, 0f, elaspedTime / time));
            elaspedTime += 0.02f;
            yield return null;
        }
        screenImage.color = Color.clear;
        IsDone = true;

        if (transform.root.name == "DontDestroyObject")
            Destroy(this.gameObject);
    }

    public void FadeOut(float time, float delayTime = 0f)
    {
        IsDone = false;
        IsStartRightAway = false;
        StartCoroutine(CoFadeOut(time, delayTime));
    }

    private IEnumerator CoFadeOut(float time, float delayTime)
    {
        while (delayTime > 0 && !IsStartRightAway)
        {
            delayTime -= 0.02f;
            yield return null;
        }

        screenImage.color = Color.clear;
        float elaspedTime = 0f;
        while (time > elaspedTime)
        {
            screenImage.color = new Color(screenImage.color.r, screenImage.color.g, screenImage.color.b, Mathf.Lerp(0f, 1f, elaspedTime / time));
            elaspedTime += 0.02f;
            yield return null;
        }
        screenImage.color = Color.black;
        IsDone = true;
    }
}
