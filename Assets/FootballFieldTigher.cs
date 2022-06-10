using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class FootballFieldTigher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var levelType = obj.currentFootLevels;

        switch (levelType)
        {
            case FootLevels.InsideRightSideOfSeatTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.BallSteppingAction));
                break;
            
            case FootLevels.BallSteppingActionTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.OutsideKick));
                break;
            
            case FootLevels.OutsideKickTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.InsideRightSideOfSeat));
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
