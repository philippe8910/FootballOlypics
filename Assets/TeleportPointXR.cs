using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointXR : MonoBehaviour
{
    private Material currentMaterial;
    
    [SerializeField] private Color currentColor , pointColor = Color.red;

    private bool isPoint;
    
    private void Start()
    {
        currentMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    private void Update()
    {
        if (isPoint)
        {
            currentMaterial.SetColor("_TintColor" , pointColor);
        }
        else
        {
            currentMaterial.SetColor("_TintColor" , currentColor);
        }

        isPoint = false;
    }

    public void SetAtive()
    {
        isPoint = true;
    }

}
