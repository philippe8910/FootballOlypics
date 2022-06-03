using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;

public class LabLevelSystem : MonoBehaviour
{
    [SerializeField] private GameObject airplane;

    [SerializeField] private GameObject exitAirplane;
    void Start()
    {
        EventBus.Subscribe<GoalTriggerDetected>(OnGoalTriggerDetected);
    }

    private void OnGoalTriggerDetected(GoalTriggerDetected obj)
    {
        airplane.SetActive(true);
        exitAirplane.SetActive(true);
    }
}
