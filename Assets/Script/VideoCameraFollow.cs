using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform mainCamera;
    void Start()
    {
        //Debug.Log("Fuck");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.position.x , mainCamera.position.y , targetPos.position.z) , 0.1f);
        transform.LookAt(mainCamera.transform.position);
    }
}
