using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalChecker : MonoBehaviour
{
    private Scene _currentScene;

    public Scene getCurrentScene()
    {
        return _currentScene;
    }

    public void setCurrentScene(Scene scene)
    {
        _currentScene = scene;
    }


}