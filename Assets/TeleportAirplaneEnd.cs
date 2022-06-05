using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project;
using Project.Event;
using Project.Event.SubtitleEvent;
using UnityEngine;
using UnityEngine.Events;

public class TeleportAirplaneEnd : MonoBehaviour
{
    private TeleportPointXR teleportPointXR;
    void Start()
    {
        teleportPointXR = GetComponent<TeleportPointXR>();
        teleportPointXR.OnTeleportEnd.AddListener(OnTeleportEnd);
    }

    private async void OnTeleportEnd()
    {
        EventBus.Post(new ChangeSceneEffectDetected());
        
        await Task.Delay(3000);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
}
