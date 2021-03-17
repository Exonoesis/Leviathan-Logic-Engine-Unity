using System.Collections.Generic;
using UnityEngine;

public class SceneNavigator : MonoBehaviour
{
    private static SceneNavigator _instance;
    public static SceneNavigator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneNavigator>();
            }
            return _instance;
        }
    }

    private Scene _currentScene;
    private Dictionary<Asset, List<Conditional>> _conditionsTable;
    private Dictionary<(Asset, Conditional), Cutscene> _errorSceneTable;
    
    void Awake()
    {
        _conditionsTable = new Dictionary<Asset, List<Conditional>>();
        _errorSceneTable = new Dictionary<(Asset, Conditional), Cutscene>();
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
                    Cutscene errorToShow = _errorSceneTable[(clickedAsset, condition)];
                    showScene(errorToShow);
                    return;
                }
            }
        }
        showScene(clickedAsset.getState().getNextScene());
    }
    private void showScene(Scene newScene)
    {
        _currentScene.hide();
        setCurrentScene(newScene);
        newScene.show();
    }
    
    public void addConditions(Asset asset, List<Conditional> conditionList)
    {
        _conditionsTable.Add(asset, conditionList);
    }

    public void addErrorScene(Asset asset, Conditional condition, Cutscene errorScene)
    {
        errorScene.setNextScene(_currentScene);
        
        _errorSceneTable.Add((asset, condition), errorScene);
    }

    public Scene getCurrentScene()
    {
        return _currentScene;
    }
    
    public void setCurrentScene(Scene newScene)
    {
        _currentScene = newScene;
    }
}