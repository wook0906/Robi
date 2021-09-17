using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterShield : AttackSkillBase
{
    protected GameObject effect;
    protected ParticleSystem particle;
    protected ParticleSystem.MainModule settings;

    [SerializeField]
    float shieldHp = 0;
    public float ShieldHp 
    { 
        get { return shieldHp; }
        set 
        { 
            shieldHp = value;
            if(shieldHp <= 0)
            {
                effect.SetActive(false);
            }
            else
            {
                effect.SetActive(true);
            }
        }
    }
    
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.MonsterShield;
        Stat = gameObject.AddComponent<SkillStat>();
        base.Init(owner, muzzleTransform, parent);
        Stat.InitSkillStat(_type);
        effect = Managers.Resource.Instantiate("Effects/ShieldEffect");
        effect.transform.SetParent(owner.transform);
        effect.transform.position = _owner.CenterPosition;
        effect.transform.localScale /= 2f;
        particle = effect.GetComponent<ParticleSystem>();
        settings = effect.GetComponent<ParticleSystem>().main;

        settings.startColor = new Color(0f, 0.5764706f, 1f, 0.4901961f);

        UseSkill();
    }

    public override bool UseSkill()
    {
        shieldHp = _owner.Stat.Hp / 2f;
        return true;
    }


    
}
