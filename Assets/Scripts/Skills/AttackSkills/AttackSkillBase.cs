using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class AttackSkillBase : ActiveSkill
{
    protected SkillType _type;
    public SkillType Type { get { return _type; } }
    [SerializeField]
    protected GameObject _prefab;

    //[SerializeField]
    //protected AttackSkillStat _stat;
    //public AttackSkillStat Stat { get { return _stat; } }
    [SerializeField]
    protected SkillStat _stat;
    public SkillStat Stat { get { return _stat; } set { _stat = value; } }

    virtual public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
        }
    }

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        base.Init(owner, muzzleTransform, parent);
        Stat = gameObject.GetComponent<SkillStat>();
    }

    public override bool UseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}"); 
        return true;
    }

    public virtual void OnFire()
    {

    }
    public virtual void OnHit(GameObject target, Projectile projectile)
    {

    }
    public virtual void OnKill(GameObject target)
    {

    }
    public virtual void OnArrive(Vector3 targetPos, Projectile projectile)
    {

    }

    protected virtual GameObject SearchNoramalStateTarget()
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        GameObject closetTarget = null;
        float closetDist = float.MaxValue;

        foreach (MonsterController target in monsters)
        {
            if (target.State != CreatureState.Chase) continue;

            float dist = (target.transform.position - _owner.transform.position).sqrMagnitude;
            if (dist < closetDist)
            {
                closetTarget = target.gameObject;
                closetDist = dist;
            }
        }

        return closetTarget;
    }

    protected virtual GameObject SearchTarget()
    {
        List<MonsterController> monsters = Managers.Object.Monsters;
        GameObject closetTarget = null;
        float closetDist = float.MaxValue;

        foreach (MonsterController target in monsters)
        {
            float dist = (target.transform.position - _owner.transform.position).sqrMagnitude;
            if (dist < closetDist)
            {
                closetTarget = target.gameObject;
                closetDist = dist;
            }
        }

        return closetTarget;
    }

    protected virtual GameObject[] SearchTargets(int targetCount = 0)
    {
        
        List<MonsterController> monsters = Managers.Object.Monsters;
        if (monsters.Count == 0)
            return null;

        if (targetCount == 0)
            targetCount = monsters.Count;

        List<GameObject> closetTargets = new List<GameObject>();
     
        monsters.Sort((m1, m2) =>
        {
            float dist1 = (m1.transform.position - _owner.transform.position).sqrMagnitude;
            float dist2 = (m2.transform.position - _owner.transform.position).sqrMagnitude;
            return dist1.CompareTo(dist2);
        });

        for (int i = 0; i < targetCount; i++)
        {
            if (i >= monsters.Count)
                break;
            closetTargets.Add(monsters[i].gameObject);
        }
        return closetTargets.ToArray();
    }
    public virtual void LevelUp(Define.SkillGrade grade)
    {

    }

}
