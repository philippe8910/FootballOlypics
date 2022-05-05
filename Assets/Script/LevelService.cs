using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class LevelService : MonoBehaviour
{
    [SerializeField] private FootLevels currentFootLevels = FootLevels.Defult;

    private void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var currentLevel = obj.currentFootLevels;
        currentFootLevels = currentLevel;
    }

    private void Update()
    {
        if (currentFootLevels == FootLevels.BallSteppingAction)
        {
            EventBus.Post(new BallSteppingActionDetected());
        }

        if (currentFootLevels == FootLevels.InsideRightSideOfSeat)
        {
            EventBus.Post(new InsideRightSideOfSeatDetected());
        }

        if (currentFootLevels == FootLevels.OutsideKick)
        {
            EventBus.Post(new OutsideKickActionDetected());
        }
        
        if (currentFootLevels == FootLevels.FreeKickTime)
        {
            EventBus.Post(new FreeKickTimeDetected());
        }
        
    }
}
