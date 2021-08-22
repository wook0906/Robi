using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    float killTime = 0f;
    ParticleSystem particleSystem;

    public void Init(float time = 0f, float scaleValue = 1f)
    {
        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            item.localScale *= scaleValue;
        } 
        if (time == 0) return;
        killTime = time;

        Invoke("DelayedDestroy", time);
    }
    void DelayedDestroy()
    {
        Managers.Resource.Destroy(this.gameObject);
    }
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        //if (useCustomTime)
        //{

        //}

        if (!particleSystem.IsAlive(true))
        {
            Managers.Resource.Destroy(this.gameObject);
        }
    }
    public void ImmediatlyDestroy()
    {
        Managers.Resource.Destroy(this.gameObject);
    }
}
