using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketForce_script : MonoBehaviour
{
    public Rigidbody rigidbody;

    private float rocketInputAxis;
    public bool nitroOn;

    public float rocketAccelerationRate;
    public float rocketDecelerationRate;
    public float nitroDecelerationRate;
    public float maxThrust;
    public float nitroThrust;

    public float nitroMeter;

    public float thrustChange;
    public float playerThrust;
	public float rocketThrust;
	
	public Vector3 rocketForce;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rocketInputAxis = gameObject.GetComponent<playerController_script>().rocketInputAxis;
        nitroOn = gameObject.GetComponent<playerController_script>().nitroInput;
        nitroMeter = gameObject.GetComponent<fuel_script>().nitroMeter;
        if (nitroMeter==0) nitroOn = false;
        processInputs();
    }

    void FixedUpdate()
    {
        rocketForce = calculateRocketForce(rocketThrust); 
    }

    void processInputs()
    {
        // float thrustChange = rocketInputAxis * sailPivotSpeed * Time.deltaTime;
        // float decelerationPerSec = decelerationRate * Time.deltaTime;
        // if (rocketInputAxis>0)
        // {
        // 	float thrustChange = rocketInputAxis * thrustPerSec;
	       //  if (!nitroOn && (rocketThrust+thrustChange >= maxThrust)) 
	       //  {
	       //  	rocketThrust = maxThrust;
	       //  }
	       //  else 
	       //  {
	       //  	rocketThrust += thrustChange;
	       //  }
        // }
        // else if (rocketThrust>0)
        // {
        // 	if ((rocketThrust-decelerationPerSec)>0) rocketThrust -= decelerationPerSec;
        // 	else rocketThrust = 0;
        // }



        if (rocketInputAxis>=0)
        {
            thrustChange = rocketInputAxis * rocketAccelerationRate * Time.deltaTime;
        }
        else
        {
            thrustChange = rocketInputAxis * rocketDecelerationRate * Time.deltaTime;
        }


        if (nitroOn)
        {
            rocketThrust = nitroThrust;
        }
        else if (rocketThrust>playerThrust)
        {
            rocketThrust -= nitroDecelerationRate * Time.deltaTime;
        }
        else
        {
            if (playerThrust+thrustChange >= maxThrust) 
            {
                playerThrust = maxThrust;
            }
            else if (playerThrust+thrustChange <= 0)
            {
                playerThrust = 0;
            }
            else
            {
                playerThrust += thrustChange;
            }
            rocketThrust = playerThrust;
        }


    }

    Vector3 calculateRocketForce(float rocketThrust)
    {
        Vector3 rocketForce = transform.forward * rocketThrust;
        return rocketForce;
    }

    // void consumeFuel()
    // {
    //     int fuelConsumed = nitroOn ? (int)(nitroThrust * 1.5f) : rocketThrust; // TODO do we want the fuel function to be linear or not
    //     fuel -= fuelConsumed;
    // }
}
