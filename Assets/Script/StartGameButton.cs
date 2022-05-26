using System.Collections;
using System.Collections.Generic;
using Project;
using Project.Event;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : ButtonEventAdd
{
    public override void OnClick()
    {
        base.OnClick();
        EventBus.Post(new StartGameDetected());
    }
}
