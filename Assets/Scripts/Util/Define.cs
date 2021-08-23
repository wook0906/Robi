using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Monsters
    {
        F01,
        C01,
        MAX,
    }
    public enum MonsterAttackType
    {
        Short,
        Long
    }
    public enum MapType
    {
        map1,
        map2,
        map3,
        map4,
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
    }
    public enum MonsterSkillType
    {
        
    }
    public enum AttackSkillType
    {
        PlayerNormal,
        FlameThrower,
        ImpactWave,
        PlayerLaser,
        Missile,
        Napalm,
        PulseWave,
        NuclearBomb,
        Bombing,
        Shield,
        Drone,
        BlackHole,
        PlayerMax,

        MonsterNormal,
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
