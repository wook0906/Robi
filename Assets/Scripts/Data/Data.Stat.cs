using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Data
{
    [Serializable]
    public class CreatureStat
    {
        public int level;
        public float maxHp;
        public int damage;
        public float speed;
        public float hpRecoveryPerSecond;
        public int totalExp;
        public float attackRange;
        public float attackInterval;
        public float detectRange;
    }

    [Serializable]
    public class CreatureStatData : ILoader<int, CreatureStat>
    {
        public List<CreatureStat> stats;

        public Dictionary<int, CreatureStat> MakeDict()
        {
            Dictionary<int, CreatureStat> dict = new Dictionary<int, CreatureStat>();
            foreach (CreatureStat stat in stats)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }

    [Serializable]
    public class BaseSkillStat
    {
        public int level;
        public float coolTime; // 0은 패시브 스킬
    }


    [Serializable]
    public class AttackSkillStat : BaseSkillStat
    {
        public int damage;
        public float attackRange; // 0은 단일 타 
        public int useCount; // 한번에 사용 가능한 스킬 횟수 기본값 1
        public int projectileCount;
        public float speed; // 투사체가 날아가는 속도 0이면 바로 타겟에 도착함
        public bool isExplode;
        public float explosionRange;
        public int explosionDamage;
        public bool isPenetrate;
        public float duration;
        public float delayPerAttack; // 데미지가 들어가는 간격 0이면 즉시 한번만 데미지가 들어ㄱ
    }

    [Serializable]
    public class AttackSkillStatData : ILoader<int, AttackSkillStat>
    {
        public List<AttackSkillStat> stats;

        public Dictionary<int, AttackSkillStat> MakeDict()
        {
            Dictionary<int, AttackSkillStat> dict = new Dictionary<int, AttackSkillStat>();
            foreach (AttackSkillStat stat in stats)
            {
                dict.Add(stat.level, stat);
            }
            return dict;
        }
    }
}
