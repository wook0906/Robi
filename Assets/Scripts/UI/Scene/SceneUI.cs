using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : UIBase
{
	public override void Init()
	{
		Managers.UI.SetCanvas(gameObject, false);
	}
	public virtual void OnActive()
    {

    }
}
