using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class LevelService : MonoBehaviour
{
    [SerializeField] private FootLevels footLevels = FootLevels.Defult;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (footLevels == FootLevels.BallSteppingAction)
        {
            EventBus.Post(new BallSteppingActionDetected());
        }

        if (footLevels == FootLevels.InsideRightSideOfSeat)
        {
            EventBus.Post(new InsideRightSideOfSeatDetected());
        }
        
    }
}
