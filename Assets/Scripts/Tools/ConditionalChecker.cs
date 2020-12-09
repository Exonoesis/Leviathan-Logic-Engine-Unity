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
    private Dictionary<Asset, List<Conditional>> _conditionsTable;
    private Dictionary<(Asset, Conditional), Scene> _errorSceneTable;

    public Scene getCurrentScene()
    {
        return _currentScene;
    }

    void Awake()
    {
        _conditionsTable = new Dictionary<Asset, List<Conditional>>();
        _errorSceneTable = new Dictionary<(Asset, Conditional), Scene>();
    }

    public void changeSceneIfSatisfied(Asset clickedAsset)
    {
        if (_conditionsTable.ContainsKey(clickedAsset))
        {
            List<Conditional> conditionsList = _conditionsTable[clickedAsset];

            foreach (Conditional condition in conditionsList)
            {
                if (!condition.isMet())
                {
                    showErrorScene(clickedAsset, condition);
                    return;
                }
            }
        }
        showNextScene(clickedAsset);
    }

    private void showErrorScene(Asset clickedAsset, Conditional failedCondition)
    {
        Scene errorToShow = _errorSceneTable[(clickedAsset, failedCondition)];

        _currentScene.hide();
        setCurrentScene(errorToShow);
        errorToShow.show();
    }

    private void showNextScene(Asset clickedAsset)
    {
        Scene sceneToShow = clickedAsset.getDesiredScene();

        _currentScene.hide();
        setCurrentScene(sceneToShow);
        sceneToShow.show();
    }

    public void setCurrentScene(Scene newScene)
    {
        _currentScene = newScene;
    }

    public void setConditionsTable(Dictionary<Asset, List<Conditional>> conditionsTable)
    {
        _conditionsTable = conditionsTable;
    }

    public void setErrorSceneTable(Dictionary<(Asset, Conditional), Scene> errorSceneTable)
    {
        _errorSceneTable = errorSceneTable;
    }
}