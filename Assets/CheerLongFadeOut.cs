using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerLongFadeOut : MonoBehaviour
{
    private AudioSource audio;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void StartFade()
    {
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeOut()
    {
        audio.volume -= 0.01f;

        if(audio.volume > 0)
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(StartFadeOut());
        }
        else
        {
            yield return null;
        }
    }
}
