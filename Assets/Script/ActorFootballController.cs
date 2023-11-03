using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using Script.Event;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorFootballController : MonoBehaviour
{
    private ActorFootball actor;

    private bool isEnter;

    private bool isStay;

    private bool isFirstEnter;

    private Transform playerTransform;
    
    [SerializeField] private bool isRightSide;
    
    [SerializeField] private int ballSteppingActionCount = 0;

    [SerializeField] private float maxDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        actor = GetComponent<ActorFootball>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
        
        EventBus.Subscribe<BallSteppingActionDetected>(OnBallSteppingActionDetected);
        EventBus.Subscribe<InsideRightSideOfSeatDetected>(OnInsideRightSideOfSeatDetected);
        EventBus.Subscribe<OutsideKickActionDetected>(OnOutsideKickActionDetected);
        EventBus.Subscribe<FreeKickTimeDetected>(OnFreeKickTimeDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        isFirstEnter = false;
        actor.ResetPosition();

        if(obj.currentFootLevels == FootLevels.Pause)
        {
            actor.SetKinematic(true);
        }
    }

    //TODO

    private void OnFreeKickTimeDetected(FreeKickTimeDetected obj)
    {
        var allAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.AllArea);

        actor.SetKinematic(false);
        actor.SetConstranit(RigidbodyConstraints.None);
        
        //ActorPositionReset();

        //print("Distance : " + Vector3.Distance((GameObject.FindWithTag("Player").transform.position),transform.position));
        
        if (allAreaTrigger && !isEnter)
        {
            var allAreaEnterObject = actor.GetTriggerEnterObject(FootBallAreaType.AllArea);
            var footBallFlyDir = actor.GetCollisionVector(allAreaEnterObject.transform.position);
            
            print("footBallFlyDir" + footBallFlyDir);

            actor.Kick(footBallFlyDir, 13);
            
            PlayKickAudio(); //踢球聲音

            EventBus.Post(new KickBallDetected());

            isEnter = true;
        }

        if (!allAreaTrigger) isEnter = false;
    }

    //TODO

    private void OnOutsideKickActionDetected(OutsideKickActionDetected obj)
    {
        var ballSteppingActionCountData = obj.ballSteppingActionCountData;
        var nextFootLevels = obj.nextFootLevels;

        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.LeftFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.RightFoot);
        
        if(isFirstEnter) actor.MoveAction(isRightSide);
        
        actor.SetKinematic(false);
        
       // ActorPositionReset();

        if (rightAreaTrigger && !isEnter)
        {
            isFirstEnter = true;
            
            if (isRightSide)
            {
                if (rightFootTrigger)
                {
                    OutsideKickScoreAction(ballSteppingActionCountData, nextFootLevels);
                }
                else
                {
                    OutsideKickPunishmentsAction();
                }
            }
            
            PlayKickAudio(); //踢球聲音

            isEnter = true;
        }

        if (leftAreaTrigger && !isEnter)
        {
            isFirstEnter = true;

            if (!isRightSide)
            {
                if (leftFootTrigger)
                {
                    OutsideKickScoreAction(ballSteppingActionCountData, nextFootLevels);
                }
                else
                {
                    OutsideKickPunishmentsAction();
                }
            }
            
            PlayKickAudio(); //踢球聲音
            
            isEnter = true;

        }

        if (!leftAreaTrigger && !rightAreaTrigger) isEnter = false;
    }

    //TODO
    
    private void OnInsideRightSideOfSeatDetected(InsideRightSideOfSeatDetected obj)
    {
        var ballSteppingActionCountData = obj.ballSteppingActionCountData;
        var nextFootLevels = obj.nextFootLevels;

        var rightAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.RightArea);
        var leftAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.LeftArea);

        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.RightArea, FootType.RightFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.LeftArea, FootType.LeftFoot);

        if(isFirstEnter) actor.MoveAction(isRightSide);
        

        actor.SetKinematic(false);

        //ActorPositionReset();

        if (rightAreaTrigger && !isEnter)
        {
            isFirstEnter = true;
            
            if (isRightSide)
            {
                if (rightFootTrigger)
                {
                    InsideRightSideOfSeatScoreAction(ballSteppingActionCountData, nextFootLevels);
                }
                else
                {
                    InsideRightSideOfSeatPunishmentsAction();
                }
            }
            
            PlayKickAudio(); //踢球聲音

            isEnter = true;
        }

        if (leftAreaTrigger && !isEnter)
        {
            isFirstEnter = true;
            
            if (!isRightSide)
            {
                if (leftFootTrigger)
                {
                    InsideRightSideOfSeatScoreAction(ballSteppingActionCountData, nextFootLevels);
                }
                else
                {
                    InsideRightSideOfSeatPunishmentsAction();
                }
            }
            
            PlayKickAudio(); //踢球聲音
            
            isEnter = true;

        }

        if (!leftAreaTrigger && !rightAreaTrigger) isEnter = false;
    }
    
    //TODO 
    
    private void OnBallSteppingActionDetected(BallSteppingActionDetected obj)
    {
        var ballSteppingActionCountData = obj.ballSteppingActionCountData;
        var nextFootLevels = obj.nextFootLevels;

        var upAreaTrigger = actor.GetAreaTrigger(FootBallAreaType.UpArea);
        
        var rightFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.RightFoot);
        var leftFootTrigger = actor.GetTriggerEnterObject(FootBallAreaType.UpArea , FootType.LeftFoot);

        actor.SetKinematic(true);
        
        //ActorPositionReset();
        
        if (upAreaTrigger && !isEnter)
        {
            if (isRightSide)
            {
                if (rightFootTrigger)
                {
                    BallSteppingActionScore(ballSteppingActionCountData, nextFootLevels);
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
                    BallSteppingActionScore(ballSteppingActionCountData, nextFootLevels);
                }
                else
                {
                    BallSteppingActionInputWrongFoot();
                }
            }

            PlayKickAudio(); //踢球聲音
            KickMoment();

            isEnter = true;
        }
        
        

        if (!upAreaTrigger) isEnter = false;
    }

    private void BallSteppingActionScore(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
    {
        isRightSide = !isRightSide;

        if (ballSteppingActionCount >= _ballSteppingActionCountData)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(_nextFootLevels));
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

    private void InsideRightSideOfSeatScoreAction(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
    {
        isRightSide = !isRightSide;
        
        if (ballSteppingActionCount >= _ballSteppingActionCountData)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(_nextFootLevels));
        }
        else
        {
            actor.SetSlowVelocity(0.3f , 2f);
            
            ballSteppingActionCount++;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
        }
    }

    private void OutsideKickScoreAction(int _ballSteppingActionCountData, FootLevels _nextFootLevels)
    {
        isRightSide = !isRightSide;
        
        if (ballSteppingActionCount >= _ballSteppingActionCountData)
        {
            ballSteppingActionCount = 0;
            EventBus.Post(new BallSteppingScoreDetedted(ballSteppingActionCount));
            EventBus.Post(new ChangeLevelDetected(_nextFootLevels));
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

    private void PlayKickAudio()
    {
        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Kick));
    }

    [Button]
    public void ResetBall()
    {
        actor.ResetPosition();
    }

    //TODO

    private void OnTriggerEnterArea(Collider collider)
    {
        if(collider.CompareTag("ResetArea"))
        {
            actor.ResetPosition();
            EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Loss));
            isFirstEnter = false;
        }
    }

    private void OnTriggerStayArea()
    {
        
    }

    private void OnTriggerExitArea()
    {
        
    }

    void KickMoment()
    {
        var allAreaEnterObject = actor.GetTriggerEnterObject(FootBallAreaType.AllArea);

        EventBus.Post(new ParticleActionDetected(allAreaEnterObject.transform.position , ParticleType.KickEffect_Azer));
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterArea(other);
    }

    //private void ActorPositionReset()
    //{
    //    if (Vector3.Distance(playerTransform.position, transform.position) >= maxDistance)
    //    {
    //        print(Vector3.Distance(playerTransform.position, transform.position));
    //        actor.ResetPosition();
    //        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Loss));
    //        isFirstEnter = false;
    //    }

    //}



}
