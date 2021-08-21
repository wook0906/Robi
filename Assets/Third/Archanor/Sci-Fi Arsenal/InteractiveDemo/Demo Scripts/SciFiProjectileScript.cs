using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SciFiArsenal
{
    public class SciFiProjectileScript : MonoBehaviour
    {
        public GameObject[] impactParticles;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;
        public GameObject[] trailParticles;

        Stack<GameObject> instances;
        [HideInInspector]
        public Vector3 impactNormal; //Used to rotate impactparticle.

        private bool hasCollided = false;

        void Start()
        {
            instances = new Stack<GameObject>();
            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
                muzzleParticle.transform.rotation = transform.rotation * Quaternion.Euler(180, 0, 0);
                Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            }
        }

        void OnCollisionEnter(Collision hit)
        {
            if (!hasCollided)
            {
                hasCollided = true;
                foreach (var item in impactParticles)
                {
                    instances.Push(Instantiate(item, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject);
                }

                if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
                {
                    Destroy(hit.gameObject);
                }

                foreach (GameObject trail in trailParticles)
                {
                    GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                    curTrail.transform.parent = null;
                    Destroy(curTrail, 3f);
                }
                Destroy(projectileParticle, 3f);

                foreach (var item in instances)
                {
                    Destroy(item,5f);
                }
                //Destroy(impactParticles, 5f);
                Destroy(gameObject);

                ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
                //Component at [0] is that of the parent i.e. this object (if there is any)
                for (int i = 1; i < trails.Length; i++)
                {

                    ParticleSystem trail = trails[i];

                    if (trail.gameObject.name.Contains("Trail"))
                    {
                        trail.transform.SetParent(null);
                        Destroy(trail.gameObject, 2f);
                    }
                }
            }
        }
    }
}