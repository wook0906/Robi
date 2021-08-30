using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class NormalAttack : AttackSkillBase
{
    NormalAttackStat normalAttackStat;
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.PlayerNormal;
        normalAttackStat = gameObject.AddComponent<NormalAttackStat>();
        base.Init(owner, muzzleTransform, parent);
        normalAttackStat.InitSkillStat(_type);
        _prefab = Resources.Load<GameObject>("Prefabs/Projectiles/NormalAttackProjectile");

        //TODO: 각 스킬의 특성에 따른 추가 로직을 넣어줘야할 듯함...
    }

    public override bool UseSkill()
    {
        _owner.State = Define.CreatureState.Attack;
        //Debug.Log($"Target:{target.name}");
        GameObject target = SearchTarget();

        if (target == null)
            return false;

        StartCoroutine(Fire(target.GetComponent<BaseController>().CenterPosition));
        
        return true;
    }
    IEnumerator Fire(Vector3 targetPos)
    {
        PlayerStat playerStat = _owner.Stat as PlayerStat;
        for (int i = 0; i < Stat.NumOfProjectilePerBurst; i++)
        {
            
            GameObject projectileGO = GameObject.Instantiate(_prefab, _parent);
            projectileGO.transform.rotation = Quaternion.identity;
            if (_muzzleTransform == null)
                projectileGO.transform.position = _owner.GetComponent<PlayerController>().launchPoints[(int)Define.LaunchPointType.RH].transform.position;
            else
                projectileGO.transform.position = _muzzleTransform.position;
            

            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.Init(_owner, targetPos, Stat.Damage * playerStat.AtkCoefficient, Stat.AttackRange,
                Stat.Speed, Stat.IsExplode, Stat.ExplosionRange, Stat.ExplosionDamage,
                Stat.IsPenetrate, Stat.Duration, LayerMask.NameToLayer("Enemy"));

            projectile.OnHit -= OnHit;
            projectile.OnHit += OnHit;
            projectile.OnKill -= OnKill;
            projectile.OnKill += OnKill;
            yield return new WaitForSeconds(Stat.DelayPerAttack);
        }
        OnFire();
    }

    public override void OnFire()
    {
        //Debug.Log("Fire");
        _owner.State = CreatureState.Idle;
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }

    public override void OnHit(GameObject target, Projectile projectile)
    {
        if (target == null)
            return;

        //Debug.Log($"Hit target:{target.name}");
    }

    public override void OnKill(GameObject target)
    {
        if (target == null)
            return;

        //Debug.Log($"Kill target:{target.name}");
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        normalAttackStat.LevelUp(grade);
    }
}
