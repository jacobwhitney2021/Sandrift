using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideThruster_script : MonoBehaviour
{
    public GameObject craft_gameobject;
    public float rudderTorque;
    public ParticleSystem leftThrust;
    public ParticleSystem rightThrust;

    // Start is called before the first frame update
    void Start()
    {
        craft_gameobject = GameObject.FindGameObjectWithTag("Player");
        leftThrust = GameObject.FindGameObjectWithTag("LeftThruster").GetComponent<ParticleSystem>();
        rightThrust = GameObject.FindGameObjectWithTag("RightThruster").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	rudderTorque = craft_gameobject.GetComponent<playerController_script>().rudderInputAxis;
    	
        if (rudderTorque>0)
        {
            leftThrust.Play();
            rightThrust.Stop();
        }
        else if (rudderTorque<0)
        {
            leftThrust.Stop();
            rightThrust.Play();
        }
        else
        {
        	leftThrust.Stop();
            rightThrust.Stop();
        }

    }
}
