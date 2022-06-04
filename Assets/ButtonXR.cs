using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class ButtonXR : MonoBehaviour
{
    public UnityEvent ClickAction = new UnityEvent();

    public void OnClick()
    {
        ClickAction.Invoke();
    }
}
