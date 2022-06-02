using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActor : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();

    [SerializeField] private List<AudioSource> narrationSources = new List<AudioSource>();

    //0.Loss
    //1.Kick

    public AudioSource ComplieAudioSource(SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case SoundEffect.Loss:
                return audioSources[0];

            case SoundEffect.Kick:
                return audioSources[1];
            
            case SoundEffect.Narration_0:
                return narrationSources[0];
            
            case SoundEffect.Narration_1:
                return narrationSources[1];
            
            case SoundEffect.Narration_2:
                return narrationSources[2];

            default:
                return null;
        }
    }

    public void AudioPlay(SoundEffect soundEffect)
    {
        var audio = ComplieAudioSource(soundEffect);
        audio.Play();
    }
    
    public void AudioPlay(SoundEffect soundEffect , Vector3 playPosition)
    {
        var insAudio = new GameObject();
        var insAudioSource = insAudio.AddComponent<AudioSource>();
        
        insAudioSource.clip = ComplieAudioSource(soundEffect).clip;
        insAudioSource.loop = false;
        insAudioSource.playOnAwake = true;

        var insAudioObject = Instantiate(insAudio, playPosition, Quaternion.identity);
        
        Destroy(insAudioObject , 1);
    }
    
}
