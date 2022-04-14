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
        actor.SetFootballKinematic(false);
        
        var upTrigger = actor.GetTrigger(FootBallDirectionDetectedType.Up);
        var rightFoot = actor.GetTriggerEnterObject(FootBallDirectionDetectedType.Up).CompareTag("RightFoot");
        var leftFoot = actor.GetTriggerEnterObject(FootBallDirectionDetectedType.Up).CompareTag("LeftFoot");

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
                    BallSteppingActionInputWrongFoot();
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
                    BallSteppingActionInputWrongFoot();
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

    private void BallSteppingActionInputWrongFoot()
    {
        Debug.LogWarning("Wrong Foot");
    }

}
