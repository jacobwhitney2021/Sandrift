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
            if (!audioSourceBoost.isPlaying)
            {
                audioSourceBoost.loop = true;
                audioSourceBoost.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (audioSourceBoost.isPlaying)
            {
                audioSourceBoost.loop = false;
                audioSourceBoost.Stop();
            }
        }



        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (!audioSourceTurnBoost.isPlaying)
            {
                audioSourceTurnBoost.loop = true;
                audioSourceTurnBoost.Play();
            }
            
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (audioSourceTurnBoost.isPlaying)
            {
                audioSourceTurnBoost.loop = false;
                audioSourceTurnBoost.Stop();
            }
            
        }
    }
}
