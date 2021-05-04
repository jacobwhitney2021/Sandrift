using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSounds : MonoBehaviour
{

    public AudioSource HeavyWindSound;
    public GameObject sail;

    private windForce_script windForces;

    public float windMag;

    // Start is called before the first frame update
    private void Start()
    {
        sail = GameObject.Find("sail");
        windForces = sail.GetComponent<windForce_script>();
        
    }

    // Update is called once per frame
    void Update()
    {
        windMag = windForces.magnitude;
        Debug.Log(windMag);
    }
}
