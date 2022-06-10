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
}
