using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DroneSkill : AttackSkillBase
{
    int numOfDrone = 0;
    Vector3[] dronePos = new Vector3[6];
    
    public override void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _type = AttackSkillType.Drone;
        base.Init(owner, muzzleTransform, parent);
        _prefab = Managers.Resource.Load<GameObject>("Prefabs/Drone");
        dronePos[0] = new Vector3(1, 1, 0);
        dronePos[1] = new Vector3(-1, 1, 0);
        dronePos[2] = new Vector3(1, -1, 0);
        dronePos[3] = new Vector3(-1, -1, 0);
        dronePos[4] = new Vector3(2, 0, 0);
        dronePos[5] = new Vector3(-2, 0, 0);
    }

    public override bool UseSkill()
    {
        DroneController drone = Instantiate(_prefab).GetComponent<DroneController>();
        drone.Init(_owner.transform, dronePos[numOfDrone], Stat);
        numOfDrone++;

        return true;
    }

    public override void OnFire()
    {
        //Debug.Log("Fire");
        _owner.ActiveSkillDispatcher.Add(Stat.CoolTime, this);
    }
}
