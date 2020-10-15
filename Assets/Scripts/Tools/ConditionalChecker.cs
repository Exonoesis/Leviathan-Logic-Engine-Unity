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

    void Awake()
    {
        conditionsTable = new Dictionary<Asset, List<Conditional>>();
        errorSceneTable = new Dictionary<Asset, Cutscene>();
    }

    public void changeSceneIfSatisfied(Asset clickedAsset)
    {
        List<Conditional> conditionsList;
        conditionsTable.TryGetValue(clickedAsset, out conditionsList);

        foreach (Conditional condition in conditionsList)
        {
            if (!condition.isMet())
            {
                showErrorCutscene(clickedAsset);
                return;
            }
        }
        showNextScene(clickedAsset);
    }

    private void showErrorCutscene(Asset clickedAsset)
    {
        Cutscene errorToShow = errorSceneTable[clickedAsset];

        _currentScene.hide();
        errorToShow.show();
    }

    private void showNextScene(Asset clickedAsset)
    {
        Scene sceneToShow = clickedAsset.getDesiredScene();

        _currentScene.hide();
        sceneToShow.show();
    }
}