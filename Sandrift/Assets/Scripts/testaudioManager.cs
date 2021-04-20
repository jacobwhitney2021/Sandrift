using UnityEngine.Audio;
using System;
using UnityEngine;

public class testaudioManager : MonoBehaviour
{
    public static testaudioManager instance;


    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.group;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start ()
    {
        if (Input.GetAxis("SailVertical") != 0)
            Play("TrimmingLine");
        Play("SandMovement");
        Play("Menumusic");
    }
    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
