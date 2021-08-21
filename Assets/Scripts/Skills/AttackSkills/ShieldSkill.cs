﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ShieldSkill : AttackSkillBase
{
    GameObject effect;

    int shieldLevel = 0;
    public int ShieldLevel
    {
        get { return shieldLevel; }
        set
        {
            if (!(value <= 5 && value >= 0)) return;
            if (effect)
            {
                ParticleSystem particle = effect.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule settings = effect.GetComponent<ParticleSystem>().main;
                switch (value)
                {
                    case 0:
                        settings.startColor = new Color(0f, 0f, 0f, 0f);
                        break;
                    case 1:
                        settings.startColor = new Color(0f, 0.5764706f, 1f, 0.4901961f);
                        break;
                    case 2:
                        settings.startColor = new Color(0f, 1f, 0.07112217f, 0.4901961f);
                        break;
                    case 3:
                        settings.startColor = new Color(0.5962632f, 0f, 1f, 0.4901961f);
                        break;
                    case 4:
                        settings.startColor = new Color(1f, 0.495748f, 0f, 0.4901961f);
                        break;
                    case 5:
                        settings.startColor = new Color(1f, 0f, 0f, 0.4901961f);
                        break;
                    default:
                        break;
                }
                shieldLevel = value;
            }
        }
    }
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.Shield;
        base.Init(owner, muzzleTransform, parent);
        effect = Managers.Resource.Instantiate("Effects/ShieldEffect");
        effect.transform.SetParent(owner.transform);
        effect.transform.localPosition = Vector3.zero;
    }

    public override bool UseSkill()
    {
        Debug.Log("Use Shield");
        ShieldLevel++;
        OnFire();
        return true;
    }

    public override void OnFire()
    {
        //Debug.Log("Fire");
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }

    
}
