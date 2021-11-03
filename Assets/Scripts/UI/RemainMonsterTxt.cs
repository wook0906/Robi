using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainMonsterTxt : MonoBehaviour
{
    public static RemainMonsterTxt S;
    public Text m_Txt;

    private void Awake()
    {
        S = this;
        m_Txt = this.gameObject.GetComponent<Text>();
    }
}
