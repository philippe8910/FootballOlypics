﻿using System.Collections;
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
}
