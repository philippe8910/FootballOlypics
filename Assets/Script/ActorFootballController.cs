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
        var upTrigger = actor.GetAreaTrigger(FootBallAreaType.UpArea);
        var upEnterFoot = actor.GetTriggerEnterObject(FootBallAreaType.UpArea);

        if (upTrigger && !isEnter)
        {
            if (isRight)
            {
                if (upEnterFoot.CompareTag("RightFoot"))
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
                if (upEnterFoot.CompareTag("LeftFoot"))
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
