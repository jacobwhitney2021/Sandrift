using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailSounds : MonoBehaviour
{
    public AudioSource audioSourceSailDown;
    public AudioSource audioSourceTrimSail;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!audioSourceSailDown.isPlaying)
            {
                audioSourceSailDown.loop = true;
                audioSourceSailDown.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (audioSourceSailDown.isPlaying)
            {
                audioSourceSailDown.loop = false;
                audioSourceSailDown.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!audioSourceSailDown.isPlaying)
            {
                audioSourceSailDown.loop = true;
                audioSourceSailDown.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (audioSourceSailDown.isPlaying)
            {
                audioSourceSailDown.loop = false;
                audioSourceSailDown.Stop();
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!audioSourceTrimSail.isPlaying)
            {
                audioSourceTrimSail.loop = true;
                audioSourceTrimSail.Play();
            }

        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (audioSourceTrimSail.isPlaying)
            {
                audioSourceTrimSail.loop = false;
                audioSourceTrimSail.Stop();
            }

        }
    }
}
