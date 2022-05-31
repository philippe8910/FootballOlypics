using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioActor actor;
    void Start()
    {
        actor = GetComponent<AudioActor>();
        
        EventBus.Subscribe<PlaySoundEffectDetected>(OnPlaySoundEffectDetected);   
    }

    private void OnPlaySoundEffectDetected(PlaySoundEffectDetected obj)
    {
        var soundEffect = obj.soundEffect;

        actor.AudioPlay(soundEffect);
    }
}
