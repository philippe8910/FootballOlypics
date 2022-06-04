using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
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

    private void OnTeleportEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("soccer");
    }
}
