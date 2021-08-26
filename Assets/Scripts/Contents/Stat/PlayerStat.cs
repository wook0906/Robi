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

    private GameScene_UI gameSceneUI;

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            //Debug.Log($"Exp:{_exp}");
            int level = Level;

            //Data.CreatureStat stat;
            //if (Managers.Data.characterStatDict.TryGetValue(level + 1, out stat) == false)
            //    return;

            //gameSceneUI.UpdateExpUI((float)_exp / stat.totalExp);
            //if (_exp < stat.totalExp)
            //    return;

            //level++;

            if (level != Level)
            {
                //Debug.Log("Level Up!");
                gameSceneUI.UpdateLevelUI(level);
                Level = level;
                //SetStat(Level);
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
}
