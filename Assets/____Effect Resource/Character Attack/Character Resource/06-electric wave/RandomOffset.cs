using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOffset : MonoBehaviour
{
    Material mat;
    public List<Vector2> offset;
    public float offsetChangeInterval = 0.1f;

    float timer;
    int idx = 0;
    int Idx
    {
        get{ return idx; }
        set
        {
            if (value >= offset.Count)
            {
                //idx = 0;
                GetComponent<ParticleAutoDestroy>().ImmediatlyDestroy();
            }
            else
                idx = value;
        }
    }

    void Start()
    {
        mat = GetComponent<LineRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > offsetChangeInterval)
        {
            mat.mainTextureOffset = offset[Idx];
            timer = Time.time;
            Idx++;
        }
    }
}
