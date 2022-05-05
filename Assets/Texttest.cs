using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class Texttest : MonoBehaviour
{
    private void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var levelType = obj.currentFootLevels;
        if (levelType == FootLevels.BallSteppingActionTutorial)
        {
            GetComponent<Animator>().Play("Level01");
        }
        
        if (levelType == FootLevels.InsideRightSideOfSeatTutorial)
        {
            GetComponent<Animator>().Play("Level02");
        }
        
        if (levelType == FootLevels.OutsideKickTutorial)
        {
            GetComponent<Animator>().Play("Level03");
        }
        
    }

    public void SetText(string text)
    {
        GetComponent<TextMesh>().text = text;
    }

    public void FirstLevelTeach()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingActionTutorial));
    }

    public void FirstLevel()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingAction));

    }
    
    public void SecondLevelTeach()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeatTutorial));
    }

    public void SecondLevel()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeat));

    }
    
    public void ThirdLevelTeach()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.OutsideKickTutorial));
    }

    public void ThirdLevel()
    {
        EventBus.Post(new ChangeLevelDetected(FootLevels.OutsideKick));

    }
}
