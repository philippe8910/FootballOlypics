using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using Sirenix.OdinInspector;
using UnityEngine;

public class SubtitleSystem : MonoBehaviour
{
    private SubtitleActor actor;

    [SerializeField] [Range(0.1f , 3)] private float subtitleRunTime;
    
    void Start()
    {
        actor = GetComponent<SubtitleActor>();
        
        EventBus.Subscribe<SubtitleDetected>(OnSubtitleDetected);
        EventBus.Subscribe<StartGameDetected>(OnStartGameDetected);
    }

    private void OnStartGameDetected(StartGameDetected obj)
    {
        EventBus.Post(new SubtitleDetected("Start"));
        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Narration_0));
    }

    [Button]
    public void Test()
    {
        var g = actor.CompileSubtitleCategory("Start");
        EventBus.Post(new PlaySoundEffectDetected(SoundEffect.Narration_0));
        StartCoroutine(StartEnterSubtitle(g));
    }

    private void OnSubtitleDetected(SubtitleDetected obj)
    {
        var subtitleCategory = obj.subtitleCategory;

        Debug.Log("Fuck");
        var g = actor.CompileSubtitleCategory(subtitleCategory);
        StartCoroutine(StartEnterSubtitle(g));
    }

    private IEnumerator StartEnterSubtitle(Subtitle subtitle)
    {
        var subtitleStringLength = subtitle.subtitleData.subtitleList.Count;
        var subtitleTimeLength = subtitle.subtitleData.subtitleTime.Count;
        var subtitleList = subtitle.subtitleData.subtitleList;
        var subtitleTimeList = subtitle.subtitleData.subtitleTime;
        var onSubtitleEnd = subtitle.subtitleData.onSubtitleEnd;

        if (subtitleStringLength != subtitleTimeLength)
        {
            Debug.LogError( subtitle + "Subtitle List count is not same");
            yield return null;
        }

        for (int i = 0 ; i < subtitleStringLength ; i++)
        {
            actor.SetSubtitleText(subtitleList[i]);
            
            yield return new WaitForSeconds(subtitleTimeList[i]);
            
        }
        
        actor.ResetSubtitleText();
        
        onSubtitleEnd.Invoke();
        
        
    }
}
