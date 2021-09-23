using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum SkillGrade
    {
        Common,
        Rare,
        Unique,
        Max
    }

    public enum CharacterType
    {
        Robi,
        Toug,
        Warri,
        Seshu,
        Thief,
        MAX,
    }
    public enum MonsterType
    {
        C01,
        C02,
        C03,
        C04,
        C05,
        C06,
        C07,

        F01,
        F02,
        F03,
        F04,
        F05,
        F06,
        F07,
        
        EC01,
        EC02,
        EC03,
        EC04,
        EC05,
        EC06,
        EC07,

        EF01,
        EF02,
        EF03,
        EF04,
        EF05,
        EF06,
        EF07,

        MAX,
    }
    public enum MonsterAttackType
    {
        Short,
        Long
    }
    public enum StageType
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        MaxCount
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum LaunchPointType
    {
        LH,
        RH,
        RS,
        LS,
        Head,
        Waist,
        Forward,
    }
    public enum ProjectileType
    {
        Normal,
        Penetrate,
        Explosion
    }

    public enum CreatureState
    {
        Idle,
        Move,
        Chase,
        Attack,
        ComebackHome,
        Knockback,
        Dragged,
        Controlled,
    }
    public enum SkillType
    {
        PlayerNormal,
        FlameThrower,
        ImpactWave,
        PlayerLaser,
        PlayerMissile,
        Napalm,
        PulseWave,
        //NuclearBomb,
        PlayerBombing,
        PlayerShield,
        Drone,
        BlackHole,
        PlayerActiveSkillMax,

        ATKIncrease,
        MaxHPIncrease,
        DEFIncrease,
        MoveSpeedIncrease,
        ProjectileIncrease,
        ExpIncrease,
        CoolTimeIncrease,
        RootRangeIncrease,
        HPRecoveryAssistIncrease,
        EnemyHpDownIncrease,
        HPRecoveryAmountIncrease,
        DurationIncrease,
        RangeIncrease,
        HPRecoveryPerSecIncrease,
        PlayerPassiveSkillMax,

        MonsterNormal1,
        MonsterNormal2,
        MonsterNormal3,
        MonsterNormal4,
        MonsterLaser,
        MonsterLaser2,
        MonsterLaunchBomb,
        MonsterBombing,
        MonsterMissile,
        MonsterShield,
        MonsterImmortalShield,
        MonsterMax,
    }

    public enum Scene
    {
        UnKnown,
        TitleScene,
        LobbyScene,
        GameScene,
    }

}
