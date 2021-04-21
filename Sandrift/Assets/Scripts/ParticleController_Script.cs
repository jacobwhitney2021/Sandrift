using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController_Script : MonoBehaviour
{
    private float rocketThrust;
    private float sailForce;
    public const float simulationSpeed = 2;
    private float maxThrust;

    private GameObject[] particles;
    // Start is called before the first frame update
    void Start()
    {
        rocketThrust = 0;
        sailForce = 0;
        particles = GameObject.FindGameObjectsWithTag("Particle");
        maxThrust = 0;
    }

    void FixedUpdate()
    {
        float force = rocketThrust / maxThrust;
        foreach (GameObject particle in particles)
        {
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            var main = ps.main;
            main.simulationSpeed = simulationSpeed; //* force;
        }
    }

}
