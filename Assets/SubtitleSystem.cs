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
    }
    
    [Button]
    public void Test()
    {
        var g = actor.CompileSubtitleCategory("Start");
        StartCoroutine(StartEnterSubtitle(g));
    }

    private void OnSubtitleDetected(SubtitleDetected obj)
    {
        var subtitleCategory = obj.subtitleCategory;
        
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
            Debug.LogError("Subtitle List count is not same");
            yield return null;
        }

        actor.SetSubtitleActive(true);

        for (int i = 0 ; i < subtitleStringLength ; i++)
        {
            actor.SetSubtitleText(subtitleList[i]);
            
            yield return new WaitForSeconds(subtitleTimeList[i]);
            
        }
        
        onSubtitleEnd.Invoke();
        
        actor.SetSubtitleActive(false);
    }
}
