using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project;
using  Project.Event;

public class BlackBoardCtr : MonoBehaviour
{
    [SerializeField] private List<GameObject> blackBoardUI;
    void Start()
    {
        EventBus.Subscribe<ChangeLevelDetected>(OnChangeLevelDetected);
        CloseAllBlackBoardUI();
    }

    private void OnChangeLevelDetected(ChangeLevelDetected obj)
    {
        var currentLevel = obj.currentFootLevels;
        
        CloseAllBlackBoardUI();
        OpenBlackBoardUI(currentLevel);
    }

    private void OpenBlackBoardUI(FootLevels _currentFootLevels)
    {
        switch (_currentFootLevels)
        {
            case FootLevels.BallSteppingAction:
                blackBoardUI[0].SetActive(true);
                break;
            case FootLevels.InsideRightSideOfSeat:
                blackBoardUI[1].SetActive(true);
                break;
            case FootLevels.OutsideKick:
                blackBoardUI[2].SetActive(true);
                break;
        }
    }

    private void CloseAllBlackBoardUI()
    {
        for (int i = 0; i < blackBoardUI.Count; i++)
        {
            blackBoardUI[i].SetActive(false);
        }   
    }
}
