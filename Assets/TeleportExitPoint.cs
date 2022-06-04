using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class TeleportExitPoint : MonoBehaviour
{
    private TeleportPointXR teleportPointXR;
    void Start()
    {
        teleportPointXR = GetComponent<TeleportPointXR>();
        teleportPointXR.OnTeleportEnd.AddListener(OnTeleportEnd);
    }

    private void OnTeleportEnd()
    {
        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Narration_2));
        EventBus.Post(new SubtitleDetected("OnTeleportExitPoint"));
    }
}
