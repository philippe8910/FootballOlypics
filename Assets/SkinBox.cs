using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class SkinBox : ButtonXR
{
    [SerializeField] private Material _material;
    private void Start()
    {
        ClickAction.AddListener(Trigger);
    }

    private void Trigger()
    {
        EventBus.Post(new ChangeSkinDetected(_material));
    }
}
