using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpotLightScript : MonoBehaviour
{
    public float onTime;
    public float offTime;
    private float onTimer;
    private float offTimer;

    // Start is called before the first frame update
    void Start()
    {
        // start off
        gameObject.GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Light>().enabled == false)
        {
        	offTimer += Time.deltaTime;

        	if (offTimer >= offTime)
        	{
        		offTimer -= offTimer;
        		gameObject.GetComponent<Light>().enabled = true;
        	}
        } 
        
        if (gameObject.GetComponent<Light>().enabled == true)
        {
        	onTimer += Time.deltaTime;

        	if (onTimer >= onTime)
        	{
        		onTimer -= onTimer;
        		gameObject.GetComponent<Light>().enabled = false;
        	}
        } 
    }
}
