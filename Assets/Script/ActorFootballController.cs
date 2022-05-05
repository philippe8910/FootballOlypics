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

    private bool isStay;
    
    [SerializeField] private bool isRightSide;
    
    [SerializeField] private int ballSteppingActionCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<ActorFootball>();
        
        EventBus.Subscribe<BallSteppingActionDetected>(OnBallSteppingActionDetected);
        EventBus.Subscribe<InsideRightSideOfSeatDetected>(OnInsideRightSideOfSeatDetected);
        EventBus.Subscribe<OutsideKickActionDetected>(OnOutsideKickActionDetected);
        EventBus.Subscribe<FreeKickTimeDetected>(OnFreeKickTimeDetected);
        EventBus.Subscribe<SlowMotionDetected>(OnSlowMotionDetected);
    }

    private void OnSlowMotionDetected(SlowMotionDetected obj)
    {
        
    }

    //TODO

    private void OnFreeKickTimeDetected(FreeKickTimeDetected obj)
    {
        var allAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.AllArea);

        actor.SetKinematic(false);
        actor.SetConstranit(RigidbodyConstraints.None);

        if (allAreaTrigger && !isEnter)
        {
            var allAreaEnterObject = actor.GetTriggerEnterObject(FootBallAreaType.AllArea);
            var footBallFlyDir = actor.GetCollisionVector(allAreaEnterObject.transform.position);
            
            print("footBallFlyDir" + footBallFlyDir);

            actor.Kick(footBallFlyDir, 10);
            isEnter = true;
        }

        if (!allAreaTrigger) isEnter = false;
    }

    //TODO

    private void OnOutsideKickActionDetected(OutsideKickActionDetected obj)
    {
        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.LeftFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.RightFoot);
        
        actor.MoveAction(isRightSide);
        
        actor.SetKinematic(false);

        if (rightAreaTrigger && !isEnter)
        {
            if (isRightSide)
            {
                if (rightFootTrigger)
                {
                    OutsideKickScoreAction();
                }
                else
                {
                    OutsideKickPunishmentsAction();
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
                    OutsideKickScoreAction();
                }
                else
                {
                    OutsideKickPunishmentsAction();
                }
            }
        }

        if (!leftAreaTrigger && !rightAreaTrigger) isEnter = false;
    }

    //TODO
    
    private void OnInsideRightSideOfSeatDetected(InsideRightSideOfSeatDetected obj)
    {
        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.RightFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.LeftFoot);

        actor.MoveAction(isRightSide);
        

        actor.SetKinematic(false);

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

        actor.SetKinematic(true);
        
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

        if (ballSteppingActionCount >= 20)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeatTutorial));
        }
        else
        {
            ballSteppingActionCount++;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
        }
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
        
        if (ballSteppingActionCount >= 20)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(FootLevels.OutsideKickTutorial));
        }
        else
        {
            actor.SetSlowVelocity(0.3f , 2f);
            
            ballSteppingActionCount++;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
        }
    }

    private void OutsideKickScoreAction()
    {
        isRightSide = !isRightSide;
        
        if (ballSteppingActionCount >= 20)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(FootLevels.FreeKickTimeTutorial));
        }
        else
        {
            actor.SetSlowVelocity(0.3f , 2f);
            
            ballSteppingActionCount++;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
        }
    }
    
    private void OutsideKickPunishmentsAction()
    {
        
    }
    
    //TODO

    private void OnTriggerEnterArea()
    {
        
    }

    private void OnTriggerStayArea()
    {
        
    }

    private void OnTriggerExitArea()
    {
        
    }

}
