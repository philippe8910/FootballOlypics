using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class LabEventList : MonoBehaviour
{
    public void OnBeginningEnd()
    {
        EventBus.Post(new SubtitleDetected("StepBallTutorial"));
        EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingActionTutorial));
    }

    public void OnBallSteppingActionTutorialEnd()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingAction));
        
        GameObject.Find("Scoreboard").GetComponent<Animator>().Play("FadIn");
    }

    public void OnInsideRightSideOfSeatTutorialEnd()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeat));
    }

    public void OnOutsideKickTutorialEnd()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.OutsideKick));
    }

    public void OnFreeKickTimeTutorialEnd()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.FreeKickTime));
    }

    public void OnFootballFieldIntroduceEnd()
    {
        var g = Random.Range(0 , 3);

        switch (g)
        {
            case 0:
                EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingAction)); 
                break;
            
            case 1:
                EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeat)); 
                break;
            
            case 2:
                EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeat)); 
                break;
        }

        Debug.Log(g);

        GameObject.Find("Scoreboard").GetComponent<Animator>().Play("FootFieldFadIn");
    }

    public void OnFinishFootField()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.FreeKickTime));
    }
}
