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
                StartCoroutine(FadeAudioSource.StartFade(audioSourceBoost, 1f, .7f));
                audioSourceBoost.loop = true;
                audioSourceBoost.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (audioSourceBoost.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceBoost, 1f, 0f));
                audioSourceBoost.loop = false;
                audioSourceBoost.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {

            StartCoroutine(FadeAudioSource.StartFade(audioSourceTurnBoost, 1f, .21f));
            audioSourceTurnBoost.loop = true;
            audioSourceTurnBoost.Play();


        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (audioSourceTurnBoost.isPlaying)
            {
                StartCoroutine(FadeAudioSource.StartFade(audioSourceTurnBoost, .7f, 0f));
                audioSourceTurnBoost.loop = false;

            }

        }
    }
}
