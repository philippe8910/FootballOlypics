using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event.SubtitleEvent;
using UnityEngine;
using UnityEngine.UI;

public class ChangeEffect : MonoBehaviour
{
    [SerializeField] float alpha;
    [SerializeField] float fadeSpeed;
    [SerializeField] Image changeEffectImage;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        changeEffectImage = GetComponent<Image>();
        
        animator.Play("FadeOut");
        
        EventBus.Subscribe<ChangeSceneEffectDetected>(OnChangeSceneEffectDetected);
        //GetComponent<Image>().color = new Color(0, 0, 0, 0);

        //StartCoroutine(StartFadeToBlack());
    }

    private void OnChangeSceneEffectDetected(ChangeSceneEffectDetected obj)
    {
        animator.Play("FadeIn");
    }

    IEnumerator StartFadeToBlack()
    {
        alpha = 0;
        while (alpha <= 1)
        {
            alpha += fadeSpeed * Time.deltaTime;

            changeEffectImage.color = new Color(0, 0, 0, alpha);

            yield return new WaitForFixedUpdate();
        }

        //這裡切換場景

        StartCoroutine(StartFadeToTransparent());
    }

    IEnumerator StartFadeToTransparent()
    {
        alpha = 1;

        while (alpha >= 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;

            changeEffectImage.color = new Color(0, 0, 0, alpha);

            yield return new WaitForFixedUpdate();
        }

        //這裡轉場結束
    }


}
