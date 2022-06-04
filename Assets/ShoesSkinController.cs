using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class ShoesSkinController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    void Start()
    {
        EventBus.Subscribe<ChangeSkinDetected>(OnChangeSkinDetected);
    }

    private void OnChangeSkinDetected(ChangeSkinDetected obj)
    {
        var currentMaterial = obj.currentMaterial;

        meshRenderer.material = currentMaterial;
    }
    

}
