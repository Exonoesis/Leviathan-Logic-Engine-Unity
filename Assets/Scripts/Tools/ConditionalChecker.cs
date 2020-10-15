using System.Collections.Generic;
using UnityEngine;

public class ConditionalChecker : MonoBehaviour
{
    private static ConditionalChecker _instance;
    public static ConditionalChecker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ConditionalChecker>();
            }

            return _instance;
        }
    }

    private Scene _currentScene;
    private Dictionary<Asset, List<Conditional>> conditionsTable;
    private Dictionary<Asset, Cutscene> errorSceneTable;

    public Scene getCurrentScene()
    {
        return _currentScene;
    }

    public void setCurrentScene(Scene scene)
    {
        _currentScene = scene;
    }

    void Awake()
    {
        conditionsTable = new Dictionary<Asset, List<Conditional>>();
        errorSceneTable = new Dictionary<Asset, Cutscene>();
    }

    public void changeSceneIfSatisfied(Asset clickedAsset)
    {
        if (conditionsTable.ContainsKey(clickedAsset))
        {
            List<Conditional> conditionsList = new List<Conditional>();

            conditionsList = conditionsTable[clickedAsset];

            foreach (Conditional condition in conditionsList)
            {
                if (condition.isMet())
                {
                    continue;
                }
                showErrorCutscene(clickedAsset);
                break;
            }
        }
        else
        {
            showNextScene(clickedAsset);
        }
    }

    private void showErrorCutscene(Asset clickedAsset)
    {
        Cutscene errorToShow = errorSceneTable[clickedAsset];

        _currentScene.hide();
        errorToShow.show();
    }

    private void showNextScene(Asset clickedAsset)
    {
        //Dictionary? | Key = string sceneName, Value = Scene
        //Scene sceneToShow = clickedAsset.getDesiredScene();

        _currentScene.hide();
        //sceneToShow.show();
    }
}