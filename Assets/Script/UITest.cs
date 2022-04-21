using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using TMPro;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private TextMesh testText;
    // Start is called before the first frame update
    void Start()
    {
        testText = GetComponent<TextMesh>();
        
        EventBus.Subscribe<BallSteppingScoreDetedted>(OnBallSteppingScoreDetedted);
    }

    private void OnBallSteppingScoreDetedted(BallSteppingScoreDetedted obj)
    {
        var scoreCount = obj.Score;

        testText.text = "" + scoreCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
