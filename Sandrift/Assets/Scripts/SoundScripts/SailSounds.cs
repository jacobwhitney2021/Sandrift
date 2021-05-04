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
                StartCoroutine(FadeAudioSource.StartFade(audioSourceSailDown, 1f, 1f));
                audioSourceSailDown.loop = true;
                audioSourceSailDown.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (audioSourceSailDown.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceSailDown, 1f, 0f));
                audioSourceSailDown.loop = false;
                audioSourceSailDown.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!audioSourceSailDown.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceSailDown, 1f, 1f));
                audioSourceSailDown.loop = true;
                audioSourceSailDown.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (audioSourceSailDown.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceSailDown, 1f, 0f));
                audioSourceSailDown.loop = false;
                audioSourceSailDown.Stop();
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            StartCoroutine(FadeAudioSource.StartFade(audioSourceTrimSail, 1f, .167f));
            audioSourceTrimSail.loop = true;
            audioSourceTrimSail.Play();


        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (audioSourceTrimSail.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceTrimSail, .7f, 0f));
                audioSourceTrimSail.loop = false;

            }

        }
    }
}
