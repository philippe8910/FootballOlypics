using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class SkinBox : ButtonXR
{
    [SerializeField] private Material _material;

    [SerializeField] private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        ClickAction.AddListener(Trigger);
        EventBus.Subscribe<ChangeSkinDetected>(OnChangeSkinDetected);
    }

    private void OnChangeSkinDetected(ChangeSkinDetected obj)
    {
        animator.Play("BoxIdle");
    }

    private void Trigger()
    {
        EventBus.Post(new ChangeSkinDetected(_material));
        animator.Play("BoxOpen");
    }
}
