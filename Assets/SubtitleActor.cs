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

    public void ResetSubtitleText()
    {
        textMesh.text = "";
    }

    public void SetSubtitleText(string text)
    {
        textMesh.text = text;
    }

    public ScriptableObjectData CompileSubtitleCategory(string category)
    {
        var getSubtitle = (ScriptableObjectData)Resources.Load("Subtitle/" + category);
        
        return getSubtitle;
    }
}
