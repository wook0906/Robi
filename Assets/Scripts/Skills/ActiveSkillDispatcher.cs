using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveSkillDispatcher
{
    private float delayPerAttack = 0.5f;
    private float lastFireAttackTime = 0f;

    public class ReservedSkillInfo
    {
        public float coolTime;
        public ActiveSkill skill;

        public ReservedSkillInfo(float coolTime, ActiveSkill skill)
        {
            this.coolTime = coolTime;
            this.skill = skill;
        }
    }

    public class ActiveSkillList
    {
        private List<ReservedSkillInfo> _skills = new List<ReservedSkillInfo>();
        public List<ReservedSkillInfo> Skills { get { return _skills; } }

        public void Add(float coolTime, ActiveSkill skill)
        {
            _skills.Add(new ReservedSkillInfo(coolTime, skill));
            _skills = _skills.OrderBy(x => x.coolTime).ToList();
        }

        public void Remove(ReservedSkillInfo info)
        {
            _skills.Remove(info);
        }
    }

    private BaseController _owner;
    private ActiveSkillList _skillList;

    public void Init(BaseController owner)
    {
        _owner = owner;
        _skillList = new ActiveSkillList();
        lastFireAttackTime = Time.time - delayPerAttack;
    }

    public void Add(float coolTime, ActiveSkill skill)
    {
        _skillList.Add(coolTime, skill);
    }

    public void Update(float deltaTime)
    {
        UpdateCoolTime(deltaTime);

        //if (Time.time - lastFireAttackTime < delayPerAttack)
        //    return;

        if (_owner.gameObject.CompareTag("Enemy") && _owner.State != Define.CreatureState.Attack)
            return;

        foreach (var item in _skillList.Skills)
        {
            
            if (item.coolTime > 0)
                break;
            if (!item.skill.UseSkill())
                continue;

            //Debug.Log("Attack");
            
            _skillList.Remove(item);
            lastFireAttackTime = Time.time;
            break;
        }
    }

    private void UpdateCoolTime(float deltaTime)
    {
        foreach (var item in _skillList.Skills)
        {
            item.coolTime = Mathf.Clamp(item.coolTime - deltaTime, 0, float.MaxValue);
        }
    }
}
