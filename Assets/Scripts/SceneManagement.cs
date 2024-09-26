using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string sceneTransitionName { get; private set; }

    public void setTransitionName(string sceneTransitionName)
    {
        this.sceneTransitionName = sceneTransitionName;
    }
}
