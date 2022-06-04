using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FootBall"))
        {
            EventBus.Post(new GoalTriggerDetected());
            EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Narration_1));
            EventBus.Post(new SubtitleDetected("OnLabEnd"));
        }
    }
}
