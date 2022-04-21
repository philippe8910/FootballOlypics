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

    [SerializeField] private bool isRightSide;

    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<ActorFootball>();
        EventBus.Subscribe<BallSteppingActionDetected>(OnBallSteppingActionDetected);
        EventBus.Subscribe<InsideRightSideOfSeatDetected>(OnInsideRightSideOfSeatDetected);
    }
        
    //TODO
    
    private void OnInsideRightSideOfSeatDetected(InsideRightSideOfSeatDetected obj)
    {
        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.RightFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.LeftFoot);

        if (rightAreaTrigger && !isEnter)
        {
            if (isRightSide)
            {
                if (rightFootTrigger)
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
            if (!isRightSide)
            {
                if (leftFootTrigger)
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
    
    //TODO 
    
    private void OnBallSteppingActionDetected(BallSteppingActionDetected obj)
    {
        var upAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.UpArea);
        
        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.RightFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.LeftFoot);

        if (upAreaTrigger && !isEnter)
        {
            if (isRightSide)
            {
                if (rightFootTrigger)
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
                if (leftFootTrigger)
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

        if (!upAreaTrigger) isEnter = false;
    }

    private void BallSteppingActionScore()
    {
        isRightSide = !isRightSide; 
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
        isRightSide = !isRightSide;
        Debug.Log("Score");
    }

}
