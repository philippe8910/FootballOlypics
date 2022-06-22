using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class TeleportFootballField : MonoBehaviour
{
    private TeleportPointXR teleportPointXR;
    void Start()
    {
        teleportPointXR = GetComponent<TeleportPointXR>();
        teleportPointXR.OnTeleportEnd.AddListener(OnTeleportEnd);
    }

    private async void OnTeleportEnd()
    {
        EventBus.Post(new SubtitleDetected("OnFootFieldTeleportEnd"));
        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.OnFootFieldTeleportEnd));
        EventBus.Post(new ChangeLevelDetected(FootLevels.Defult));
    }
}
