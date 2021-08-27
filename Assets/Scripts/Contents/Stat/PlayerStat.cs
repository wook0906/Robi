using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerStat : CreatureStat
{
    [SerializeField]
    protected float _atkCoefficient;
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;
    [SerializeField]
    protected int _totalExp;
    
    private GameScene_UI gameSceneUI;

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            //Debug.Log($"Exp:{_exp}");
            int level = Level;

            gameSceneUI.UpdateExpUI((float)_exp / _totalExp);
            if (_exp < _totalExp)
                return;

            level++;

            _totalExp = RenewTotalExp(level);
            if (level != Level)
            {
                Managers.UI.ShowPopupUI<LevelUp_Popup>();
                gameSceneUI.UpdateLevelUI(level);
                
                Level = level;
                _exp = 0;
            }
            
        }
    }
    public float AtkCoefficient { get { return _atkCoefficient; } set { _atkCoefficient = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    public override float Hp
    {
        get => base.Hp;
        set
        {
            _hp = value;
            if(gameSceneUI)
                gameSceneUI.UpdateHpUI(_hp / _maxHp);
        }
    }
    private IEnumerator Start()
    {
        _level = 1;
        gameSceneUI = FindObjectOfType<GameScene_UI>();
        yield return new WaitUntil(() => gameSceneUI);
    }


    public void SetStat()
    {
        Dictionary<Define.CharacterType, CharacterStatData> dict = Managers.Data.characterStatDict;
        CharacterStatData stat = dict[(Define.CharacterType)System.Enum.Parse(typeof(Define.CharacterType), name)];

        Hp = stat._maxHp;
        _maxHp = stat._maxHp;
        _atkCoefficient = stat._atk;
        _moveSpeed = stat._moveSpeed;
        _totalExp = 110;
    }

    public override void OnAttacked(BaseController attacker, float damage)
    {
        base.OnAttacked(attacker, damage);
    }

    protected override void OnDead(BaseController attacker)
    {
        Debug.Log("Player Dead");
        Destroy(gameObject);
        Managers.Game.OnGameOver();
    }

    //public override string ToString()
    //{
    //    StringBuilder builder = new StringBuilder();

    //    builder.Append($"Level:{Level} ");
    //    builder.Append($"HP:{MaxHp} ");
    //    builder.Append($"Damage:{Damage} ");
    //    builder.Append($"Speed:{MoveSpeed} ");
    //    builder.Append($"HpRecovery:{HpRecoveryPerSecond} ");
    //    builder.Append($"TotalExp:{Exp} ");

    //    return builder.ToString();
    //}
    int RenewTotalExp(int level)
    {
        int a = 1;
        int b = 1;
        int c = 0;
        int X = level * 10;
        return a * (X * X) + b * X + c;
    }
}
