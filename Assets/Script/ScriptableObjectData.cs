using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;


[CreateAssetMenu(fileName = "data" , menuName = "data/Test")]
public class ScriptableObjectData : ScriptableObject
{
    public List<string> testList = new List<string>();

    public UnityEvent testEvent = new SteamVR_Events.Event();
}
