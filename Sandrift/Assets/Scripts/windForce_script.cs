using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windForce_script : MonoBehaviour
{
    public Vector3 windVector;
    public float sailAngle;
    public float sailFullness;

    private Rigidbody rigidbody;

    public Vector3 sailForce;

    void Start()
    {
        sailAngle = 0.0f;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        sailAngle = gameObject.GetComponent<playerController_script>().sailAngle;
        sailFullness = gameObject.GetComponent<playerController_script>().sailFullness;
        sailForce = calculateSailForce(windVector, rigidbody.velocity, sailAngle, sailFullness); // do i have to do time delta time for this????? or leave it 
        rigidbody.AddForce(sailForce);
    }

    Vector3 calculateSailForce(Vector3 windVector, Vector3 playerVelocity, float sailAngle, float sailFullness)
    {
    	Vector3 apparentWindVector = windVector - playerVelocity;
		float windAngle = Vector3.Angle(new Vector3(0,0,1), apparentWindVector); // angle from global forward. need to make sure sailAngle is defined in a similar way (i.e polar coordinates)
		float sailWindAngle = Mathf.Abs(sailAngle - windAngle);
		// sailforcevecotr hsould not be based of wind vector but should be sailvector modified by angle between windvector and sail angle, further modified by difference between windspeed and craft speed (zero force if speeds are the same)
    	Vector3 sailForceVector;
    	if ((sailWindAngle <= 90f) || (sailWindAngle >= 270f)) { // this is not working, seems to always be true   || (sailWindAngle >= (3/2)*Mathf.PI)
    		sailForceVector = transform.forward * apparentWindVector.magnitude * Mathf.Cos(sailWindAngle*(Mathf.PI/180f)) * sailFullness; // maybe make square of wind speed
    	}
    	else {
    		sailForceVector = Vector3.zero;
    	}
    	return sailForceVector;
    }

    // Also create function to apply some kind of roll rotation if component of the wind vector is pushing into the sail/craft side on
}
