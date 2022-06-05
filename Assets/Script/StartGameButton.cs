using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project;
using Project.Event;
using Project.Event.SubtitleEvent;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : ButtonEventAdd
{
    public async override void OnClick()
    {
        //base.OnClick();
        EventBus.Post(new ChangeSceneEffectDetected());
        
        await Task.Delay(3000);
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
