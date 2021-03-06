using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DroneSkill : AttackSkillBase
{
    int numOfDrone = 0;
    Vector3[] dronePos = new Vector3[6];

    DroneSkillStat droneSkillStat;

    List<DroneController> drones;

    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = SkillType.Drone;
        droneSkillStat = gameObject.AddComponent<DroneSkillStat>();
        base.Init(owner, muzzleTransform, parent);
        droneSkillStat.InitSkillStat(_type);
        drones = new List<DroneController>();
        _prefab = Managers.Resource.Load<GameObject>("Prefabs/Drone");
        dronePos[0] = new Vector3(-1.25f, 2, -4);
        dronePos[1] = new Vector3(1.25f, 2, -4);
        dronePos[2] = new Vector3(-2.5f, 0, -4);
        dronePos[3] = new Vector3(2.5f, 0, -4);
        dronePos[4] = new Vector3(-1.25f, -2, -4);
        dronePos[5] = new Vector3(1.25f, -2, -4);
    }

    public override bool UseSkill()
    {
        foreach (var item in drones)
            item.SetStat(droneSkillStat);

        DroneController drone = Instantiate(_prefab).GetComponent<DroneController>();
        drones.Add(drone);
        drone.Init(_owner.transform, dronePos[numOfDrone], droneSkillStat);
        numOfDrone++;

        return true;
    }


    public override void OnFire()
    {
        //Debug.Log("Fire");
        _owner.AttackSkillDispatcher.Add(Stat.CoolTime, this);
    }
    public override void LevelUp(Define.SkillGrade grade)
    {
        droneSkillStat.LevelUp(grade);
    }
}
