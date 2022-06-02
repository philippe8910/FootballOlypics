using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        
        EventBus.Subscribe<StartGameDetected>(OnStartGameDetected);
    }

    private void OnStartGameDetected(StartGameDetected obj)
    {
        animator.Play("MainMenuFadeOut");
    }

}
