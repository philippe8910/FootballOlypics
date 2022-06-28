using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class GoalAreaFootField : MonoBehaviour
{
    [SerializeField] private GameObject trophy;

    [SerializeField] private GameObject finishEffect;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FootBall"))
        {
            EventBus.Post(new PlaySoundEffectDetected(SoundEffect.CheerShort));
            trophy.SetActive(true);
            finishEffect.SetActive(true);
        }
        
    }
}
