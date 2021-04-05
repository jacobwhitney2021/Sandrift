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
	// AudioSource creaking_source;

    void Start()
    {
        AudioSource creaking_source = AddAudio(creaking_clip, true, true, 1.0f);
        creaking_source.Play ();
    }


    void Update()
    {
        
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


}
