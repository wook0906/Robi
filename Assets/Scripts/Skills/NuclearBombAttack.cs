using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class NuclearBombAttack : AttackSkillBase
{
    Image nuclearFadeImage;
    NuclearSkillStat nuclearStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        //type = SkillType.NuclearBomb;
        nuclearStat = gameObject.AddComponent<NuclearSkillStat>(); 
        base.Init(owner, muzzleTransform, parent);
        nuclearStat.InitSkillStat(_type);
        nuclearFadeImage = GameObject.Find("NuclearFade").GetComponent<Image>();
    }

    public override bool UseSkill()
    {
        //Debug.Log("Nuclear Bomb!!");
        StartCoroutine(CorUseSkill());
        return true;
    }
    IEnumerator CorUseSkill()
    {
        Debug.Log($"{_type} Fired. #Info : CoolTime : {Stat.CoolTime}, Damage : {Stat.Damage}, AttackRange : {Stat.AttackRange}, NumOfProjectilePerBurst {Stat.NumOfProjectilePerBurst}, Speed : {Stat.Speed}, IsExplode : {Stat.IsExplode}, ExplosionRange : {Stat.ExplosionRange}, ExplosionDamage : {Stat.ExplosionDamage}, isPernerate : {Stat.IsPenetrate}, Duration : {Stat.Duration}, DelayPerAttack : {Stat.DelayPerAttack}");

        float alpha = 0f;
        while (alpha <= 0.99f)
        {
            alpha = Mathf.LerpUnclamped(alpha, 1f, 2f * Time.deltaTime);
            nuclearFadeImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        alpha = 1f;
        nuclearFadeImage.color = Color.white;

        MonsterController[] monsters = new MonsterController[Managers.Object.Monsters.Count];
        Managers.Object.Monsters.CopyTo(monsters);
        foreach (var monster in monsters)
        {
            monster.GetComponent<CreatureStat>().OnAttacked(_owner, float.MaxValue);
        }
        yield return new WaitForSeconds(1f);

        while (alpha >= 0.01f)
        {
            alpha = Mathf.LerpUnclamped(alpha, 0f, 2f * Time.deltaTime);
            nuclearFadeImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        alpha = 0f;
        nuclearFadeImage.color = Color.clear;

        OnFire();
    }
    public override void OnFire()
    {
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {

    }

    public override void OnKill(GameObject target)
    {

    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        nuclearStat.LevelUp(grade);
    }
}
