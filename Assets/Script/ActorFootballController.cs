using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorFootballController : MonoBehaviour
{
    private ActorFootball actor;

    private bool isEnter;

    [SerializeField] private bool isRight;
    
    [SerializeField] private int BallSteppingActionCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<ActorFootball>();
        EventBus.Subscribe<BallSteppingActionDetected>(OnBallSteppingActionDetected);
    }

    private void OnBallSteppingActionDetected(BallSteppingActionDetected obj)
    {
        var upTrigger = actor.GetTrigger(FootBallDetectedType.Up);
        var rightFoot = actor.GetTriggerEnterObject(FootBallDetectedType.Right).CompareTag("RightFoot");
        var leftFoot = actor.GetTriggerEnterObject(FootBallDetectedType.Left).CompareTag("LeftFoot");

        if (upTrigger && !isEnter)
        {
            if (isRight)
            {
                if (rightFoot)
                {
                    BallSteppingActionScore();
                }
                else
                {
                    Debug.Log("Wrong Foot!!!!!");
                }
            }
            else
            {
                if (leftFoot)
                {
                    BallSteppingActionScore();
                }
                else
                {
                    Debug.Log("Wrong Foot!!!!!");
                }
            }
            
            isEnter = true;
        }

        if (!upTrigger) isEnter = false;
    }

    private void BallSteppingActionScore()
    {
        isRight = !isRight;
        BallSteppingActionCount++;
    }

}
