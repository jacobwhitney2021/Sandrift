﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class windForce_script : MonoBehaviour
{
    public Vector3 windVector;
    public float sailAngle;
    public float sailFullness;

    public Rigidbody rigidbody;

    public Vector3 sailForce;
    public Vector3 rocketForce;

    public int rocketThrust; // between 0 and 50
    public bool nitroOn; // TODO: add cooldown for nitro // TODO cooldown does not finish
    public const int nitroThrust = 150;

    public const int MAXIMUM_WIND = 30;
    public const int MAXIMUM_WIND_CHANGE = 2;

    public Vector3 windGoal;

    public const int MAX_FUEL = 1000;
    private int fuel;

    private System.Random rnd;

    void Start()
    {
        sailAngle = 0.0f;
        rigidbody = gameObject.GetComponent<Rigidbody>();

        rocketThrust = 10;
        nitroOn = false;
        fuel = MAX_FUEL;

        windVector = new Vector3(0, 0, 0);
        windGoal = new Vector3(0, 0, 0);

        rnd = new System.Random();

    }

    void FixedUpdate()
    {
        sailAngle = gameObject.GetComponent<playerController_script>().sailAngle;
        sailFullness = gameObject.GetComponent<playerController_script>().sailFullness;
        sailForce = calculateSailForce(windVector, rigidbody.velocity, sailAngle, sailFullness); // do i have to do time delta time for this????? or leave it 

        fixedUpdateRocket();

        // rocket stuff here
    }

    void createWind()
    {
        if (windGoal == windVector)
        {
            windGoal.x = rnd.Next(-1 * MAXIMUM_WIND, MAXIMUM_WIND);
            windGoal.z = rnd.Next(-1 * MAXIMUM_WIND, MAXIMUM_WIND);
        } else {
            windVector.x = applyWindOnAxis(windGoal.x, windVector.x);
            windVector.z = applyWindOnAxis(windGoal.z, windVector.z);

        }
    }

    float applyWindOnAxis(float goal, float curr)
    {
        float diff = goal - curr;
        float change = rnd.Next(MAXIMUM_WIND_CHANGE);

        if (change > System.Math.Abs(diff))
        {
            return goal;
        }
        else if (diff > 0)
        {
            return curr + change;
        }
        else
        {
            return curr - change;
        }
    }

    void addTotalForce()
    { 
        // TODO calculate total forces
        rigidbody.AddForce(sailForce + rocketForce); 
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

    void fixedUpdateRocket()
    {
        consumeFuel();
        calculateRocketForce();

    }

    void calculateRocketForce()
    {
        int force = nitroOn ? nitroThrust : rocketThrust;
        rocketForce = transform.forward * force;
    }

    void consumeFuel()
    {
        int fuelConsumed = nitroOn ? (int)(nitroThrust * 1.5f) : rocketThrust; // TODO do we want the fuel function to be linear or not
        fuel -= fuelConsumed;

    }
}
