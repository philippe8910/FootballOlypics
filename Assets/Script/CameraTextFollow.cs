using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTextFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTextPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTextPos.position , 0.1f);
        transform.LookAt(new Vector3(Camera.main.transform.position.x , transform.position.y , Camera.main.transform.position.z));
    }
}
