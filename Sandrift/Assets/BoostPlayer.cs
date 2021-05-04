using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlayer : MonoBehaviour
{
    public AudioSource audioSourceBoost;
    public AudioSource audioSourceTurnBoost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            audioSourceBoost.Play();
            audioSourceBoost.loop = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSourceBoost.Stop();
            audioSourceBoost.loop = false;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {

            audioSourceTurnBoost.Play();
            audioSourceTurnBoost.loop = true;
        }

        if (Input.GetKeyUp(KeyCode.A) & Input.GetKeyUp(KeyCode.D))
        {
            audioSourceTurnBoost.Stop();
            audioSourceTurnBoost.loop = false;
        }
    }
}
