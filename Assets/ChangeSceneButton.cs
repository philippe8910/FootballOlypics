using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project;
using Project.Event.SubtitleEvent;
using UnityEngine;

public class ChangeSceneButton : ButtonEventAdd
{
    public async override void OnClick()
    {
        EventBus.Post(new ChangeSceneEffectDetected());

        await Task.Delay(3000);
        
        EventBus.ClearAllAction();

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
