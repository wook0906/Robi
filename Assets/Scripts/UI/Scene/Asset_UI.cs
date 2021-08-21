using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset_UI : SceneUI
{
    public override void Init()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
