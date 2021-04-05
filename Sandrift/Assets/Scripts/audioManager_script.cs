using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager_script : MonoBehaviour
{
	public AudioClip creaking_clip;
	public AudioClip sailLuffing_clip;
	public AudioClip lightWind1_clip;
	public AudioClip lightWind2_clip;
	public AudioClip lightWind3_clip;
	public AudioClip heavyWind1_clip;
	AudioSource creaking_audio;
	AudioSource sailLuffing_audio;
    AudioSource lightWind2_audio;
    AudioSource heavyWind1_audio;

	private GameObject playerObject;
	Vector3 windVector;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
		windVector = playerObject.GetComponent<windForce_script>().windVector;

        creaking_audio = AddAudio(creaking_clip, true, true, 1.0f);
        sailLuffing_audio = AddAudio(sailLuffing_clip, true, true, 1.0f);
        lightWind2_audio = AddAudio(lightWind2_clip, true, true, 7.5f);
        heavyWind1_audio = AddAudio(heavyWind1_clip, false, false, 1.0f);
        
        creaking_audio.Play();
        sailLuffing_audio.Play();
        lightWind2_audio.Play();
        //heavyWind1_audio.Play();
    }


    void Update()
    {
    	playWindBasedAudio();
    }


	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) 
	{ 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	void playWindBasedAudio() 
	{
		// AudioSource currentWindAudio = null;

		// if (windVector.magnitude>0.0f) 
		// {
		// 	if (windVector.magnitude<=0.0f)
		// 	{
		// 		currentWindAudio = lightWind2_audio;
		// 	}
			

		// 	if (!sailLuffing_audio.isPlaying)
		// 	{
		// 		sailLuffing_audio.Play();
		// 	}
		// }
		
		// if (windVector.magnitude>10.0f) 
		// {
		// 	currentWindAudio = lightWind2_audio;
		// }
		
		// if (!currentWindAudio.isPlaying)
		// {
		// 	currentWindAudio.Play();
		// }
		
	}

}
