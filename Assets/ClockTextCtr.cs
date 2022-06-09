using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Project;
using Project.Event;

public class ClockTextCtr : MonoBehaviour
{
    [SerializeField] private Text clockText;

    [SerializeField] private int countLimit;
    
    void Start()
    {
        EventBus.Subscribe<BallSteppingScoreDetedted>(OnBallSteppingScoreDetedted);
    }

    private void OnBallSteppingScoreDetedted(BallSteppingScoreDetedted obj)
    {
        var scoreCount = obj.Score;

        clockText.text = scoreCount + "/" + countLimit;
    }

    void Update()
    {
        
    }
}
