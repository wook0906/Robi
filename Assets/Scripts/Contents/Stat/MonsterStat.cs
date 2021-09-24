using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : CreatureStat
{
    protected BaseController owner;

    [SerializeField]
    protected int _damage;
    [SerializeField]
    protected float _defense;
    //[SerializeField]
    //protected float _detectRange;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected float _attackInterval;
    [SerializeField]
    protected int _exp;
    
    public int _normalAttack = 1;
    public int _launchBomb = 1;
    public int _missile = 1;
    public int _immortalDuration;
    public int _immortalCoolTime;

    protected bool _useBerserk = false;
    bool isOnBerserk = false;
    protected bool _useSuicideBombing = false;
    bool _isOnImmortal;

    public Define.MonsterType mobType;


    public override float MaxHp
    {
        get { return _maxHp; }
        set
        {
            float maxHp = value - (value * MonsterController.target.GetComponent<PlayerController>().passiveSkill.skillDict[Define.SkillType.EnemyHpDownIncrease]);
            _maxHp = maxHp;
        }
    }
    public override float Hp
    {
        get { return _hp; }
        set 
        {
            if (value > MaxHp)
                _hp = MaxHp;
            else
                _hp = value;
        }
    }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    //public float DetectRange { get { return _detectRange; } set { _detectRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }
    public bool IsOnImmortal { get { return _isOnImmortal; } set { _isOnImmortal = value; } }
    //public int Exp { get { return _exp; } set { _exp = value; } }
    public float ShieldHp 
    { 
        get 
        {
            MonsterShield shieldSkill = GetComponentInChildren<MonsterShield>();
            if (shieldSkill == null) return 0;
            else return GetComponentInChildren<MonsterShield>().ShieldHp;
        }
        set 
        {
            MonsterShield shieldSkill = GetComponentInChildren<MonsterShield>();
            if (shieldSkill == null) return;
            GetComponentInChildren<MonsterShield>().ShieldHp = value;
        }
    }

    private void Start()
    {
        GameScene scene = Managers.Scene.CurrentScene as GameScene;
        InitStat(scene.player.GetComponent<PlayerStat>().Level);
    }

    public void InitStat(int playerLevel)
    {
        //Dictionary<int, Data.CreatureStat> dict = Managers.Data.MonsterStatDict;
        Dictionary<Define.MonsterType, MonsterStatData> dict = Managers.Data.monsterStatDict;
        mobType = (Define.MonsterType)System.Enum.Parse(typeof(Define.MonsterType), name);
        MonsterStatData originStat = dict[mobType];
        owner = GetComponent<BaseController>();

        MaxHp = originStat._maxHp;
        MaxHp += (MaxHp * 0.1f) * playerLevel;
        _hp = _maxHp;
        _damage = originStat._atk;
        Damage += Mathf.RoundToInt((Damage * 0.1f) * playerLevel);

        _defense = originStat._def;
        _moveSpeed = originStat._moveSpeed;
        _attackRange = originStat._attackRange;
        //_detectRange = stat._detectRange;
        //_exp = stat._exp;
        _useBerserk = originStat._berserk;
        _useSuicideBombing = originStat._suicideBombing;

        _normalAttack = originStat.normalAttack;
        _launchBomb = originStat.launchBomb;
        _missile = originStat.missile;
        _immortalDuration = originStat._immortalDuration;
        _immortalCoolTime = originStat._immortalCoolTime;


        #region skill Entry
        if (originStat._immortal)
        {
            GameObject go = new GameObject() { name = "MonsterImmortal" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterImmortalShield immortal = go.AddComponent<MonsterImmortalShield>();
            immortal.Init(owner, null, null);
        }
        if (originStat._shield)
        {
            GameObject go = new GameObject() { name = "MonsterShield" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterShield shield = go.AddComponent<MonsterShield>();
            shield.Init(owner, null, null);
        }

        if (originStat._normalAttack1 ||
            originStat._normalAttack2 ||
            originStat._normalAttack3 ||
            originStat._normalAttack4)
        {
            GameObject go = new GameObject() { name = "MonsterNormalAttack" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterNormalAttack monsterNormalAttack = go.AddComponent<MonsterNormalAttack>();
            monsterNormalAttack.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterNormalAttack);
        }
        if (originStat._missile)
        {
            GameObject go = new GameObject() { name = "MonsterMissile" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterMissile monsterMissile = go.AddComponent<MonsterMissile>();
            monsterMissile.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterMissile);
        }
        if (originStat._laser)
        {
            GameObject go = new GameObject() { name = "MonsterLaser" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterLaserAttack monsterLaser = go.AddComponent<MonsterLaserAttack>();
            monsterLaser.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterLaser);
        }
        if (originStat._launchBomb)
        {
            GameObject go = new GameObject() { name = "MonsterLaunchBomb" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterLaunchBomb monsterLaunchBomb = go.AddComponent<MonsterLaunchBomb>();
            monsterLaunchBomb.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterLaunchBomb);
        }
        if (originStat._bombing)
        {
            GameObject go = new GameObject() { name = "MonsterBombing" };
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            MonsterBombing monsterBombing = go.AddComponent<MonsterBombing>();
            monsterBombing.Init(owner, null, null);
            owner.AttackSkillDispatcher.Add(0f, monsterBombing);
        }
        #endregion

    }

    public override void OnAttacked(BaseController attacker, float damage)
    {
        if (IsOnImmortal) return;

        float resultDamage = ShieldHp - damage;
        if (resultDamage >= 0f)
        {
            ShieldHp = resultDamage;
            return;
        }
        this.Hp += resultDamage;
        if (_useBerserk && !isOnBerserk)
        {
            isOnBerserk = true;
            Damage *= 2;
            MoveSpeed *= 2;
        }
        if (this.Hp <= 0)
        {
            Hp = 0;
            if (_useSuicideBombing)
            {
                if(Vector3.Distance(this.transform.position, attacker.transform.position)<= 10f)
                {
                    attacker.Stat.OnAttacked(GetComponent<BaseController>(), 100);
                }
            }
            OnDead(attacker);
        }
    }

    protected override void OnDead(BaseController attacker)
    {
        PlayerStat playerStat = attacker.GetComponent<PlayerStat>();
        if(playerStat != null)
        {
            Define.StageType stageType = (Define.StageType)PlayerPrefs.GetInt("SelectedMap");
            StageConfigData stageData = Managers.Data.stageConfigDataDict[stageType];
            GameScene gameScene = Managers.Scene.CurrentScene as GameScene;

            float resultExp = (stageData.waves[gameScene.currentWaveLevel].expPerMonster * playerStat.ExpAssist) * (1f + playerStat.GetComponent<PlayerController>().passiveSkill.skillDict[Define.SkillType.ExpIncrease]);

            Debug.Log($"Get Exp:{resultExp}");

            playerStat.Exp += resultExp;
        }
        Managers.Object.RemoveMonster(GetComponent<MonsterController>());
        Destroy(gameObject);
    }
    public void LevelUp(int playerLevel)
    {
        MaxHp = Managers.Data.monsterStatDict[mobType]._maxHp + (Managers.Data.monsterStatDict[mobType]._maxHp * 0.1f) * playerLevel;
        Hp += (MaxHp * 0.1f) * playerLevel;
        Damage = Managers.Data.monsterStatDict[mobType]._atk + Mathf.RoundToInt((Managers.Data.monsterStatDict[mobType]._atk * 0.1f) * playerLevel);
    }
}
