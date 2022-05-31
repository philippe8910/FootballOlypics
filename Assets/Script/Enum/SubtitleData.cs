using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubtitleData
{
    public List<string> SubtitleList = new List<string>();
}


[CreateAssetMenu(fileName = "New Subtitle" , menuName = "Subtitle/Create Subtitle Asset" , order = 1)]
public class Subtitle : ScriptableObject
{
    public SubtitleData subtitleData;

}
