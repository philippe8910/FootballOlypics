using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class FootballFieldTigher : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);

        animator = GetComponent<Animator>();
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var levelType = obj.currentFootLevels;

        switch (levelType)
        {
            case FootLevels.FreeKickTimeTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.FreeKickTime));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.FinalKick));
                EventBus.Post(new SubtitleDetected("FinalKick"));
                break;

            case FootLevels.BallSteppingActionTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.Pause));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.FinalKick));
                EventBus.Post(new SubtitleDetected("FinalKick"));
                break;

            case FootLevels.OutsideKickTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.Pause));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.FinalKick));
                EventBus.Post(new SubtitleDetected("FinalKick"));
                break;

            case FootLevels.InsideRightSideOfSeatTutorial:
                EventBus.Post(new ChangeLevelDetected(FootLevels.Pause));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.FinalKick));
                EventBus.Post(new SubtitleDetected("FinalKick"));
                break;
        }

        

        if (levelType == FootLevels.Defult)
        {
            animator?.Play("AniCrossarm");
        }
    }

}
