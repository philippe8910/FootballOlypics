using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class SlowMotionSystem : MonoBehaviour
{
    [SerializeField] private float mutiplier , recoverTime;
    public virtual void Start()
    {
        mutiplier = 1;
        EventBus.Subscribe<SlowMotionDetected>(OnSlowMotionDetected);
    }

    public virtual void OnSlowMotionDetected(SlowMotionDetected obj)
    {
        
    }

    private void DefultSlowMotion()
    {
        SetMutiplier(1);
    }

    public void SetMutiplier(float _mutiplier)
    {
        mutiplier = _mutiplier;
    }
}
