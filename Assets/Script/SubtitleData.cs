using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SubtitleData
{
    public List<string> subtitleList = new List<string>();
    public List<float> subtitleTime = new List<float>();

    public UnityEvent onSubtitleEnd = new UnityEvent();
}


[CreateAssetMenu(fileName = "New Subtitle" , menuName = "Subtitle/Create Subtitle Asset")]
public class Subtitle : ScriptableObject
{
    public SubtitleData subtitleData;
}
