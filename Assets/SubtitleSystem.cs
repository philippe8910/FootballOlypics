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
        var subtitleLength = subtitle.subtitleData.SubtitleList.Count;
        var subtitleList = subtitle.subtitleData.SubtitleList;
        
        actor.SetSubtitleActive(true);

        for (int i = 0 ; i < subtitleLength ; i++)
        {
            actor.SetSubtitleText(subtitleList[i]);
            
            yield return new WaitForSeconds(subtitleRunTime);
            
        }
        
        actor.SetSubtitleActive(false);
    }
}
