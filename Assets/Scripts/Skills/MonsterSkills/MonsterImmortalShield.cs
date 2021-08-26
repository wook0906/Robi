using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterImmortalShield : AttackSkillBase
{
    protected GameObject effect;
    protected ParticleSystem particle;
    protected ParticleSystem.MainModule settings;
    MonsterStat ownerStat;

    [SerializeField]
    public bool IsImmortal
    {
        get { return ownerStat.IsOnImmortal; }
        set
        {
            ownerStat.IsOnImmortal = value;
            if (!value)
            {
                effect.SetActive(false);
            }
            else
                effect.SetActive(true);
        }
    }

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.MonsterImmortalShield;
        gameObject.AddComponent<SkillStat>().InitSkillStat(_type);
        base.Init(owner, muzzleTransform, parent);
        effect = Managers.Resource.Instantiate("Effects/ShieldEffect");
        effect.transform.SetParent(owner.transform);
        effect.transform.position = _owner.CenterPosition;
        effect.transform.localScale /= 2f;
        
        particle = effect.GetComponent<ParticleSystem>();
        settings = effect.GetComponent<ParticleSystem>().main;
        settings.startColor = new Color(0f, 0.5764706f, 1f, 0.4901961f);

        effect.SetActive(false);
        this.ownerStat = owner.Stat as MonsterStat;
        UseSkill();
    }



    public override bool UseSkill()
    {
        StartCoroutine(ActiveShield());
        return true;
    }
    IEnumerator ActiveShield()
    {
        while (true)
        {
            IsImmortal = true;
            yield return new WaitForSeconds(ownerStat._immortalDuration);
            IsImmortal = false;
            yield return new WaitForSeconds(ownerStat._immortalCoolTime);
        }
    }

    //public override void OnFire()
    //{
    //    //Debug.Log("Fire");
    //    _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    //}

    
}
