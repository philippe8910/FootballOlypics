using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


[CreateAssetMenu(fileName = "Subtitledata" , menuName = "SubtitleVerson2/Subtitledata")]
public class ScriptableObjectData : ScriptableObject
{
    public List<string> subtitleList = new List<string>();
    public List<float> subtitleTime = new List<float>();

    public UnityEvent onSubtitleEnd = new UnityEvent();
}
