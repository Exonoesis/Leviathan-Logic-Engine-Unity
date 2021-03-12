﻿using System.Collections.Generic;
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
        Scene sceneToShow = clickedAsset.getState().getNextScene();

        _currentScene.hide();
        setCurrentScene(sceneToShow);
        sceneToShow.show();
    }

    public void setCurrentScene(Scene newScene)
    {
        _currentScene = newScene;
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
}