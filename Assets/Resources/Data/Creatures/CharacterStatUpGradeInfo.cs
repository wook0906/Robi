using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatUpGradeInfo
{
    private int hpUpgrade = 0;
    public int HpUpgrade
    {
        get { return hpUpgrade; }
        set
        {
            if (value > 30)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_HpUpgrade", value);
            hpUpgrade = value;
        }
    }
    private int defUpgrade = 0;
    public int DefUpgrade
    {
        get { return defUpgrade; }
        set
        {
            if (value > 30)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_DefUpgrade", value);
            defUpgrade = value;
        }
    }
    private int atkUpgrade = 0;
    public int AtkUpgrade
    {
        get { return atkUpgrade; }
        set
        {
            if (value > 30)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_AtkUpgrade", value);
            atkUpgrade = value;
        }
    }
    private int moveSpeedUpgrade = 0;
    public int MoveSpeedUpgrade
    {
        get { return moveSpeedUpgrade; }
        set
        {
            if (value > 5)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_MoveSpeedUpgrade", value);
            moveSpeedUpgrade = value;
        }
    }
    private int rootRagneUpgrade = 0;
    public int RootRangeUpgrade
    {
        get { return rootRagneUpgrade; }
        set
        {
            if (value > 5)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_RootRangeUpgrade", value);
            rootRagneUpgrade = value;
        }
    }
    private int expUpgrade;
    public int ExpUpgrade
    {
        get { return expUpgrade; }
        set
        {
            if (value > 5)
            {
                return;
            }
            PlayerPrefs.SetInt($"{Managers.Data.SelectedCharacter}_ExpUpgrade", value);
            expUpgrade = value;
        }
    }
}
