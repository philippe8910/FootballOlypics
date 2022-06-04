using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event.SubtitleEvent;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChangeEffectSystem : MonoBehaviour
{
    private Material currentMaterial;
    
    void Start()
    {
        currentMaterial = GetComponent<MeshRenderer>().material;
    }
    
}
