using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRooter : MonoBehaviour
{
    SphereCollider rooterCollider;
    
    PlayerController owner;

  
    public void Init(PlayerController player, float radius)
    {
        rooterCollider = gameObject.GetOrAddComponent<SphereCollider>();
        owner = player;
        rooterCollider.radius = radius;
    }

    public void SetRadius(float radius)
    {
        rooterCollider.radius = radius;
    }
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("itemRooter Trigger!");
    }
}
