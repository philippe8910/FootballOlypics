using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubtitleData
{
    public List<string> subtitleList = new List<string>();
    public List<float> subtitleTime = new List<float>();
}


[CreateAssetMenu(fileName = "New Subtitle" , menuName = "Subtitle/Create Subtitle Asset" , order = 1)]
public class Subtitle : ScriptableObject
{
    public SubtitleData subtitleData;

}
