using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumePingPong : MonoBehaviour
{

    public AudioSource MenuWind;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        MenuWind.volume = Mathf.PingPong(Time.time/6, .5f) + .5f;
        MenuWind.pitch = Mathf.PingPong(Time.time / 10, .25f) + .75f;
    }
}
