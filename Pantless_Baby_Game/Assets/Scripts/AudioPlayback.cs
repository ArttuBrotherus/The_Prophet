using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayback : MonoBehaviour
{

    AudioSource audSrc;

    // Start is called before the first frame update
    void Start()
    {
        audSrc = GetComponent<AudioSource>();
        audSrc.mute = true;
    }

    public void oneSound(AudioClip theClip, float vol)
    {
        audSrc.mute = false;
        audSrc.PlayOneShot(theClip, vol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
