using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuneContactParticle_script : MonoBehaviour
{
    public GameObject craft_gameobject;
    public Rigidbody rigidbody;
	public GameObject[] particles;

	public float craft_speed;
	public float threshold;


    // Start is called before the first frame update
    void Start()
    {
        craft_gameobject = GameObject.FindGameObjectWithTag("Player");
        rigidbody = craft_gameobject.GetComponent<Rigidbody>();
        particles = GameObject.FindGameObjectsWithTag("DuneContactParticle");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	craft_speed = rigidbody.velocity.magnitude;
        if (craft_speed<threshold)
        {
        	foreach (GameObject particle in particles)
	        {
	            particle.GetComponent<ParticleSystem>().Stop();
	        }
        }
        else
        {
        	foreach (GameObject particle in particles)
	        {
	            particle.GetComponent<ParticleSystem>().Play();
	        }
        }
    }
}
