using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project;
using Project.Event;
using Script.Event;

public class FootballFieldLevelSystem : LevelService
{
    public override void Update()
    {
        if (currentFootLevels == FootLevels.BallSteppingAction)
        {
            EventBus.Post(new BallSteppingActionDetected(11, FootLevels.InsideRightSideOfSeatTutorial));
        }

        if (currentFootLevels == FootLevels.InsideRightSideOfSeat)
        {
            EventBus.Post(new InsideRightSideOfSeatDetected(7, FootLevels.OutsideKickTutorial));
        }

        if (currentFootLevels == FootLevels.OutsideKick)
        {
            EventBus.Post(new OutsideKickActionDetected(9, FootLevels.FreeKickTimeTutorial));
        }

        if (currentFootLevels == FootLevels.FreeKickTime)
        {
            EventBus.Post(new FreeKickTimeDetected());
        }
    }
}
