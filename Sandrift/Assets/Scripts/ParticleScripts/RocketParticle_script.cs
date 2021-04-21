using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParticle_script : MonoBehaviour
{
    private GameObject craft_gameobject;
    private Vector3 rocketForce;
    private ParticleSystem rocket_particles;

    // Start is called before the first frame update
    void Start()
    {
        craft_gameobject = GameObject.FindGameObjectWithTag("Player");
        rocket_particles = GameObject.FindGameObjectWithTag("RocketParticle").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rocketForce = craft_gameobject.GetComponent<rocketForce_script>().rocketForce;

        if (rocketForce.magnitude>0f)
        {
        	rocket_particles.Play();
        }
        else
        {
        	rocket_particles.Stop();
        }
    }
}
