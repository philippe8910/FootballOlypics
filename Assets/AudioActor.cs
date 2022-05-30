using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActor : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();

    //0.Loss
    //1.Kick

    public void AudioPlay(SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case SoundEffect.Loss:
                audioSources[0].Play();
                break;
            
            case SoundEffect.Kick:
                audioSources[1].Play();
                break;
        }
    }
    
}
