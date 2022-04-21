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
    
    private int BallSteppingActionCount = 0;

    [SerializeField] private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<ActorFootball>();
        EventBus.Subscribe<BallSteppingActionDetected>(OnBallSteppingActionDetected);
        EventBus.Subscribe<InsideRightSideOfSeatDetected>(OnInsideRightSideOfSeatDetected);
    }

    private void OnInsideRightSideOfSeatDetected(InsideRightSideOfSeatDetected obj)
    {
        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.RightFoot);
        var leftTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.LeftFoot);

        if (rightAreaTrigger && !isEnter)
        {
            if (isRight)
            {
                if (rightTrigger)
                {
                    InsideRightSideOfSeatScoreAction();
                }
                else
                {
                    InsideRightSideOfSeatPunishmentsAction();
                }
            }
            isEnter = true;
        }

        if (leftAreaTrigger && !isEnter)
        {
            if (!isRight)
            {
                if (leftTrigger)
                {
                    InsideRightSideOfSeatScoreAction();
                }
                else
                {
                    InsideRightSideOfSeatPunishmentsAction();
                }
            }
        }

        if (!leftAreaTrigger && !rightAreaTrigger) isEnter = false;
    }
    
    private void OnBallSteppingActionDetected(BallSteppingActionDetected obj)
    {
        var upTrigger = actor.GetAreaTrigger(FootBallAreaType.UpArea);
        var rightTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.RightFoot);
        var leftTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.LeftFoot);

        if (upTrigger && !isEnter)
        {
            if (isRight)
            {
                if (rightTrigger)
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
                if (leftTrigger)
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
        EventBus.Post(new BallSteppingScoreDetedted(BallSteppingActionCount));
    }

    private void BallSteppingActionInputWrongFoot()
    {
        Debug.LogWarning("Wrong Foot");
    }
    
    private void InsideRightSideOfSeatPunishmentsAction()
    {
        Debug.LogWarning("Wrong Foot");
    }

    private void InsideRightSideOfSeatScoreAction()
    {
        isRight = !isRight;
        Debug.Log("Score");
    }

}
