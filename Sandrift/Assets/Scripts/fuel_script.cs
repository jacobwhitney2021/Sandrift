using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel_script : MonoBehaviour
{
    public float fuelMeter;
    public float nitroMeter;
    public float fuelDrainRate;
    public float nitroDrainRate;

    public float nitroCooldownTime;
    private float nitroCooldownTimer;

    public float playerThrust;
    public bool nitroOn;


    // Start is called before the first frame update
    void Start()
    {
		fuelMeter = 1f;
		nitroMeter = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        playerThrust = gameObject.GetComponent<rocketForce_script>().playerThrust;
        nitroOn = gameObject.GetComponent<rocketForce_script>().nitroOn;

        if (playerThrust > 0)
        {
        	if ((fuelMeter - fuelDrainRate*playerThrust*Time.deltaTime) < 0)
        	{
        		fuelMeter = 0;
        	}
        	else
        	{
        		fuelMeter -= fuelDrainRate*playerThrust*Time.deltaTime;
        	}
        } 


        if (nitroOn)
        {
        	if ((nitroMeter - nitroDrainRate*Time.deltaTime) < 0)
        	{
        		nitroMeter = 0;
        	}
        	else
        	{
        		nitroMeter -= nitroDrainRate*Time.deltaTime;
        	}
        }

        if (nitroMeter == 0)
        {
			nitroCooldownTimer += Time.deltaTime;

	        // Check if we have reached beyond our cooldown time.
	        // Subtracting is more accurate over time than resetting to zero.
	        if (nitroCooldownTimer > nitroCooldownTime)
	        {
	            // Remove the recorded cooldown time and resets the nitro
	            nitroCooldownTimer = nitroCooldownTimer - nitroCooldownTime;
	            nitroMeter = 1;
	        }
        }

        
    }

}
