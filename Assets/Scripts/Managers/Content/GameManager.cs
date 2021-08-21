using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public MonsterSpawner MonsterSpawner { get; set; }

    public void Init()
    {
        
    }

    public void OnGameOver()
    {
        //CancelInvoke();
        Debug.Log("GameOver");
        MonsterSpawner.Clear();
        Managers.UI.ShowPopupUI<GameOver_Popup>();
    }
}
