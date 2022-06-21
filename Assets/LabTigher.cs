using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class LabTigher : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        EventBus.Subscribe<StartGameDetected>(OnStartGameDetected);
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var levelType = obj.currentFootLevels;

        switch (levelType)
        {
            case FootLevels.BallSteppingActionTutorial:
                animator.Play("BallSteppingActionTutorial");
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.BallStepTutorial));
                break;
            
            case FootLevels.InsideRightSideOfSeatTutorial:
                animator.Play("InsideRightSideOfSeatTutorial");
                EventBus.Post(new SubtitleDetected("InsideRightSideOfSeatTutorial"));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.InsideKickTutorial));
                break;
            
            case FootLevels.OutsideKickTutorial:
                animator.Play("OutsideKickTutorial");
                EventBus.Post(new SubtitleDetected("OutsideKickTutorial"));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.OutsideKickTutorial));
                break; 
            
            case FootLevels.FreeKickTimeTutorial:
                animator.Play("Begining");
                EventBus.Post(new SubtitleDetected("FreeKickTimeTutorial"));
                EventBus.Post(new PlaySoundEffectDetected(SoundEffect.BallStepTutorial));
                break; 
        }
    }

    private void OnStartGameDetected(StartGameDetected obj)
    {
        animator.Play("AniWavehand");
    }


}
