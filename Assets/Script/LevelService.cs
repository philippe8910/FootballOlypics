using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelService : MonoBehaviour
{
    public FootLevels currentFootLevels = FootLevels.Defult;

    public virtual void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var currentLevel = obj.currentFootLevels;
        currentFootLevels = currentLevel;

        OnChangeLevelTrigger();
    }

    public virtual void Update()
    {
        if (currentFootLevels == FootLevels.BallSteppingAction)
        {
            EventBus.Post(new BallSteppingActionDetected(20, FootLevels.InsideRightSideOfSeatTutorial));
        }

        if (currentFootLevels == FootLevels.InsideRightSideOfSeat)
        {
            EventBus.Post(new InsideRightSideOfSeatDetected(20,FootLevels.OutsideKickTutorial));
        }

        if (currentFootLevels == FootLevels.OutsideKick)
        {
            EventBus.Post(new OutsideKickActionDetected(20,FootLevels.FreeKickTimeTutorial));
        }
        
        if (currentFootLevels == FootLevels.FreeKickTime)
        {
            EventBus.Post(new FreeKickTimeDetected());
        }
        
    }

    public virtual void OnChangeLevelTrigger()
    {
        
    }

    [Button]
    public void ChangeAction()
    {
        EventBus.Post(new ChangeLevelDetected(currentFootLevels));
    }
}
