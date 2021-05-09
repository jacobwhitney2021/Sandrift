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
        if (nitroMeter<1 && nitroMeter>0)
        {
            nitroOn = true;
        }
        if (nitroMeter==0) nitroOn = false;
        processInputs();
    }

    void FixedUpdate()
    {
        rocketForce = calculateRocketForce(rocketThrust); 
    }

    void processInputs()
    {
        thrustChange = rocketInputAxis * rocketDecelerationRate * Time.deltaTime;

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
            rocketThrust = playerThrust;
        }
    }

    Vector3 calculateRocketForce(float rocketThrust)
    {
        Vector3 rocketForce = transform.forward * rocketThrust;
        return rocketForce;
    }

}
