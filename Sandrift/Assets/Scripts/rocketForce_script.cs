using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketForce_script : MonoBehaviour
{
    public Rigidbody rigidbody;

    public GameObject particleController;

    private float rocketInputAxis;
    public bool nitroOn; // TODO: add cooldown for nitro // TODO cooldown does not finish

    public float thrustChangePerInput;
    public float decelerationRate;
	public float maxThrust;
	public float nitroThrust;
	
	public float rocketThrust;
	
	public Vector3 rocketForce;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        particleController.GetComponent<ParticleController_Script>().SetMaxForce(maxThrust);
    }

    // Update is called once per frame
    void Update()
    {
        rocketInputAxis = gameObject.GetComponent<playerController_script>().rocketInputAxis;
        nitroOn = gameObject.GetComponent<playerController_script>().nitroInput;
        processInputs();
    }

    void FixedUpdate()
    {

        particleController.GetComponent<ParticleController_Script>().SetRocketForce(rocketThrust);
        rocketForce = calculateRocketForce(rocketThrust); 
    }

    void processInputs()
    {
        float thrustPerSec = thrustChangePerInput * Time.deltaTime;
        float decelerationPerSec = decelerationRate * Time.deltaTime;
        if (rocketInputAxis>0)
        {
        	float thrustChange = rocketInputAxis * thrustPerSec;
	        if (!nitroOn && (rocketThrust+thrustChange >= maxThrust)) 
	        {
	        	rocketThrust = maxThrust;
	        }
	        else 
	        {
	        	rocketThrust += thrustChange;
	        }
        }
        else if (rocketThrust>0)
        {
        	if ((rocketThrust-decelerationPerSec)>0) rocketThrust -= decelerationPerSec;
        	else rocketThrust = 0;
        }


        if (nitroOn)
        {
        	rocketThrust = nitroThrust;
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
