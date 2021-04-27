using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParticle_script : MonoBehaviour
{
    private GameObject craft_gameobject;
    private float maxThrust;
    private float rocketThrust;
    private ParticleSystem rocket_particles;
    private ParticleSystem nitro_particles;

    // Start is called before the first frame update
    void Start()
    {
        craft_gameobject = GameObject.FindGameObjectWithTag("Player");
        rocket_particles = GameObject.FindGameObjectWithTag("RocketParticle").GetComponent<ParticleSystem>();
        nitro_particles = GameObject.FindGameObjectWithTag("NitroParticle").GetComponent<ParticleSystem>();
        maxThrust = craft_gameobject.GetComponent<rocketForce_script>().maxThrust;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rocketThrust = craft_gameobject.GetComponent<rocketForce_script>().rocketThrust;

        if (rocketThrust>maxThrust)
        {
            nitro_particles.Play();
        }
        else
        {
            nitro_particles.Stop();
        }

        if ((rocketThrust>0f) && (rocketThrust<= maxThrust))
        {
        	rocket_particles.Play();

        }
        else
        {
        	rocket_particles.Stop();
        }

        var rocketEmission = rocket_particles.emission;
        rocketEmission.rateOverTime = Mathf.Log(rocketThrust+2)*25;
        rocket_particles.startLifetime = rocketThrust/90;

    }
}
