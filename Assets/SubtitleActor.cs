using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class SubtitleActor : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;

    private void Start()
    {
        //textMesh = GetComponent<TextMesh>();
    }

    public void SetSubtitleActive(bool isActive)
    {
        textMesh.gameObject.SetActive(isActive);
    }

    public void SetSubtitleText(string text)
    {
        textMesh.text = text;
    }

    public Subtitle CompileSubtitleCategory(string category)
    {
        var getSubtitle = (Subtitle)Resources.Load("Subtitle/" + category);
        
        return getSubtitle;
    }
}
