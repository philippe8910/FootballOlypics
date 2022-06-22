using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project;
using Project.Event;
using System;
using System.Threading.Tasks;

public class Defender : MonoBehaviour
{
    private Animator animator;

    public GameObject footBall;
   
    void Start()
    {
        Debug.Log("sss");


        animator = GetComponent<Animator>();
        EventBus.Subscribe<FreeKickTimeDetected>(OnFreeKickTimeDetected);
    }

    private void OnFreeKickTimeDetected(FreeKickTimeDetected obj)
    {
        if(Vector3.Distance(transform.position, footBall.transform.position) <= 2.5f)
        {
            if (footBall.transform.position.x > 0)
            {
                animator.Play("test_dae01_Left");
            }
            else
            {
                animator.Play("test_dae01_Right");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
